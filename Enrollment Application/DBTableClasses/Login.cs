using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment_Application
{
    public class Login
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string passwordHash { get; set; }
        public string passwordSalt { get; set; }
        public Nullable<System.DateTime> dateCreated { get; set; }
        public string submitted { get; set; }
        public string studentType { get; set; }
    }
}
