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
    public partial class AdultStudentPolicyUC : UserControl
    {
        // declare variables that will be used

        AdultPolicyClass ap;

        PolicyTextValidation validCheck = new PolicyTextValidation();

        #region Constructor --- initializes variables and assigns datacontext for textfields in this UC
        public AdultStudentPolicyUC()
        {
            InitializeComponent();

            // Data Access variable to retrieve and autofill stored information for the user
            DataAccess db = new DataAccess();

            ap = db.GetAP(LoginPage.adultCheck.Id);

            // code avoids getting an error on data retrieval if the signature has not been filled out yet
            if (ap.studentSignature != null)
            {
                using (MemoryStream ms = new MemoryStream(ap.studentSignature))
                {
                    signatureCanvas.Strokes = new System.Windows.Ink.StrokeCollection(ms);
                    ms.Close();
                }
            }

            // set the datacontext of the text fields to be the AdultEmergencyContact variable
            textFields.DataContext = ap;

            // set border of signature to be base color
            sigCanBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(33, 148, 243));
        }
        #endregion

        #region Code executes when the BackBtn is clicked
        // changes UC visibility in Information_Page
        // also changes the selected index of the listview to match the UC that is being displayed
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Information_Page.aspuc.Visibility = Visibility.Hidden;

            Information_Page.selectedIndex = 2;

            Information_Page.lv.SelectedIndex = 2;

            Information_Page.aecuc.Visibility = Visibility.Visible;
        }
        #endregion

        #region Code executes when NextBtn is clicked
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // variable to tell whether the signature fields have been filled out
            bool sigError = false;

            #region Checks the signature field, if it is not empty it is saved as a byte array --- also changes signature field border colors based on input
            if (signatureCanvas.Strokes.Count == 0)
            {
                sigCanBorder.BorderBrush = Brushes.Red;
                sigError = true;
            }

            else
            {
                if (sigCanBorder.BorderBrush == Brushes.Red)
                {
                    sigCanBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(33, 148, 243));
                }

                sigError = false;
            }

            byte[] signature;
            using (MemoryStream ms = new MemoryStream())
            {
                signatureCanvas.Strokes.Save(ms);
                signature = ms.ToArray();
            }
            #endregion

            // update ap object information
            ap.attendance = attendanceCheck.IsChecked.ToString();
            ap.tobacco = tobaccoCheck.IsChecked.ToString();
            ap.internetAccess = internetCheck.IsChecked.ToString();
            ap.studentInsurance = insuranceCheck.IsChecked.ToString();
            ap.fieldTrips = fieldTripsCheck.IsChecked.ToString();
            ap.drugTesting = drugTestingCheck.IsChecked.ToString();
            ap.noticeOfDisclosures = noticeOfDisclosuresCheck.IsChecked.ToString();
            ap.cellPhoneContact = cellPhoneContactCheck.IsChecked.ToString();
            ap.releaseForPhotography = photographyReleaseCheck.IsChecked.ToString();
            ap.studentSignature = signature;


            // update the validCheck variable

            validCheck.UpdateValues(ap);

            // update the datacontext to be validCheck if it was not already
            if (textFields.DataContext != validCheck)
            {
                textFields.DataContext = validCheck;
            }

            // DataAccess variable to be used to save information to the database
            DataAccess db = new DataAccess();

            db.SaveAP(ap, validCheck);

            // if no errors are found, save changes to database and change visibility of UC to hidden
            // also change selected index for Information_Page --- this is what controls the moving cursor grid on that page
            if (validCheck.IsValid.Count == 0 && !sigError)
            {

                Information_Page.aspuc.Visibility = Visibility.Hidden;

                Information_Page.aciuc.Visibility = Visibility.Visible;

                Information_Page.selectedIndex = 4;

                Information_Page.lv.SelectedIndex = 4;
            }
        }
        #endregion

        #region Code executes when SigClear is clicked
        private void SigClear_Click(object sender, RoutedEventArgs e)
        {
            signatureCanvas.Strokes.Clear();
        }
        #endregion
    }
}
