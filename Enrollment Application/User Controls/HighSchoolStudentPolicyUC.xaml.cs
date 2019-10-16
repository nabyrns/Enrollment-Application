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
    public partial class HighSchoolStudentPolicyUC : UserControl
    {
        // declare variables that will be used

        HighSchoolPolicyClass hsp;

        PolicyTextValidation validCheck = new PolicyTextValidation();

        #region Constructor --- initializes variables and assigns datacontext for textfields in this UC
        public HighSchoolStudentPolicyUC()
        {
            InitializeComponent();

            // DataAccess variable to retrieve and autofill previously stored information for the user
            DataAccess db = new DataAccess();

            hsp = db.GetHSP(LoginPage.highschoolCheck.Id);

            // if signature fields have not been filled out yet, these code blocks avoid encountering an error
            if (hsp.studentSignature != null)
            {
                using (MemoryStream ms = new MemoryStream(hsp.studentSignature))
                {
                    signatureCanvas.Strokes = new System.Windows.Ink.StrokeCollection(ms);
                    ms.Close();
                }
            }

            if (hsp.parentSignature != null)
            {
                using (MemoryStream ms = new MemoryStream(hsp.parentSignature))
                {
                    parentSignatureCanvas.Strokes = new System.Windows.Ink.StrokeCollection(ms);
                    ms.Close();
                }
            }

            // set the datacontext of the text fields to be the AdultEmergencyContact variable
            textFields.DataContext = hsp;

            // set base colors of signature borders
            sigCanBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(33, 148, 243));
            parSigCanBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(33, 148, 243));
        }
        #endregion

        #region Code executes when the BackBtn is clicked
        // changes UC visibility in Information_Page
        // also changes the selected index of the listview to match the UC that is being displayed
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Information_Page.hsspuc.Visibility = Visibility.Hidden;

            Information_Page.selectedIndex = 2;

            Information_Page.lv.SelectedIndex = 2;

            Information_Page.hsecuc.Visibility = Visibility.Visible;
        }
        #endregion

        #region Code executes when the NextBtn is clicked
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // variable to tell whether the signature fields have been filled out
            bool sigError = false;

            #region Checks the signature fields, if they are not empty they are saved as byte arrays --- also changes signature field border colors based on input
            if (signatureCanvas.Strokes.Count == 0 && parentSignatureCanvas.Strokes.Count == 0)
            {
                sigCanBorder.BorderBrush = Brushes.Red;
                parSigCanBorder.BorderBrush = Brushes.Red;
                sigError = true;
            }

            else if (signatureCanvas.Strokes.Count == 0)
            {
                sigCanBorder.BorderBrush = Brushes.Red;
                parSigCanBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(33, 148, 243));
                sigError = true;
            }

            else if (parentSignatureCanvas.Strokes.Count == 0)
            {
                parSigCanBorder.BorderBrush = Brushes.Red;
                sigCanBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(33, 148, 243));
                sigError = true;
            }

            else
            {
                if (sigCanBorder.BorderBrush == Brushes.Red)
                {
                    sigCanBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(33, 148, 243));
                }

                if (parSigCanBorder.BorderBrush == Brushes.Red)
                {
                    parSigCanBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(33, 148, 243));
                }

                sigError = false;
            }

            byte[] signature;
            using (MemoryStream ms = new MemoryStream())
            {
                signatureCanvas.Strokes.Save(ms);
                signature = ms.ToArray();
            }

            byte[] parentSignature;
            using (MemoryStream ms = new MemoryStream())
            {
                parentSignatureCanvas.Strokes.Save(ms);
                parentSignature = ms.ToArray();
            }
            #endregion

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
            hsp.studentSignature = signature;
            hsp.parentSignature = parentSignature;


            // update the validCheck variable

            validCheck.UpdateValues(hsp);

            // update the datacontext to be validCheck if it was not already
            if (textFields.DataContext != validCheck)
            {
                textFields.DataContext = validCheck;
            }

            // if no errors are found, save changes to database and change visibility of UC to hidden
            // also change selected index for Information_Page --- this is what controls the moving cursor grid on that page
            if (validCheck.IsValid && !sigError)
            {
                // DataAccess variable to save information to DataBase
                DataAccess db = new DataAccess();

                db.SaveHSP(hsp);

                Information_Page.hsspuc.Visibility = Visibility.Hidden;

                Information_Page.hsciuc.Visibility = Visibility.Visible;

                Information_Page.selectedIndex = 4;

                Information_Page.lv.SelectedIndex = 4;
            }
        }
        #endregion

        #region Methods control clearing of Signature Fields
        private void SigClear_Click(object sender, RoutedEventArgs e)
        {
            signatureCanvas.Strokes.Clear();
        }

        private void ParSigClear_Click(object sender, RoutedEventArgs e)
        {
            parentSignatureCanvas.Strokes.Clear();
        }
        #endregion
    }
}
