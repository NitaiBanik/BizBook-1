using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BizBook.Models
{
    public class BusinessProfile
    {
        [Key]
        public int BusinessID { get; set; }

        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }

        [Display(Name = "Type of Business")]
        public string BusinessType { get; set; }

        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "City, State, Zip")]
        public string CityStateZip { get; set; }

<<<<<<< HEAD
        [Display(Name = "Description")]
        public string BusinessBio { get; set; }

        [Display(Name = "Promos")]
        public string Promotions { get; set; }

        [Display(Name = "Click Link to Learn More")]
=======
        [Display(Name = "Business Bio")]
        public string BusinessBio { get; set; }

        [Display(Name = "Current Promotions")]
        public string Promotions { get; set; }

        [Display(Name = "Website Link")]
>>>>>>> 45a9604ee67fb55446a3b0a655e89b90b008e72e
        public string Link { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        
        [Display(Name = "Image 1")]
        public string Image1 { get; set; }

    }
}
