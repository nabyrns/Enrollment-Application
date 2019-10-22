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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Enrollment_Application
{
    public partial class HealthInfoUC : UserControl
    {

        // declare variables that may be used depending on which type of student the login was for
        HighSchoolHealthInfoClass hshi;
        AdultHealthInfoClass ahi;

        // create new validation object
        HealthInfoTextValidation validCheck = new HealthInfoTextValidation();

        #region Constructor --- initializes variables based on student type of login and assigns datacontext for the textfields in this UC to whichever variable is used
        public HealthInfoUC()
        {
            InitializeComponent();

            // DataAccess variable to be used to retrieve and autofill information stored in the database for the user
            DataAccess db = new DataAccess();

            // initialize previously declared values to null for later checking
            hshi = null;
            ahi = null;

            // check what type of student logged in
            // then query database and pull student info, assigning values to the proper HealthInfo object
            // then assign the datacontext for the text fields in the UC to the newly reassigned variable
            if (LoginPage.highschoolCheck != null)
            {

                hshi = db.GetHSHI(LoginPage.highschoolCheck.Id);


                if (hshi.healthSignature != null)
                {
                    using (MemoryStream ms = new MemoryStream(hshi.healthSignature))
                    {
                        signatureCanvas.Strokes = new System.Windows.Ink.StrokeCollection(ms);
                        ms.Close();
                    }
                }


                textFields.DataContext = hshi;

                siglabel.Text = "Parent/Guardian Signature: ";
            }

            else
            {

                ahi = db.GetAHI(LoginPage.adultCheck.Id);

                if (ahi.healthSignature != null)
                {
                    using (MemoryStream ms = new MemoryStream(ahi.healthSignature))
                    {
                        signatureCanvas.Strokes = new System.Windows.Ink.StrokeCollection(ms);
                        ms.Close();
                    }
                }

                textFields.DataContext = ahi;

                siglabel.Text = "Student Signature: ";
            }

            // set signature border to base color
            sigCanBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(33, 148, 243));
        }
        #endregion

        #region Code executes when NextBtn is clicked
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // variable that will check whether the signature field contains an error
            bool sigError = false;

            // if the signature field is empty, sigError is set to true and the border color changes to the error color
            if (signatureCanvas.Strokes.Count == 0)
            {
                sigCanBorder.BorderBrush = Brushes.Red;
                sigError = true;
            }

            // if the signature field is not empty and the box was the error color, change back to base color and set sigError to false
            else
            {
                if (sigCanBorder.BorderBrush == Brushes.Red)
                {
                    sigCanBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(33, 148, 243));
                }

                sigError = false;
            }

            // assign stroke values to new byte array in order to later save the information to the database
            byte[] signature;
            using (MemoryStream ms = new MemoryStream())
            {
                signatureCanvas.Strokes.Save(ms);
                signature = ms.ToArray();
            }

            // check what kind of student was logged in
            if (ahi != null)
            {
                // reassign the values of the HealthInfo object to be current
                ahi.primaryPhysician = primaryPhysText.Text.Trim();
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

                // update validCheck to contain the newly updated values

                validCheck.UpdateValues(ahi);
            }

            // same as above code
            else
            {
                hshi.primaryPhysician = primaryPhysText.Text.Trim();
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

                validCheck.UpdateValues(hshi);
            }

            // update the datacontext to be validCheck if it was not already
            if (textFields.DataContext != validCheck)
            {
                textFields.DataContext = validCheck;
            }

            // if no errors are found, save changes to database and change visibility of UC to hidden
            // also change selected index for Information_Page --- this is what controls the moving cursor grid on that page

            if (LoginPage.adultCheck != null)
            {
                DataAccess db = new DataAccess();

                db.SaveAHI(ahi, validCheck);
            }

            else
            {
                DataAccess db = new DataAccess();

                db.SaveHSHI(hshi, validCheck);
            }

            if (validCheck.IsValid.Count == 0 && !sigError)
            {

                Information_Page.hiuc.Visibility = Visibility.Hidden;

                if (LoginPage.adultCheck != null)
                {

                    Information_Page.aecuc.Visibility = Visibility.Visible;
                }

                else
                {

                    Information_Page.hsecuc.Visibility = Visibility.Visible;
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

            else
            {
                Information_Page.hsbiuc.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region Code exectues when SigClear is clicked
        private void SigClear_Click(object sender, RoutedEventArgs e)
        {
            signatureCanvas.Strokes.Clear();
        }
        #endregion
    }
}
