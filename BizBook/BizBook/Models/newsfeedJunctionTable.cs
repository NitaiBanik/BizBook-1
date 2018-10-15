using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BizBook.Models
{
    public class NewsfeedJunctionTable
    {

        //public int ID { get; set; }
        public BlogPost blogPost { get; set; }

        //public int BusinessID { get; set; }
        public BusinessProfile businessProfile { get; set; }

        //public int SavedBusinessId { get; set; }
        public SavedBusiness savedBusiness { get; set; }
    }
}
