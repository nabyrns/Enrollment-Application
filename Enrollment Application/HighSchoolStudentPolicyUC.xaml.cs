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
    /// <summary>
    /// Interaction logic for HighSchoolStudentPolicyUC.xaml
    /// </summary>
    public partial class HighSchoolStudentPolicyUC : UserControl
    {
        // declare variables that will be used
        EnrollmentDBEntities _db = new EnrollmentDBEntities();

        HighSchoolPolicy hsp;

        HighSchoolPolicyTextValidation validCheck;

        #region Constructor --- initializes variables and assigns datacontext for textfields in this UC
        public HighSchoolStudentPolicyUC()
        {
            InitializeComponent();

            // initialize AdultEmergencyContact variable to contain matching information pulled from the database
            hsp = (from m in _db.HighSchoolPolicies where m.Id == LoginPage.highschoolCheck.Id select m).FirstOrDefault();

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

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            #region Checks the signature fields, if they are not empty they are saved as byte arrays
           /* if (signatureCanvas.Strokes.Count == 0 || parentSignatureCanvas.Strokes.Count == 0)
            {
                ErrorMessage error = new ErrorMessage("Signature field was not filled in.");

                error.ShowDialog();

                return;
            }*/

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

            // update hsp information to contain what is in the text fields
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


            // initialize or update the validCheck variable, which is a text validation variable
            // the connection between the hsp variable and the validCheck variable is what allows for updating in the database
            validCheck = new HighSchoolPolicyTextValidation()
            {
                _attendance = bool.Parse(hsp.attendance),
                _tobacco = bool.Parse(hsp.tobacco),
                _internetAccess = bool.Parse(hsp.internetAccess),
                _studentInsurance = bool.Parse(hsp.studentInsurance),
                _fieldTrips = bool.Parse(hsp.fieldTrips),
                _drugTesting = bool.Parse(hsp.drugTesting),
                _noticeOfDisclosures = bool.Parse(hsp.noticeOfDisclosures),
                _cellPhoneContact = bool.Parse(hsp.cellPhoneContact),
                _releaseForPhotography = bool.Parse(hsp.releaseForPhotography),
                _studentSignatureStrokes = signatureCanvas.Strokes.Count,
                _parentSignatureStrokes = parentSignatureCanvas.Strokes.Count
            };

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


                Information_Page.hsspuc.Visibility = Visibility.Hidden;



                Information_Page.selectedIndex = 4;

                Information_Page.lv.SelectedIndex = 4;
            }
        }
    }
}
