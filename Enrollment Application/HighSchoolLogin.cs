//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Enrollment_Application
{
    using System;
    using System.Collections.Generic;
    
    public partial class HighSchoolLogin
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