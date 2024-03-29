﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib;

namespace Enrollment_Application
{
    public class DataAccess
    {
        #region Checks emails
        public bool CheckEmails(string email)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                var check = connection.Query<Login>($"Select * from AllLogins where email = @Email", new { Email = email }).FirstOrDefault();

                if (check != null)
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Adds a new login
        public void AddLogin(string Email, string PasswordHash, string PasswordSalt, DateTime DateCreated, string Submitted, string StudentType)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {

                Login l = new Login()
                {
                    email = Email,
                    passwordHash = PasswordHash,
                    passwordSalt = PasswordSalt,
                    dateCreated = DateCreated,
                    submitted = Submitted,
                    studentType = StudentType
                };

                if (StudentType == "High School")
                {
                    connection.Execute("dbo.AddHSLogin @email, @passwordHash, @passwordSalt, @dateCreated, @submitted, @studentType", l);
                }

                else
                {
                    connection.Execute("dbo.AddAdultLogin @email, @passwordHash, @passwordSalt, @dateCreated, @submitted, @studentType", l);
                }
            }
        }
        #endregion

        #region Checks the specific tables containing login info --- this area is used during the login process
        public Login HSCheck(string email)
        {
            Login check;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                check = connection.Query<Login>($"Select * from HighSchoolLogins where email = @Email", new {Email =  email }).FirstOrDefault();
            }

            return check;
        }

        public Login AdultCheck(string email)
        {
            Login check;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                check = connection.Query<Login>($"Select * from AdultLogins where email = @Email", new { Email = email }).FirstOrDefault();
            }

            return check;
        }
        #endregion

        #region These methods are for creating the user controls that will be used in the application
        public AdultBasicInfoClass GetABI(int Id)
        {
            AdultBasicInfoClass abi;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                abi = connection.Query<AdultBasicInfoClass>($"Select * from AdultBasicInfo where Id = @id", new { id = Id }).FirstOrDefault();
            }

            return abi;
        }

        public HighSchoolBasicInfoClass GetHSBI(int Id)
        {
            HighSchoolBasicInfoClass hsbi;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                hsbi = connection.Query<HighSchoolBasicInfoClass>($"Select * from HighSchoolBasicInfo where Id = @id", new { id = Id }).FirstOrDefault();
            }

            return hsbi;
        }

        public AdultHealthInfoClass GetAHI(int Id)
        {
            AdultHealthInfoClass ahi;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                ahi = connection.Query<AdultHealthInfoClass>($"Select * from AdultHealthInfo where Id = @id", new { id = Id }).FirstOrDefault();
            }

            return ahi;
        }

        public HighSchoolHealthInfoClass GetHSHI(int Id)
        {
            HighSchoolHealthInfoClass hshi;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                hshi = connection.Query<HighSchoolHealthInfoClass>($"Select * from HighSchoolHealthInfo where Id = @id", new { id = Id }).FirstOrDefault();
            }

            return hshi;
        }

        public AdultEmergencyContactClass GetAEC(int Id)
        {
            AdultEmergencyContactClass aec;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                aec = connection.Query<AdultEmergencyContactClass>($"Select * from AdultEmergencyContact where Id = @id", new { id = Id }).FirstOrDefault();
            }

            return aec;
        }

        public HighSchoolEmergencyContactClass GetHSEC(int Id)
        {
            HighSchoolEmergencyContactClass hsec;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                hsec = connection.Query<HighSchoolEmergencyContactClass>($"Select * from HighSchoolEmergencyContact where Id = @id", new { id = Id }).FirstOrDefault();
            }

            return hsec;
        }

        public AdultPolicyClass GetAP(int Id)
        {
            AdultPolicyClass ap;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                ap = connection.Query<AdultPolicyClass>($"Select * from AdultPolicy where Id = @id", new { id = Id }).FirstOrDefault();
            }

            return ap;
        }

        public HighSchoolPolicyClass GetHSP(int Id)
        {
            HighSchoolPolicyClass hsp;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                hsp = connection.Query<HighSchoolPolicyClass>($"Select * from HighSchoolPolicy where Id = @id", new { id = Id }).FirstOrDefault();
            }

            return hsp;
        }

        public AdultConfidentialInfoClass GetACI(int Id)
        {
            AdultConfidentialInfoClass aci;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                aci = connection.Query<AdultConfidentialInfoClass>($"Select * from AdultConfidentialInfo where Id = @id", new { id = Id }).FirstOrDefault();
            }

            return aci;
        }

        public HighSchoolConfidentialInfoClass GetHSCI(int Id)
        {
            HighSchoolConfidentialInfoClass hsci;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                hsci = connection.Query<HighSchoolConfidentialInfoClass>($"Select * from HighSchoolConfidentialInfo where Id = @id", new { id = Id }).FirstOrDefault();
            }

            return hsci;
        }
        #endregion

        #region These methods are for saving information to the database tables
        public void SaveABI(AdultBasicInfoClass abi, AdultBasicInfoTextValidation abiCheck)
        {
            string query = $"Update AdultBasicInfo Set ";
            List<string> listToSave = new List<string>( new string[] { "lastName = @LastName", "firstName = @FirstName", "middleInitial = @MiddleInitial", "preferredName = @PreferredName", "program = @Program", "streetAddress = @StreetAddress", "city = @City", "state = @State", "zipCode = @ZipCode", "primaryPhoneNum = @PrimaryPhoneNum", "cellPhoneNum = @CellPhoneNum", "hispanicOrLatino = @HispanicOrLatino", "race = @Race", "gender = @Gender", "dateOfBirth = @DateOfBirth", "SSN = @ssn", "completedEdLevel = @CompletedEdLevel", "attendedCollegeOrTech = @AttendedCollegeOrTech", "liveWithParent = @LiveWithParent" } );

            List<string> toRemove = abiCheck.IsValid;

            foreach(var v in toRemove)
            {
                for (int i = 0; i < listToSave.Count; i++)
                {
                    if (listToSave[i].Contains(v))
                    {
                        listToSave.RemoveAt(i);
                        i--;
                    }
                }
            }

            foreach (var s in listToSave)
            {
                if (listToSave.IndexOf(s) != listToSave.Count - 1)
                {
                    query += s + ",";
                }

                else
                {
                    query += s;
                }
            }

            query += " Where Id = @id";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                connection.Execute(query,
                    new { LastName = abi.lastName, FirstName = abi.firstName, MiddleInitial = abi.middleInitial, PreferredName = abi.preferredName, Program = abi.program, StreetAddress = abi.streetAddress, City = abi.city, State = abi.state, ZipCode = abi.zipCode, PrimaryPhoneNum = abi.primaryPhoneNum, CellPhoneNum = abi.cellPhoneNum, HispanicOrLatino = abi.hispanicOrLatino, Race = abi.race, Gender = abi.gender, DateOfBirth = abi.dateOfBirth, ssn = abi.SSN, CompletedEdLevel = abi.completedEdLevel, AttendedCollegeOrTech = abi.attendedCollegeOrTech, LiveWithParent = abi.liveWithParent, id = abi.Id });

            }
        }

        public void SaveACI(AdultConfidentialInfoClass aci)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                connection.Execute($"Update AdultConfidentialInfo Set formCompletionDate = @FormCompletionDate, foodStamps = @FoodStamps, dependentChildrenAid = @DependentChildrenAid, supplementaryIncome = @SupplementaryIncome, housingAssistance = @HousingAssistance, none = @None, homeless = @Homeless, agedOutFosterCare = @AgedOutFosterCare, outOfWorkforce = @OutOfWorkforce Where Id = @id",
                    new { FormCompletionDate = aci.formCompletionDate, FoodStamps = aci.foodStamps, DependentChildrenAid = aci.dependentChildrenAid, SupplementaryIncome = aci.supplementaryIncome, HousingAssistance = aci.housingAssistance, None = aci.none, Homeless = aci.homeless, AgedOutFosterCare = aci.agedOutFosterCare, OutOfWorkforce = aci.outOfWorkforce, id = aci.Id});

                connection.Execute($"Update AdultLogins Set submitted = @Submitted, dateSubmitted = @DateSubmitted Where Id = @id", new { Submitted = "Yes", DateSubmitted = DateTime.Now, id = aci.Id });
            }
        }

        public void SaveAEC(AdultEmergencyContactClass aec, AdultECUCTextValidation aecCheck)
        {
            string query = $"Update AdultEmergencyContact Set ";
            List<string> listToSave = new List<string>(new string[] { "contactName = @ContactName", "relationship = @Relationship", "primaryNum = @PrimaryNum", "alternateNum = @AlternateNum", "nameNearestRelative = @NameNearestRelative", "NRrelationship = @nrRelationship", "NRstreetAddress = @nrStreetAddress", "NRcity = @nrCity", "NRstate = @nrState", "NRzip = @nrZip", "NRprimaryNum = @nrPrimaryNum", "NRworkNum = @nrWorkNum", "NRcellNum = @nrCellNum" } );
            List<string> toRemove = aecCheck.IsValid;

            foreach (var v in toRemove)
            {
                for (int i = 0; i < listToSave.Count; i++)
                {
                    if (listToSave[i].Contains(v))
                    {
                        listToSave.RemoveAt(i);
                        i--;
                    }
                }
            }

            foreach (var s in listToSave)
            {
                if (listToSave.IndexOf(s) != listToSave.Count - 1)
                {
                    query += s + ",";
                }

                else
                {
                    query += s;
                }
            }

            query += " Where Id = @id";

            if (query.Contains("Set  Where"))
            {
                return;
            }

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                connection.Execute(query,
                    new { ContactName = aec.contactName, Relationship = aec.relationship, PrimaryNum = aec.primaryNum, AlternateNum = aec.alternateNum, NameNearestRelative = aec.nameNearestRelative, nrRelationship = aec.NRrelationship, nrStreetAddress = aec.NRstreetAddress, nrCity = aec.NRcity, nrState = aec.NRstate, nrZip = aec.NRzip, nrPrimaryNum = aec.NRprimaryNum, nrWorkNum = aec.NRworkNum, nrCellNum = aec.NRcellNum, id = aec.Id});
            }
        }

        public void SaveAHI(AdultHealthInfoClass ahi, HealthInfoTextValidation hiCheck)
        {
            string query = $"Update AdultHealthInfo Set ";
            List<string> listToSave = new List<string>(new string[] { "primaryPhysician = @PrimaryPhysician", "otherPhysician = @OtherPhysician", "pPhysicianPhoneNum = @PphysicianPhoneNum", "oPhysicianPhoneNum = @OphysicianPhoneNum", "diabeticType = @DiabeticType", "allergies = @Allergies", "heartIssues = @HeartIssues", "metabolic = @Metabolic", "jointMuscle = @JointMuscle", "chronicIllness = @ChronicIllness", "migraines = @Migraines", "neurological = @Neurological", "pulmonary = @Pulmonary", "asthma = @Asthma", "other = @Other", "otherMeds = @OtherMeds", "specificFirstAidNeeds = @SpecificFirstAidNeeds", "repPermissionForTreatment = @RepPermissionForTreatment", "healthSignature = @signature" });
            List<string> toRemove = hiCheck.IsValid;

            foreach (var v in toRemove)
            {
                for (int i = 0; i < listToSave.Count; i++)
                {
                    if (listToSave[i].Contains(v))
                    {
                        listToSave.RemoveAt(i);
                        i--;
                    }
                }
            }

            foreach (var s in listToSave)
            {
                if (listToSave.IndexOf(s) != listToSave.Count - 1)
                {
                    query += s + ",";
                }

                else
                {
                    query += s;
                }
            }

            query += " Where Id = @id";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {

                connection.Execute(query,
                    new { signature = ahi.healthSignature, PrimaryPhysician = ahi.primaryPhysician, OtherPhysician = ahi.otherPhysician, PphysicianPhoneNum = ahi.pPhysicianPhoneNum, OphysicianPhoneNum = ahi.oPhysicianPhoneNum, DiabeticType = ahi.diabeticType, Allergies = ahi.allergies, HeartIssues = ahi.heartIssues, Metabolic = ahi.metabolic, JointMuscle = ahi.jointMuscle, ChronicIllness = ahi.chronicIllness, Migraines = ahi.migraines, Neurological = ahi.neurological, Pulmonary = ahi.pulmonary, Asthma = ahi.asthma, Other = ahi.other, OtherMeds = ahi.otherMeds, SpecificFirstAidNeeds = ahi.specificFirstAidNeeds, RepPermissionForTreatment = ahi.repPermissionForTreatment, id = ahi.Id });
            }
        }

        public void SaveAP(AdultPolicyClass ap, PolicyTextValidation pCheck)
        {
            string query = $"Update AdultPolicy Set ";
            List<string> listToSave = new List<string>(new string[] { "attendance = @Attendance", "tobacco = @Tobacco", "internetAccess = @InternetAccess", "studentInsurance = @StudentInsurance", "fieldTrips = @FieldTrips", "drugTesting = @DrugTesting", "noticeOfDisclosures = @NoticeOfDisclosures", "cellPhoneContact = @CellPhoneContact", "releaseForPhotography = @ReleaseForPhotography", "studentSignature = @signature" });

            List<string> toRemove = pCheck.IsValid;

            foreach (var v in toRemove)
            {
                for (int i = 0; i < listToSave.Count; i++)
                {
                    if (listToSave[i].Contains(v))
                    {
                        listToSave.RemoveAt(i);
                        i--;
                    }
                }
            }

            foreach (var s in listToSave)
            {
                if (listToSave.IndexOf(s) != listToSave.Count - 1)
                {
                    query += s + ",";
                }

                else
                {
                    query += s;
                }
            }

            query += " Where Id = @id";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                connection.Execute(query,
                    new { signature = ap.studentSignature, Attendance = ap.attendance, Tobacco = ap.tobacco, InternetAccess = ap.internetAccess, StudentInsurance = ap.studentInsurance, FieldTrips = ap.fieldTrips, DrugTesting = ap.drugTesting, NoticeOfDisclosures = ap.noticeOfDisclosures, CellPhoneContact = ap.cellPhoneContact, ReleaseForPhotography = ap.releaseForPhotography, id = ap.Id });
            }
        }

        public void SaveHSBI(HighSchoolBasicInfoClass hsbi, HighSchoolBasicInfoTextValidation hsbiCheck)
        {
            string query = $"Update HighSchoolBasicInfo Set ";
            List<string> listToSave = new List<string>(new string[] { "lastName = @LastName", "firstName = @FirstName", "middleInitial = @MiddleInitial", "preferredName = @PreferredName", "program = @Program", "streetAddress = @StreetAddress", "city = @City", "state = @State", "zipCode = @ZipCode", "primaryPhoneNum = @PrimaryPhoneNum", "cellPhoneNum = @CellPhoneNum", "hispanicOrLatino = @HispanicOrLatino", "race = @Race", "gender = @Gender", "dateOfBirth = @DateOfBirth", "currentEdLevel = @CurrentEdLevel", "sendingHS = @SendingHS" });

            List<string> toRemove = hsbiCheck.IsValid;

            foreach (var v in toRemove)
            {
                for (int i = 0; i < listToSave.Count; i++)
                {
                    if (listToSave[i].Contains(v))
                    {
                        listToSave.RemoveAt(i);
                        i--;
                    }
                }
            }

            foreach (var s in listToSave)
            {
                if (listToSave.IndexOf(s) != listToSave.Count - 1)
                {
                    query += s + ",";
                }

                else
                {
                    query += s;
                }
            }

            query += " Where Id = @id";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                connection.Execute(query,
                    new { LastName = hsbi.lastName, FirstName = hsbi.firstName, MiddleInitial = hsbi.middleInitial, PreferredName = hsbi.preferredName, Program = hsbi.program, StreetAddress = hsbi.streetAddress, City = hsbi.city, State = hsbi.state, ZipCode = hsbi.zipCode, PrimaryPhoneNum = hsbi.primaryPhoneNum, CellPhoneNum = hsbi.cellPhoneNum, HispanicOrLatino = hsbi.hispanicOrLatino, Race = hsbi.race, Gender = hsbi.gender, DateOfBirth = hsbi.dateOfBirth, CurrentEdLevel = hsbi.currentEdLevel, SendingHS = hsbi.sendingHS, id = hsbi.Id });
            }
        }

        public void SaveHSCI(HighSchoolConfidentialInfoClass hsci)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                connection.Execute($"Update HighSchoolConfidentialInfo Set formCompletionDate = @FormCompletionDate, foodStamps = @FoodStamps, dependentChildrenAid = @DependentChildrenAid, supplementaryIncome = @SupplementaryIncome, housingAssistance = @HousingAssistance, none = @None, homeless = @Homeless, agedOutFosterCare = @AgedOutFosterCare, parentsActiveDuty = @ParentsActiveDuty, reducedLunch = @ReducedLunch, parentSignature = @parentSig Where Id = @id",
                    new { FormCompletionDate = hsci.formCompletionDate, parentSig = hsci.parentSignature, FoodStamps = hsci.foodStamps, DependentChildrenAid = hsci.dependentChildrenAid, SupplementaryIncome = hsci.supplementaryIncome, HousingAssistance = hsci.housingAssistance, None = hsci.none, Homeless = hsci.homeless, AgedOutFosterCare = hsci.agedOutFosterCare, ParentsActiveDuty = hsci.parentsActiveDuty, ReducedLunch = hsci.reducedLunch, id = hsci.Id });

                connection.Execute($"Update HighSchoolLogins Set submitted = @Submitted, dateSubmitted = @DateSubmitted Where Id = @id", new { Submitted = "Yes", DateSubmitted = DateTime.Now, id = hsci.Id });
            }
        }

        public void SaveHSHI(HighSchoolHealthInfoClass hshi, HealthInfoTextValidation hiCheck)
        {
            string query = $"Update HighSchoolHealthInfo Set ";
            List<string> listToSave = new List<string>(new string[] { "primaryPhysician = @PrimaryPhysician", "otherPhysician = @OtherPhysician", "pPhysicianPhoneNum = @PphysicianPhoneNum", "oPhysicianPhoneNum = @OphysicianPhoneNum", "diabeticType = @DiabeticType", "allergies = @Allergies", "heartIssues = @HeartIssues", "metabolic = @Metabolic", "jointMuscle = @JointMuscle", "chronicIllness = @ChronicIllness", "migraines = @Migraines", "neurological = @Neurological", "pulmonary = @Pulmonary", "asthma = @Asthma", "other = @Other", "otherMeds = @OtherMeds", "specificFirstAidNeeds = @SpecificFirstAidNeeds", "repPermissionForTreatment = @RepPermissionForTreatment", "healthSignature = @signature" });
            List<string> toRemove = hiCheck.IsValid;

            foreach (var v in toRemove)
            {
                for (int i = 0; i < listToSave.Count; i++)
                {
                    if (listToSave[i].Contains(v))
                    {
                        listToSave.RemoveAt(i);
                        i--;
                    }
                }
            }

            foreach (var s in listToSave)
            {
                if (listToSave.IndexOf(s) != listToSave.Count - 1)
                {
                    query += s + ",";
                }

                else
                {
                    query += s;
                }
            }

            query += " Where Id = @id";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {

                connection.Execute(query,
                    new { signature = hshi.healthSignature, PrimaryPhysician = hshi.primaryPhysician, OtherPhysician = hshi.otherPhysician, PphysicianPhoneNum = hshi.pPhysicianPhoneNum, OphysicianPhoneNum = hshi.oPhysicianPhoneNum, DiabeticType = hshi.diabeticType, Allergies = hshi.allergies, HeartIssues = hshi.heartIssues, Metabolic = hshi.metabolic, JointMuscle = hshi.jointMuscle, ChronicIllness = hshi.chronicIllness, Migraines = hshi.migraines, Neurological = hshi.neurological, Pulmonary = hshi.pulmonary, Asthma = hshi.asthma, Other = hshi.other, OtherMeds = hshi.otherMeds, SpecificFirstAidNeeds = hshi.specificFirstAidNeeds, RepPermissionForTreatment = hshi.repPermissionForTreatment, id = hshi.Id });
            }
        }

        public void SaveHSEC(HighSchoolEmergencyContactClass hsec, HighSchoolECUCTextValidation hsecCheck)
        {
            string query = $"Update HighSchoolEmergencyContact Set ";
            List<string> listToSave = new List<string>(new string[] { "parentNameOne = @ParentNameOne", "parentOneRelationship = @ParentOneRelationship", "parentOneAddress = @ParentOneAddress", "parentOneCity = @ParentOneCity", "parentOneState = @ParentOneState", "parentOneZip = @ParentOneZip", "parentOnePrimaryNum = @ParentOnePrimaryNum", "parentOneWorkNum = @ParentOneWorkNum", "parentOneCellNum = @ParentOneCellNum", "parentOneEmail = @ParentOneEmail", "residesWithP1 = @ResidesWithP1", "parentNameTwo = @ParentNameTwo", "parentTwoRelationship = @ParentTwoRelationship", "parentTwoAddress = @ParentTwoAddress", "parentTwoCity = @ParentTwoCity", "parentTwoState = @ParentTwoState", "parentTwoZip = @ParentTwoZip", "parentTwoPrimaryNum = @ParentTwoPrimaryNum", "parentTwoWorkNum = @ParentTwoWorkNum", "parentTwoCellNum = @ParentTwoCellNum", "parentTwoEmail = @ParentTwoEmail", "residesWithP2 = @ResidesWithP2", "EContactName = @eContactName", "EContactRelationship = @eContactRelationship", "EContactPrimaryNum = @eContactPrimaryNum", "EContactWorkNum = @eContactWorkNum", "EContactCellNum = @eContactCellNum" });
            List<string> toRemove = hsecCheck.IsValid;

            foreach (var v in toRemove)
            {
                for (int i = 0; i < listToSave.Count; i++)
                {
                    if (listToSave[i].Contains(v))
                    {
                        listToSave.RemoveAt(i);
                        i--;
                    }
                }
            }

            foreach (var s in listToSave)
            {
                if (listToSave.IndexOf(s) != listToSave.Count - 1)
                {
                    query += s + ",";
                }

                else
                {
                    query += s;
                }
            }

            query += " Where Id = @id";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                connection.Execute(query,
                    new { ParentNameOne = hsec.parentNameOne, ParentOneRelationship = hsec.parentOneRelationship, ParentOneAddress = hsec.parentOneAddress, ParentOneCity = hsec.parentOneCity, ParentOneState = hsec.parentOneState, ParentOneZip = hsec.parentOneZip, ParentOnePrimaryNum = hsec.parentOnePrimaryNum, ParentOneWorkNum = hsec.parentOneWorkNum, ParentOneCellNum = hsec.parentOneCellNum, ParentOneEmail = hsec.parentOneEmail, ResidesWithP1 = hsec.residesWithP1, ParentNameTwo = hsec.parentNameTwo, ParentTwoRelationship = hsec.parentTwoRelationship, ParentTwoAddress = hsec.parentTwoAddress, ParentTwoCity = hsec.parentTwoCity, ParentTwoState = hsec.parentTwoState, ParentTwoZip = hsec.parentTwoZip, ParentTwoPrimaryNum = hsec.parentTwoPrimaryNum, ParentTwoWorkNum = hsec.parentTwoWorkNum, ParentTwoCellNum = hsec.parentTwoCellNum, ParentTwoEmail = hsec.parentTwoEmail, ResidesWithP2 = hsec.residesWithP2, eContactName = hsec.EContactName, eContactRelationship = hsec.EContactRelationship, eContactPrimaryNum = hsec.EContactPrimaryNum, eContactWorkNum = hsec.EContactWorkNum, eContactCellNum = hsec.EContactCellNum, id = hsec.Id });
            }
        }

        public void SaveHSP(HighSchoolPolicyClass hsp, PolicyTextValidation pCheck)
        {
            string query = $"Update HighSchoolPolicy Set ";
            List<string> listToSave = new List<string>(new string[] { "parentSignature = @ParentSignature","attendance = @Attendance", "tobacco = @Tobacco", "internetAccess = @InternetAccess", "studentInsurance = @StudentInsurance", "fieldTrips = @FieldTrips", "drugTesting = @DrugTesting", "noticeOfDisclosures = @NoticeOfDisclosures", "cellPhoneContact = @CellPhoneContact", "releaseForPhotography = @ReleaseForPhotography", "studentSignature = @signature" });

            List<string> toRemove = pCheck.IsValid;

            foreach (var v in toRemove)
            {
                for (int i = 0; i < listToSave.Count; i++)
                {
                    if (listToSave[i].Contains(v))
                    {
                        listToSave.RemoveAt(i);
                        i--;
                    }
                }
            }

            foreach (var s in listToSave)
            {
                if (listToSave.IndexOf(s) != listToSave.Count - 1)
                {
                    query += s + ",";
                }

                else
                {
                    query += s;
                }
            }

            query += " Where Id = @id";

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                connection.Execute(query,
                    new { ParentSignature = hsp.parentSignature, signature = hsp.studentSignature, Attendance = hsp.attendance, Tobacco = hsp.tobacco, InternetAccess = hsp.internetAccess, StudentInsurance = hsp.studentInsurance, FieldTrips = hsp.fieldTrips, DrugTesting = hsp.drugTesting, NoticeOfDisclosures = hsp.noticeOfDisclosures, CellPhoneContact = hsp.cellPhoneContact, ReleaseForPhotography = hsp.releaseForPhotography, id = hsp.Id });
            }
        }

        #endregion

        #region Methods set the datacontext for datagrids in admin window

        public List<AdultBasicInfoClass> AdultDGSearch()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                 return connection.Query<AdultBasicInfoClass>($"Select * from AdultBasicInfo where lastName != 'null' AND firstName != 'null' Order By lastName").ToList();
            }
        }

        public List<AdultBasicInfoClass> AdultDGSearch(string search)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                return connection.Query<AdultBasicInfoClass>($"Select * from AdultBasicInfo where firstName Like '%" + search + "%' OR lastName Like '%" + search + "%' Order By lastName").ToList();
            }
        }

        public List<AdultBasicInfoClass> AdultDGSearch(string first, string second)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                return connection.Query<AdultBasicInfoClass>($"Select * from AdultBasicInfo where (firstName Like '%" + first + "%' AND lastName Like '%" + second + "%') OR (firstName Like '%" + second + "%' AND lastName Like '%" + first + "%') Order By lastName").ToList();
            }
        }

        public List<HighSchoolBasicInfoClass> HSDGSearch()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                return connection.Query<HighSchoolBasicInfoClass>($"Select * from HighSchoolBasicInfo where lastName != 'null' AND firstName != 'null' Order By lastName").ToList();
            }
        }

        public List<HighSchoolBasicInfoClass> HSDGSearch(string search)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                return connection.Query<HighSchoolBasicInfoClass>($"Select * from HighSchoolBasicInfo where firstName Like '%" + search + "%' OR lastName Like '%" + search + "%' Order By lastName").ToList();
            }
        }

        public List<HighSchoolBasicInfoClass> HSDGSearch(string first, string second)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
            {
                return connection.Query<HighSchoolBasicInfoClass>($"Select * from HighSchoolBasicInfo where (firstName Like '%" + first + "%' AND lastName Like '%" + second + "%') OR (firstName Like '%" + second + "%' AND lastName Like '%" + first + "%') Order By lastName").ToList();
            }
        }

        #endregion
    }
}
