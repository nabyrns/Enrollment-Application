using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment_Application
{
    public class HighSchoolHealthInfoClass
    {
        public int Id { get; set; }
        public string primaryPhysician { get; set; }
        public string otherPhysician { get; set; }
        public string pPhysicianPhoneNum { get; set; }
        public string oPhysicianPhoneNum { get; set; }
        public string diabeticType { get; set; }
        public string allergies { get; set; }
        public string heartIssues { get; set; }
        public string metabolic { get; set; }
        public string jointMuscle { get; set; }
        public string chronicIllness { get; set; }
        public string migraines { get; set; }
        public string neurological { get; set; }
        public string pulmonary { get; set; }
        public string asthma { get; set; }
        public string other { get; set; }
        public string otherMeds { get; set; }
        public string specificFirstAidNeeds { get; set; }
        public string repPermissionForTreatment { get; set; }
        public byte[] healthSignature { get; set; }
    }
}
