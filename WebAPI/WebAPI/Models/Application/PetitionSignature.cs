using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.Application
{
    public class PetitionSignature
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public int PetitionId { get; set; }
        [ForeignKey("PetitionId")]
        public Petition Petition { get; set; }
    }
}
