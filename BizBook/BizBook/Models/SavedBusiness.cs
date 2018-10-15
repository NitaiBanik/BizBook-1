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

        [ForeignKey("BusinessProfile")]
        public int BusinessId { get; set; }
        public BusinessProfile businessProfile { get; set; }

        [ForeignKey("Consumer")]
        public int ConsumerId { get; set; }
        public Consumer Consumer { get; set; }

        //[ForeignKey("BlogPost")]
        //public int BlogPostId { get; set; }
        //public BlogPost BlogPost { get; set; }
    }
}
