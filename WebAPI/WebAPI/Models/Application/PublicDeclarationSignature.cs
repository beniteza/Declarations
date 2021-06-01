using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.Application
{
    public class PublicDeclarationSignature
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PersonName { get; set; }

        public int PublicDeclarationId { get; set; }
        [ForeignKey("PublicDeclarationId")]
        public PublicDeclaration PublicDeclaration { get; set; }
    }
}
