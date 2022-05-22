using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organic_Food_01_EXM.Models
{
    public class Subscriber
    {
        public int Id { get; set; }
        public string EmailAddress { get; set; }
    }
    public class ContactInfo
    {
        public int id { get; set; }
        public string CustomerName { get; set; }
        public string EmailAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
