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
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PubDate { get; set; }
        public DateTime LastEdited { get; set; }
        public bool IsPublished { get; set; } = true;



    }
}
