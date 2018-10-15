using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BizBook.Models
{
    public class BlogPost
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }

        [ForeignKey("BusinessProfile")]
        [Display(Name = "Business Id")]
        public int BusinessId { get; set; }
        public BusinessProfile BusinessProfile { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Content")]
        public string Content { get; set; }

        [Display(Name = "Date Published")]
        public DateTime PubDate { get; set; }

        [Display(Name = "Date Edited")]
        public DateTime? LastEdited { get; set; }
    }
}
