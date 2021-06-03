using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Models.Account;
using WebAPI.Models.Application;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Route("AccountInfo")]
        public async Task<IActionResult> AccountInfo()
        {
            string username = User.Claims.First(c => c.Type == ClaimTypes.Name).Value;

            var user = await _userManager.FindByNameAsync(username);
            return Ok(new
            {
                user.Email,
                user.FirstName,
                user.LastName,
                user.DateOfBirth,
                user.Country,
                user.City,
                user.AddressLine,
                user.ZipCode
            });
        }

        [HttpPost]
        [Route("AccountInfo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountInfo([FromBody] AccountInfoModel model)
        {
            // Get user
            string username = User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            var user = await _userManager.FindByNameAsync(username);

            // Update account
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DateOfBirth = model.DateOfBirth;
            user.Country = model.Country;
            user.City = model.City;
            user.AddressLine = model.AddressLine;
            user.ZipCode = model.ZipCode;

            var result = await _userManager.UpdateAsync(user);

            return Ok(result);
        }

        [HttpPost]
        [Route("ChangePassword")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            // Check if passwords match
            if (model.NewPassword != model.ConfirmNewPassword)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Passwords do not match." });

            // Get user
            string username = User.Claims.First(c => c.Type == ClaimTypes.Name).Value;
            var user = await _userManager.FindByNameAsync(username);

            // Update password
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            return Ok(result);
        }
    }
}
