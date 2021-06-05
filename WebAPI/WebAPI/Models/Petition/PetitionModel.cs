using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Petition
{
    public class PetitionModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Topic is required")]
        public string Topic { get; set; }

        [Required(ErrorMessage = "PetitionDate is required")]
        public DateTime PetitionDate { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }


        [Required(ErrorMessage = "PetitionList is required")]
        public List<PetitionSignatureModel> SignatureList { get; set; }
    }
}
