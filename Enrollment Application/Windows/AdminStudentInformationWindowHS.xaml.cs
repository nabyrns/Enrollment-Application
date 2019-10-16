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
    public partial class AdminStudentInformationWindowHS : Window
    {

        // declare all variables to be used

        #region Variables
        int Id;

        public static bool overwrite = false;

        HighSchoolBasicInfoTextValidation hsbiCheck = new HighSchoolBasicInfoTextValidation();
        HighSchoolECUCTextValidation hsecCheck = new HighSchoolECUCTextValidation();
        HealthInfoTextValidation hiCheck = new HealthInfoTextValidation();
        PolicyTextValidation pCheck = new PolicyTextValidation();

        HighSchoolBasicInfoClass hsbi;
        HighSchoolEmergencyContactClass hsec;
        HighSchoolHealthInfoClass hshi;
        HighSchoolPolicyClass hsp;
        HighSchoolConfidentialInfoClass hsci;
        #endregion

        #region Constructor that takes the students ID number and assigns it to a local variable for later use --- also calls the Load() method to initialize data contexts and autofill information

        public AdminStudentInformationWindowHS(int studentId)
        {
            InitializeComponent();

            Id = studentId;

            Load();
        }

        private void Load()
        {
            DataAccess db = new DataAccess();

            hsbi = db.GetHSBI(Id);
            hsec = db.GetHSEC(Id);
            hshi = db.GetHSHI(Id);
            hsp = db.GetHSP(Id);
            hsci = db.GetHSCI(Id);

            BasicInfoField.DataContext = hsbi;
            EmergencyContactField.DataContext = hsec;
            HealthInformationField.DataContext = hshi;
            StudentPolicyField.DataContext = hsp;
            ConfidentialInfoField.DataContext = hsci;

            if (hshi.healthSignature != null)
            {
                using (MemoryStream ms = new MemoryStream(hshi.healthSignature))
                {
                    HISignatureCanvas.Strokes = new System.Windows.Ink.StrokeCollection(ms);
                    ms.Close();
                }
            }

            if (hsp.parentSignature != null)
            {
                using (MemoryStream ms = new MemoryStream(hsp.parentSignature))
                {
                    parentSigPolicyCanvas.Strokes = new System.Windows.Ink.StrokeCollection(ms);
                    ms.Close();
                }
            }

            if (hsp.studentSignature != null)
            {
                using (MemoryStream ms = new MemoryStream(hsp.studentSignature))
                {
                    policySignatureCanvas.Strokes = new System.Windows.Ink.StrokeCollection(ms);
                    ms.Close();
                }
            }

            if (hsci.parentSignature != null)
            {
                using (MemoryStream ms = new MemoryStream(hsci.parentSignature))
                {
                    parentSigCanvas.Strokes = new System.Windows.Ink.StrokeCollection(ms);
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
            hsbi.lastName = aLastName.Text.Trim();
            hsbi.firstName = aFirstName.Text.Trim();
            hsbi.middleInitial = aMiddleInitial.Text.Trim();
            hsbi.program = aProgram.Text.Trim();
            hsbi.streetAddress = aStudentStreetAddress.Text.Trim();
            hsbi.city = aStudentCity.Text.Trim();
            hsbi.state = aStudentState.Text;
            hsbi.zipCode = aStudentZIP.Text.Trim();
            hsbi.primaryPhoneNum = aStudentPrimaryPhone.Text.Trim();
            hsbi.cellPhoneNum = aStudentCellPhone.Text.Trim();
            hsbi.hispanicOrLatino = aStudentLatino.Text;
            hsbi.race = aStudentRace.Text;
            hsbi.gender = aStudentGender.Text;
            hsbi.dateOfBirth = aStudentDOB.SelectedDate;
            hsbi.sendingHS = aSendingHighSchool.Text.Trim();
            hsbi.currentEdLevel = aEducationLevel.Text;

            // update values of validCheck
            hsbiCheck.UpdateValues(hsbi);

            // update the datacontext to be validCheck if it was not already
            if (BasicInfoField.DataContext != hsbiCheck)
            {
                BasicInfoField.DataContext = hsbiCheck;
            }

            // update hsec to contain current information
            hsec.parentNameOne = a1ParentName.Text.Trim();
            hsec.parentOneRelationship = a1ParentRelationship.Text.Trim();
            hsec.parentOneAddress = a1ParentAddress.Text.Trim();
            hsec.parentOneCity = a1ParentCity.Text.Trim();
            hsec.parentOneState = a1ParentState.Text;
            hsec.parentOneZip = a1ParentZIP.Text.Trim();
            hsec.parentOnePrimaryNum = a1ParentPrimaryNumber.Text.Trim();
            hsec.parentOneCellNum = a1ParentCellNumber.Text.Trim();
            hsec.parentOneEmail = a1ParentEmail.Text.Trim();
            hsec.residesWithP1 = a1ParentResiding.IsChecked.ToString();
            hsec.parentNameTwo = a2ParentName.Text.Trim();
            hsec.parentTwoRelationship = a2ParentRelationship.Text.Trim();
            hsec.parentTwoAddress = a2ParentAddress.Text.Trim();
            hsec.parentTwoCity = a2ParentCity.Text.Trim();
            hsec.parentTwoState = a2ParentState.Text;
            hsec.parentTwoZip = a2ParentZIP.Text.Trim();
            hsec.parentTwoPrimaryNum = a2ParentPrimaryNumber.Text.Trim();
            hsec.parentTwoCellNum = a2ParentCellNumber.Text.Trim();
            hsec.parentTwoEmail = a2ParentEmail.Text.Trim();
            hsec.residesWithP2 = a2ParentResiding.IsChecked.ToString();
            hsec.EContactName = ECName.Text.Trim();
            hsec.EContactRelationship = ECRelationship.Text.Trim();
            hsec.EContactPrimaryNum = ECPrimaryNum.Text.Trim();
            hsec.EContactCellNum = ECCellNum.Text.Trim();


            // update the validCheck variable
            hsecCheck.UpdateValues(hsec);

            // update the datacontext to be hsecCheck if it was not already
            if (EmergencyContactField.DataContext != hsecCheck)
            {
                EmergencyContactField.DataContext = hsecCheck;
            }

            // assign stroke values to new byte array in order to later save the information to the database
            byte[] signature;
            using (MemoryStream ms = new MemoryStream())
            {
                HISignatureCanvas.Strokes.Save(ms);
                signature = ms.ToArray();
            }

            hshi.primaryPhysician = primaryPhysicianText.Text.Trim();
            hshi.otherPhysician = oPhysicianText.Text.Trim();
            hshi.pPhysicianPhoneNum = primaryPhysPhoneText.Text.Trim();
            hshi.oPhysicianPhoneNum = oPhysicianPhoneNum.Text.Trim();
            hshi.diabeticType = diabetesCombo.Text;
            hshi.allergies = allergiesText.Text.Trim();
            hshi.heartIssues = heartConditionsText.Text.Trim();
            hshi.metabolic = metabolicCheck.IsChecked.ToString();
            hshi.jointMuscle = jointMuscleCheck.IsChecked.ToString();
            hshi.chronicIllness = chronicIllnessCheck.IsChecked.ToString();
            hshi.migraines = migrainesCheck.IsChecked.ToString();
            hshi.neurological = neurologicalCheck.IsChecked.ToString();
            hshi.pulmonary = pulmonaryCheck.IsChecked.ToString();
            hshi.asthma = asthmaCheck.IsChecked.ToString();
            hshi.other = otherCheck.IsChecked.ToString();
            hshi.otherMeds = otherMedsText.Text.Trim();
            hshi.specificFirstAidNeeds = specificNeedsText.Text.Trim();
            hshi.repPermissionForTreatment = treatmentPermissionCombo.Text;
            hshi.healthSignature = signature;

            hiCheck.UpdateValues(hshi);

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

            byte[] policyParentSig;
            using (MemoryStream ms = new MemoryStream())
            {
                parentSigPolicyCanvas.Strokes.Save(ms);
                policyParentSig = ms.ToArray();
            }

            // update hsp information to current values
            hsp.attendance = attendanceCheck.IsChecked.ToString();
            hsp.tobacco = tobaccoCheck.IsChecked.ToString();
            hsp.internetAccess = internetCheck.IsChecked.ToString();
            hsp.studentInsurance = insuranceCheck.IsChecked.ToString();
            hsp.fieldTrips = fieldTripsCheck.IsChecked.ToString();
            hsp.drugTesting = drugTestingCheck.IsChecked.ToString();
            hsp.noticeOfDisclosures = noticeOfDisclosuresCheck.IsChecked.ToString();
            hsp.cellPhoneContact = cellPhoneContactCheck.IsChecked.ToString();
            hsp.releaseForPhotography = photographyReleaseCheck.IsChecked.ToString();
            hsp.studentSignature = policySig;
            hsp.parentSignature = policyParentSig;

            pCheck.UpdateValues(hsp);

            // update the datacontext to be pCheck if it was not already
            if (StudentPolicyField.DataContext != pCheck)
            {
                StudentPolicyField.DataContext = pCheck;
            }

            // update ConfidentialInfo object to contain current values
            hsci.foodStamps = foodStampsCheck.IsChecked.ToString();
            hsci.dependentChildrenAid = dependentChildrenCheck.IsChecked.ToString();
            hsci.supplementaryIncome = SSIcheck.IsChecked.ToString();
            hsci.housingAssistance = section8Check.IsChecked.ToString();
            hsci.none = noneCheck.IsChecked.ToString();
            hsci.homeless = homelessCheck.IsChecked.ToString();
            hsci.agedOutFosterCare = agedOutCheck.IsChecked.ToString();
            hsci.parentsActiveDuty = activeDutyCheck.IsChecked.ToString();
            hsci.reducedLunch = reducedLunchCheck.IsChecked.ToString();

            DataAccess db = new DataAccess();

            OverwriteConfirmation ow = new OverwriteConfirmation("Some of the information for this student may have been changed. Are you sure you want to overwrite the data for this user? The information may not be recovered.");

            ow.ShowDialog();

            if (overwrite == true)
            {
                db.SaveHSBI(hsbi);
                db.SaveHSEC(hsec);
                db.SaveHSHI(hshi);
                db.SaveHSP(hsp);
                db.SaveHSCI(hsci);

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
