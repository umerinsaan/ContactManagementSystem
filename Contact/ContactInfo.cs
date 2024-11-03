using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact
{
    public class ContactInfo
    {
        public int ID {  get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string Telephone {  get; set; }
        public string Extension { get; set; }
        public string EmailAddress { get; set; }
    }
}
