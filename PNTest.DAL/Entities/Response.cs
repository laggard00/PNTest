using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNTest.DAL.Entities
{
    public class Response
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public virtual ICollection<Location>? Locations { get; set; }

    }
}
