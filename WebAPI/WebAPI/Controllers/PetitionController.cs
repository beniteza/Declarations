using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Models.Application;
using WebAPI.Models.Petition;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PetitionController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public PetitionController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("List")]
        [AllowAnonymous]
        public async Task<IActionResult> List()
        {
            var petitionList = await _db.Petition.Include(m => m.User).Include(m => m.PetitionSignatureList).ToListAsync();
            var list = new List<object>();

            foreach (var petition in petitionList)
            {
                list.Add(new
                {
                    id = petition.Id,
                    title = petition.Title,
                    description = petition.Description,
                    topic = petition.Topic,
                    userName = petition.User.UserName,
                    updatedDate = petition.UpdatedDate,
                    petitionDate = petition.PetitionDate,
                    country = petition.Country,
                    city = petition.City,
                    signatureCount = petition.PetitionSignatureList.Count
                });
            }

            return Ok(new
            {
                result = list
            });

        }

        [HttpGet]
        [Route("MyList")]
        public async Task<IActionResult> MyPetitionList()
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var petitionList = await _db.Petition.Include(m => m.PetitionSignatureList).Where(x => x.UserId == userId).ToListAsync();
            var list = new List<object>();

            foreach (var petition in petitionList)
            {
                list.Add(new
                {
                    petition.Id,
                    petition.Title,
                    petition.Topic,
                    petition.Description,
                    petition.UpdatedDate,
                    petition.PetitionDate,
                    petition.Country,
                    petition.City,
                    signatureCount = petition.PetitionSignatureList.Count
                });
            }

            return Ok(new
            {
                result = list
            });

        }

        [HttpGet]
        [Route("Get")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var petition = await _db.Petition.Include(m => m.User).Include(m => m.PetitionSignatureList).FirstOrDefaultAsync(u => u.Id == id);
            var signatureList = new List<object>();

            if (petition == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "The petition does not exist" });
            
            foreach (var signature in petition.PetitionSignatureList)
            {
                signatureList.Add(new 
                { 
                    id = signature.Id, 
                    firstName = signature.FirstName, 
                    lastName = signature.LastName,
                    email = signature.Email,
                    submittedDate = signature.SubmittedDate,
                    message = signature.Message
                });
            }

            return Ok(new
            {
                petition.Id,
                petition.Title,
                petition.Description,
                petition.Topic,
                author = $"{petition.User.FirstName} {petition.User.LastName} ({petition.User.UserName})",
                petition.UpdatedDate,
                petition.PetitionDate,
                petition.Country,
                petition.City,
                signatureList
            });

        }

        [HttpPost]
        [Route("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PetitionModel model)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "An error ocurred" });

            // Get UserId
            string userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var petition = new Petition();

            petition.Title = model.Title;
            petition.Description = model.Description;
            petition.Topic = model.Topic;
            petition.UserId = userId;
            petition.UpdatedDate = DateTime.Now;
            petition.PetitionDate = model.PetitionDate;
            petition.Country = model.Country;
            petition.City = model.City;

            // Signatures
            var signatureList = new List<PetitionSignature>();

            // Add
            foreach (var signature in model.SignatureList)
            {
                signatureList.Add(new PetitionSignature
                {
                    FirstName = signature.FirstName,
                    LastName = signature.LastName,
                    Email = signature.Email
                });
            }

            petition.PetitionSignatureList = signatureList;

            await _db.Petition.AddAsync(petition);
            var result = await _db.SaveChangesAsync();

            return Ok(petition.Id);
        }

        [HttpPut]
        [Route("Update")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PetitionModel model)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "An error ocurred" });

            if (model.Id == null || model.Id == 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Petition not specified" });
            
            // Get UserId
            string userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            Petition petition = await _db.Petition.Include(m => m.PetitionSignatureList).FirstOrDefaultAsync(u => u.Id == model.Id.Value);

            if (petition == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Petition does not exist" });
            
            petition.Title = model.Title;
            petition.Description = model.Description;
            petition.Topic = model.Topic;
            petition.UserId = userId;
            petition.UpdatedDate = DateTime.Now;
            petition.PetitionDate = model.PetitionDate;
            petition.Country = model.Country;
            petition.City = model.City;

            // Signatures
            var signatureList = petition.PetitionSignatureList;

            // Remove
            var count = signatureList.Count;
            for (int i = 0; i < count; i++)
            {
                var signature = signatureList[i];

                if (!model.SignatureList.Exists(x => x.Id == signature.Id))
                {
                    _db.PetitionSignature.Remove(signature);
                }
            }

            // Add or Update
            foreach (var signature in model.SignatureList)
            {
                var existingSignature = signatureList.Where(x => x.Id == signature.Id).FirstOrDefault();

                if (existingSignature != null)
                {
                    existingSignature.FirstName = signature.FirstName;
                    existingSignature.LastName = signature.LastName;
                    existingSignature.Email = signature.Email;
                }
                else
                {
                    signatureList.Add(new PetitionSignature
                    {
                        FirstName = signature.FirstName,
                        LastName = signature.LastName,
                        Email = signature.Email
                    });
                }
            }

            var result = await _db.SaveChangesAsync();

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete")]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Delete(int id)
        {
            var petition = await _db.Petition.FirstOrDefaultAsync(u => u.Id == id);

            if (petition == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Petition does not exist" });
         
            // Check if user allowed to delete
            string userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (petition.UserId != userId)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User not authorized to delete" });
            
            // Delete
            _db.Petition.Remove(petition);

            var result = await _db.SaveChangesAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("AddSignature")]
        [AllowAnonymous]
        public async Task<IActionResult> AddSignature(PetitionSignatureModel model)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "An error ocurred" });

            if(model.PetitionId < 1)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Invalid petition" });

            var signature = new PetitionSignature();

            signature.PetitionId = model.PetitionId;
            signature.FirstName = model.FirstName;
            signature.LastName = model.LastName;
            signature.Email = model.Email;
            signature.Message = model.Message;
            signature.SubmittedDate = DateTime.Now;

            await _db.PetitionSignature.AddAsync(signature);
            var result = await _db.SaveChangesAsync();

            return Ok(new 
            {
                signature.Id,
                signature.FirstName,
                signature.LastName,
                signature.Email,
                signature.SubmittedDate,
                signature.Message
            });
        }

        [HttpDelete]
        [Route("DeleteSignature")]
        public async Task<IActionResult> DeleteSignature(int petitionId, int signatureId)
        {
            var petition = await _db.Petition.Include(m => m.PetitionSignatureList).FirstOrDefaultAsync(u => u.Id == petitionId);

            if (petition == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Petition does not exist" });

            // Check if user allowed to delete
            string userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (petition.UserId != userId)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User not authorized to delete" });

            // Check if signature exists
            var signature = petition.PetitionSignatureList.Where(x => x.Id == signatureId).FirstOrDefault();

            if (signature == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Signature does not exist" });

            // Delete
            _db.PetitionSignature.Remove(signature);

            var result = await _db.SaveChangesAsync();
            return Ok(result);
        }
    }
}
