﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EnrollmentDBEntities : DbContext
    {
        public EnrollmentDBEntities()
            : base("name=EnrollmentDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdultBasicInfo> AdultBasicInfoes { get; set; }
        public virtual DbSet<AdultEmergencyContact> AdultEmergencyContacts { get; set; }
        public virtual DbSet<AdultHealthInfo> AdultHealthInfoes { get; set; }
        public virtual DbSet<AdultLogin> AdultLogins { get; set; }
        public virtual DbSet<HighSchoolBasicInfo> HighSchoolBasicInfoes { get; set; }
        public virtual DbSet<HighSchoolEmergencyContact> HighSchoolEmergencyContacts { get; set; }
        public virtual DbSet<HighSchoolHealthInfo> HighSchoolHealthInfoes { get; set; }
        public virtual DbSet<HighSchoolLogin> HighSchoolLogins { get; set; }
    }
}
