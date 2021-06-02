using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Account
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "CurrentPassword is required")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "NewPassword is required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "ConfirmNewPassword is required")]
        public string ConfirmNewPassword { get; set; }
    }
}
