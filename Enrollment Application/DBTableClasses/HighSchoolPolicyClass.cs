using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment_Application
{
    public class HighSchoolPolicyClass
    {
        public int Id { get; set; }
        public string attendance { get; set; }
        public string tobacco { get; set; }
        public string internetAccess { get; set; }
        public string studentInsurance { get; set; }
        public string fieldTrips { get; set; }
        public string drugTesting { get; set; }
        public string noticeOfDisclosures { get; set; }
        public string cellPhoneContact { get; set; }
        public string releaseForPhotography { get; set; }
        public byte[] studentSignature { get; set; }
        public byte[] parentSignature { get; set; }
    }
}
