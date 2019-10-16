using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Enrollment_Application
{
    public partial class AdminStudentInformationWindowA : Window
    {

        // declare all variables to be used

        #region Variables
        int Id;

        public static bool overwrite = false;

        AdultBasicInfoTextValidation abiCheck = new AdultBasicInfoTextValidation();
        AdultECUCTextValidation aecCheck = new AdultECUCTextValidation();
        HealthInfoTextValidation hiCheck = new HealthInfoTextValidation();
        PolicyTextValidation pCheck = new PolicyTextValidation();

        AdultBasicInfoClass abi;
        AdultEmergencyContactClass aec;
        AdultHealthInfoClass ahi;
        AdultPolicyClass ap;
        AdultConfidentialInfoClass aci;
        #endregion

        #region Constructor that takes the students ID number and assigns it to a local variable for later use --- also calls the Load() method to initialize data contexts and autofill information

        public AdminStudentInformationWindowA(int studentId)
        {
            InitializeComponent();

            Id = studentId;

            Load();
        }

        private void Load()
        {
            DataAccess db = new DataAccess();

            abi = db.GetABI(Id);
            aec = db.GetAEC(Id);
            ahi = db.GetAHI(Id);
            ap = db.GetAP(Id);
            aci = db.GetACI(Id);

            BasicInfoField.DataContext = abi;
            EmergencyContactField.DataContext = aec;
            HealthInformationField.DataContext = ahi;
            StudentPolicyField.DataContext = ap;
            ConfidentialInfoField.DataContext = aci;

            if (ahi.healthSignature != null)
            {
                using (MemoryStream ms = new MemoryStream(ahi.healthSignature))
                {
                    HISignatureCanvas.Strokes = new System.Windows.Ink.StrokeCollection(ms);
                    ms.Close();
                }
            }

            if (ap.studentSignature != null)
            {
                using (MemoryStream ms = new MemoryStream(ap.studentSignature))
                {
                    policySignatureCanvas.Strokes = new System.Windows.Ink.StrokeCollection(ms);
                    ms.Close();
                }
            }
        }

        #endregion

        #region Drag Method
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion

        #region Code executes when SubmitBtn is clicked
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {

            // update BasicInfo variables to be current
            abi.lastName = aLastName.Text.Trim();
            abi.firstName = aFirstName.Text.Trim();
            abi.middleInitial = aMiddleInitial.Text.Trim();
            abi.program = aProgram.Text.Trim();
            abi.streetAddress = aStudentStreetAddress.Text.Trim();
            abi.city = aStudentCity.Text.Trim();
            abi.state = aStudentState.Text;
            abi.zipCode = aStudentZIP.Text.Trim();
            abi.primaryPhoneNum = aStudentPrimaryPhone.Text.Trim();
            abi.cellPhoneNum = aStudentCellPhone.Text.Trim();
            abi.hispanicOrLatino = aStudentLatino.Text;
            abi.race = aStudentRace.Text;
            abi.gender = aStudentGender.Text;
            abi.dateOfBirth = aStudentDOB.SelectedDate;
            abi.SSN = abi.SSN;
            abi.completedEdLevel = aEducationLevel.Text;
            abi.attendedCollegeOrTech = attendedCollegeCombo.Text;
            abi.liveWithParent = liveWithParentCombo.Text;

            // update values of validCheck
            abiCheck.UpdateValues(abi);

            // update the datacontext to be validCheck if it was not already
            if (BasicInfoField.DataContext != abiCheck)
            {
                BasicInfoField.DataContext = abiCheck;
            }

            // update hsec to contain current information
            aec.contactName = ECName.Text.Trim();
            aec.relationship = ECRelationship.Text.Trim();
            aec.primaryNum = ECPrimaryNum.Text.Trim();
            aec.alternateNum = ECCellNum.Text.Trim();
            aec.nameNearestRelative = NRName.Text.Trim();
            aec.NRrelationship = NRRelationship.Text.Trim();
            aec.NRstreetAddress = NRStreetAddress.Text.Trim();
            aec.NRcity = NRcity.Text.Trim();
            aec.NRstate = NRstate.Text;
            aec.NRzip = NRzip.Text.Trim();
            aec.NRprimaryNum = NRPrimaryNumber.Text.Trim();
            aec.NRcellNum = NRAlternateNumber.Text.Trim();

            // update the validCheck variable

            aecCheck.UpdateValues(aec);

            // update the datacontext to be hsecCheck if it was not already
            if (EmergencyContactField.DataContext != aecCheck)
            {
                EmergencyContactField.DataContext = aecCheck;
            }

            // assign stroke values to new byte array in order to later save the information to the database
            byte[] signature;
            using (MemoryStream ms = new MemoryStream())
            {
                HISignatureCanvas.Strokes.Save(ms);
                signature = ms.ToArray();
            }

            ahi.primaryPhysician = primaryPhysicianText.Text.Trim();
            ahi.otherPhysician = oPhysicianText.Text.Trim();
            ahi.pPhysicianPhoneNum = primaryPhysPhoneText.Text.Trim();
            ahi.oPhysicianPhoneNum = oPhysicianPhoneNum.Text.Trim();
            ahi.diabeticType = diabetesCombo.Text;
            ahi.allergies = allergiesText.Text.Trim();
            ahi.heartIssues = heartConditionsText.Text.Trim();
            ahi.metabolic = metabolicCheck.IsChecked.ToString();
            ahi.jointMuscle = jointMuscleCheck.IsChecked.ToString();
            ahi.chronicIllness = chronicIllnessCheck.IsChecked.ToString();
            ahi.migraines = migrainesCheck.IsChecked.ToString();
            ahi.neurological = neurologicalCheck.IsChecked.ToString();
            ahi.pulmonary = pulmonaryCheck.IsChecked.ToString();
            ahi.asthma = asthmaCheck.IsChecked.ToString();
            ahi.other = otherCheck.IsChecked.ToString();
            ahi.otherMeds = otherMedsText.Text.Trim();
            ahi.specificFirstAidNeeds = specificNeedsText.Text.Trim();
            ahi.repPermissionForTreatment = treatmentPermissionCombo.Text;
            ahi.healthSignature = signature;

            hiCheck.UpdateValues(ahi);

            // update the datacontext to be hiCheck if it was not already
            if (HealthInformationField.DataContext != hiCheck)
            {
                HealthInformationField.DataContext = hiCheck;
            }

            byte[] policySig;
            using (MemoryStream ms = new MemoryStream())
            {
                policySignatureCanvas.Strokes.Save(ms);
                policySig = ms.ToArray();
            }

            // update hsp information to current values
            ap.attendance = attendanceCheck.IsChecked.ToString();
            ap.tobacco = tobaccoCheck.IsChecked.ToString();
            ap.internetAccess = internetCheck.IsChecked.ToString();
            ap.studentInsurance = insuranceCheck.IsChecked.ToString();
            ap.fieldTrips = fieldTripsCheck.IsChecked.ToString();
            ap.drugTesting = drugTestingCheck.IsChecked.ToString();
            ap.noticeOfDisclosures = noticeOfDisclosuresCheck.IsChecked.ToString();
            ap.cellPhoneContact = cellPhoneContactCheck.IsChecked.ToString();
            ap.releaseForPhotography = photographyReleaseCheck.IsChecked.ToString();
            ap.studentSignature = policySig;

            pCheck.UpdateValues(ap);

            // update the datacontext to be pCheck if it was not already
            if (StudentPolicyField.DataContext != pCheck)
            {
                StudentPolicyField.DataContext = pCheck;
            }

            // update ConfidentialInfo object to contain current values
            aci.foodStamps = foodStampsCheck.IsChecked.ToString();
            aci.dependentChildrenAid = dependentChildrenCheck.IsChecked.ToString();
            aci.supplementaryIncome = SSIcheck.IsChecked.ToString();
            aci.housingAssistance = section8Check.IsChecked.ToString();
            aci.none = noneCheck.IsChecked.ToString();
            aci.homeless = homelessCheck.IsChecked.ToString();
            aci.agedOutFosterCare = agedOutCheck.IsChecked.ToString();
            aci.outOfWorkforce = workforceCheck.IsChecked.ToString();

            DataAccess db = new DataAccess();

            OverwriteConfirmation ow = new OverwriteConfirmation("Some of the information for this student may have been changed. Are you sure you want to overwrite the data for this user? The information may not be recovered.");

            ow.ShowDialog();

            if (overwrite == true)
            {
                db.SaveABI(abi);
                db.SaveAEC(aec);
                db.SaveAHI(ahi);
                db.SaveAP(ap);
                db.SaveACI(aci);

                this.Close();
            }
        }
        #endregion

        #region Code executes when CloseBtn is clicked
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}