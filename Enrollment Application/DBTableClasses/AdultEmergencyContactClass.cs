using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment_Application
{
    public class AdultEmergencyContactClass
    {
        public int Id { get; set; }
        public string contactName { get; set; }
        public string relationship { get; set; }
        public string primaryNum { get; set; }
        public string alternateNum { get; set; }
        public string nameNearestRelative { get; set; }
        public string NRrelationship { get; set; }
        public string NRstreetAddress { get; set; }
        public string NRcity { get; set; }
        public string NRstate { get; set; }
        public string NRzip { get; set; }
        public string NRprimaryNum { get; set; }
        public string NRworkNum { get; set; }
        public string NRcellNum { get; set; }
    }
}
