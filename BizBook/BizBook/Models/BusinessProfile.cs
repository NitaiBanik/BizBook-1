using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BizBook.Models
{
    public class BusinessProfile
    {
        [Key]
        public int BusinessID { get; set; }

        [Display(Name = "Name of Your Business")]
        public string BusinessName { get; set; }

        [Display(Name = "Type of Business")]
        public string BusinessType { get; set; }

        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "City, State, Zip")]
        public string CityStateZip { get; set; }

        [Display(Name = "Give a brief description of your business and the services you offer.")]
        public string BusinessBio { get; set; }

        [Display(Name = "List and Special promotions of cupons here")]
        public string Promotions { get; set; }

        [Display(Name = "Link to your website.")]
        public string Link { get; set; }
        
    }
}
