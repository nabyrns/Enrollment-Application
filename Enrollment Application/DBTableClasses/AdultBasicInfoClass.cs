using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment_Application
{
    public class AdultBasicInfoClass
    {
        public int Id { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string middleInitial { get; set; }
        public string preferredName { get; set; }
        public string program { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string primaryPhoneNum { get; set; }
        public string cellPhoneNum { get; set; }
        public string hispanicOrLatino { get; set; }
        public string race { get; set; }
        public string gender { get; set; }
        public Nullable<System.DateTime> dateOfBirth { get; set; }
        public string SSN { get; set; }
        public string completedEdLevel { get; set; }
        public string attendedCollegeOrTech { get; set; }
        public string liveWithParent { get; set; }
    }
}
