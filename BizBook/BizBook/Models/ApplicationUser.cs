using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BizBook.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Not Sure What Goes Here")]
        public string Name { get; set; }

        [NotMapped]
        public bool IsBusiness { get; set; }
    }
}
