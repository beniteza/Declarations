using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models.Application
{
    public class Petition
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Topic { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [Required]
        public DateTime UpdatedDate { get; set; }
        [Required]
        public DateTime PetitionDate { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }

        public List<PetitionSignature> PetitionSignatureList { get; set; }
    }
}
