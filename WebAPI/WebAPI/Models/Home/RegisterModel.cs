using System;
using System.ComponentModel.DataAnnotations;
using WebAPI.Models.Account;

namespace WebAPI.Models.Home
{
    public class RegisterModel : AccountInfoModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
}
