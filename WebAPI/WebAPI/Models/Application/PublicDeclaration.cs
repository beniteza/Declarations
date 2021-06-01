using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Application
{
    public class PublicDeclaration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public DateTime SubmittedDate { get; set; }
        [Required]
        public DateTime DeclarationDate { get; set; }
        [Required]
        public string State { get; set; }

        public List<PublicDeclarationSignature> PublicDeclarationSignatureList { get; set; }
    }
}
