using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BizBook.Models
{
    public class SavedBusiness
    {
        [Key]
        public int SavedBusinessId { get; set; }

        //[Display(Name = "Saved Businesses")]
        //public List<BusinessProfile> SavedBusinesses { get; set; }

        [ForeignKey("BusinessProfile")]
        [Display(Name = "Business Profile")]
        public int BusinessId { get; set; }
        public BusinessProfile businessProfile { get; set; }

        [ForeignKey("Consumer")]
        public int ConsumerId { get; set; }
        public Consumer Consumer { get; set; }
    }
}
