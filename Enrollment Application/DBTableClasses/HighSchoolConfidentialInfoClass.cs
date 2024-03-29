﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment_Application
{
    public class HighSchoolConfidentialInfoClass
    {
        public int Id { get; set; }
        public string foodStamps { get; set; }
        public string dependentChildrenAid { get; set; }
        public string supplementaryIncome { get; set; }
        public string housingAssistance { get; set; }
        public string none { get; set; }
        public string homeless { get; set; }
        public string agedOutFosterCare { get; set; }
        public string parentsActiveDuty { get; set; }
        public string reducedLunch { get; set; }
        public byte[] parentSignature { get; set; }
        public Nullable<System.DateTime> formCompletionDate { get; set; }
    }
}
