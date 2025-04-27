using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNTest.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public string ApiKey { get; set; }
    }
}
