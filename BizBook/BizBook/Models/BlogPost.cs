using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BizBook.Models
{
    public class BlogPost
    {
        [Key]
        public string ID { get; set; }
        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }
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
