using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment_Application
{
    public class AdultConfidentialInfoClass
    {
        public int Id { get; set; }
        public string foodStamps { get; set; }
        public string dependentChildrenAid { get; set; }
        public string supplementaryIncome { get; set; }
        public string housingAssistance { get; set; }
        public string none { get; set; }
        public string homeless { get; set; }
        public string agedOutFosterCare { get; set; }
        public string outOfWorkforce { get; set; }
        public Nullable<System.DateTime> formCompletionDate { get; set; }
    }
}
