using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Application;

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
        [Route("PetitionList")]
        [AllowAnonymous]
        public async Task<IActionResult> PetitionList()
        {
            var petitionList = await _db.Petition.ToListAsync();
            var list = new List<object>();

            foreach (var petition in petitionList)
            {
                list.Add(new
                {
                    id = petition.Id,
                    title = petition.Title,
                    description = petition.Description,
                    userId = petition.UserId,
                    submittedDate = petition.SubmittedDate,
                    declarationDate = petition.PetitionDate,
                    state = petition.State
                });
            }

            return new JsonResult(new
            {
                result = list
            });

        }
    }
}
