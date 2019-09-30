using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Enrollment_Application
{
    public partial class HealthInfoUC : UserControl
    {
        // create new database connection
        EnrollmentDBEntities _db = new EnrollmentDBEntities();

        // declare variables that may be used depending on which type of student the login was for
        HighSchoolHealthInfo hshi;
        AdultHealthInfo ahi;

        // create new validation object
        HealthInfoTextValidation validCheck;

        #region Constructor --- initializes variables based on student type of login and assigns datacontext for the textfields in this UC to whichever variable is used
        public HealthInfoUC()
        {
            InitializeComponent();

            // initialize previously declared values to null for later checking
            hshi = null;
            ahi = null;

            // check what type of student logged in
            // then query database and pull student info, assigning values to the proper HealthInfo object
            // then assign the datacontext for the text fields in the UC to the newly reassigned variable
            if (LoginPage.highschoolCheck != null)
            {
                hshi = (from m in _db.HighSchoolHealthInfoes where m.Id == LoginPage.highschoolCheck.Id select m).FirstOrDefault();


                textFields.DataContext = hshi;
            }

            else
            {
                ahi = (from m in _db.AdultHealthInfoes where m.Id == LoginPage.adultCheck.Id select m).FirstOrDefault();

                textFields.DataContext = ahi;
            }
        }
        #endregion

        #region Code executes when NextBtn is clicked
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {

            // check what kind of student was logged in
            if (ahi != null)
            {
                // reassign the values of the HealthInfo object to be what is in the control for that variable
                ahi.primaryPhysician = primaryPhysText.Text;
                ahi.otherPhysician = oPhysicianText.Text;
                ahi.pPhysicianPhoneNum = primaryPhysPhoneText.Text;
                ahi.oPhysicianPhoneNum = oPhysicianPhoneNum.Text;
                ahi.diabeticType = diabetesCombo.Text;
                ahi.allergies = allergiesText.Text;
                ahi.heartIssues = heartConditionsText.Text;
                ahi.metabolic = metabolicCheck.IsChecked.ToString();
                ahi.jointMuscle = jointMuscleCheck.IsChecked.ToString();
                ahi.chronicIllness = chronicIllnessCheck.IsChecked.ToString();
                ahi.migraines = migrainesCheck.IsChecked.ToString();
                ahi.neurological = neurologicalCheck.IsChecked.ToString();
                ahi.pulmonary = pulmonaryCheck.IsChecked.ToString();
                ahi.asthma = asthmaCheck.IsChecked.ToString();
                ahi.other = otherCheck.IsChecked.ToString();
                ahi.otherMeds = otherMedsText.Text;
                ahi.specificFirstAidNeeds = specificNeedsText.Text;
                ahi.repPermissionForTreatment = treatmentPermissionCombo.Text;

                // initialize or update validCheck to contain the newly updated values in the UC being used
                // the connection between validCheck and the UC being used is what allows for saving to the database
                validCheck = new HealthInfoTextValidation()
                {
                    _primaryPhysician = ahi.primaryPhysician,
                    _otherPhysician = ahi.otherPhysician,
                    _pPhysicianPhoneNum = ahi.pPhysicianPhoneNum,
                    _oPhysicianPhoneNum = ahi.oPhysicianPhoneNum,
                    _diabeticType = ahi.diabeticType,
                    _allergies = ahi.allergies,
                    _heartIssues = ahi.heartIssues,
                    _metabolic = bool.Parse(ahi.metabolic),
                    _jointMuscle = bool.Parse(ahi.jointMuscle),
                    _chronicIllness = bool.Parse(ahi.chronicIllness),
                    _migraines = bool.Parse(ahi.migraines),
                    _neurological = bool.Parse(ahi.neurological),
                    _pulmonary = bool.Parse(ahi.pulmonary),
                    _asthma = bool.Parse(ahi.asthma),
                    _other = bool.Parse(ahi.other),
                    _otherMeds = ahi.otherMeds,
                    _specificFirstAidNeeds = ahi.specificFirstAidNeeds,
                    _repPermissionForTreatment = ahi.repPermissionForTreatment
                };
            }

            // same as above code
            else
            {
                hshi.primaryPhysician = primaryPhysText.Text;
                hshi.otherPhysician = oPhysicianText.Text;
                hshi.pPhysicianPhoneNum = primaryPhysPhoneText.Text;
                hshi.oPhysicianPhoneNum = oPhysicianPhoneNum.Text;
                hshi.diabeticType = diabetesCombo.Text;
                hshi.allergies = allergiesText.Text;
                hshi.heartIssues = heartConditionsText.Text;
                hshi.metabolic = metabolicCheck.IsChecked.ToString();
                hshi.jointMuscle = jointMuscleCheck.IsChecked.ToString();
                hshi.chronicIllness = chronicIllnessCheck.IsChecked.ToString();
                hshi.migraines = migrainesCheck.IsChecked.ToString();
                hshi.neurological = neurologicalCheck.IsChecked.ToString();
                hshi.pulmonary = pulmonaryCheck.IsChecked.ToString();
                hshi.asthma = asthmaCheck.IsChecked.ToString();
                hshi.other = otherCheck.IsChecked.ToString();
                hshi.otherMeds = otherMedsText.Text;
                hshi.specificFirstAidNeeds = specificNeedsText.Text;
                hshi.repPermissionForTreatment = treatmentPermissionCombo.Text;

                validCheck = new HealthInfoTextValidation()
                {
                    _primaryPhysician = hshi.primaryPhysician,
                    _otherPhysician = hshi.otherPhysician,
                    _pPhysicianPhoneNum = hshi.pPhysicianPhoneNum,
                    _oPhysicianPhoneNum = hshi.oPhysicianPhoneNum,
                    _diabeticType = hshi.diabeticType,
                    _allergies = hshi.allergies,
                    _heartIssues = hshi.heartIssues,
                    _metabolic = bool.Parse(hshi.metabolic),
                    _jointMuscle = bool.Parse(hshi.jointMuscle),
                    _chronicIllness = bool.Parse(hshi.chronicIllness),
                    _migraines = bool.Parse(hshi.migraines),
                    _neurological = bool.Parse(hshi.neurological),
                    _pulmonary = bool.Parse(hshi.pulmonary),
                    _asthma = bool.Parse(hshi.asthma),
                    _other = bool.Parse(hshi.other),
                    _otherMeds = hshi.otherMeds,
                    _specificFirstAidNeeds = hshi.specificFirstAidNeeds,
                    _repPermissionForTreatment = hshi.repPermissionForTreatment
                };
            }

            // update the datacontext to be validCheck if it was not already
            if (textFields.DataContext != validCheck)
            {
                textFields.DataContext = validCheck;
            }

            // if no errors are found, save changes to database and change visibility of UC to hidden
            // also change selected index for Information_Page --- this is what controls the moving cursor grid on that page
            if (validCheck.IsValid)
            {
                _db.SaveChanges();

                Information_Page.hiuc.Visibility = Visibility.Hidden;

                if (LoginPage.adultCheck != null)
                {
                    Information_Page.aecuc.Visibility = Visibility.Visible;
                }

                Information_Page.selectedIndex = 2;

                Information_Page.lv.SelectedIndex = 2;
            }
        }
        #endregion

        #region Code executes when BackBtn is clicked
        // changes UC visibility in Information_Page
        // also changes the selected index of the listview to match the UC that is being displayed
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Information_Page.hiuc.Visibility = Visibility.Hidden;

            Information_Page.selectedIndex = 0;

            Information_Page.lv.SelectedIndex = 0;

            if (LoginPage.adultCheck != null)
            {
                Information_Page.abiuc.Visibility = Visibility.Visible;
            }
        }
        #endregion
    }
}
