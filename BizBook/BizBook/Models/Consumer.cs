using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BizBook.Models
{
    public class Consumer
    {
        [Key]
        public int ConsumerID { get; set; }

        [Display(Name = "Your Name")]
        public string ConsumerName { get; set; }

        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "City, State, Zip")]
        public string CityStateZip { get; set; }

        [Display(Name = "Would you like to receive email notifications when new businesses join?")]
        public bool IsSubscribed { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
