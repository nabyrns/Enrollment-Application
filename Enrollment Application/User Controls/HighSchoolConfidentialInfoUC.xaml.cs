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
    /// Interaction logic for HighSchoolConfidentialInfoUC.xaml
    /// </summary>
    public partial class HighSchoolConfidentialInfoUC : UserControl
    {
        // declare variables that will be used
        EnrollmentDBEntities _db = new EnrollmentDBEntities();

        HighSchoolConfidentialInfo hsci;

        public HighSchoolConfidentialInfoUC()
        {
            InitializeComponent();

            // initialize AdultEmergencyContact variable to contain matching information pulled from the database
            hsci = (from m in _db.HighSchoolConfidentialInfoes where m.Id == LoginPage.highschoolCheck.Id select m).FirstOrDefault();

            if (hsci.parentSignature != null)
            {
                using (MemoryStream ms = new MemoryStream(hsci.parentSignature))
                {
                    signatureCanvas.Strokes = new System.Windows.Ink.StrokeCollection(ms);
                    ms.Close();
                }
            }

            textFields.DataContext = hsci;

            sigCanBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(33, 148, 243));
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            bool sigError = false;

            

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

            hsci.foodStamps = foodStampsCheck.IsChecked.ToString();
            hsci.dependentChildrenAid = dependentChildrenCheck.IsChecked.ToString();
            hsci.supplementaryIncome = SSIcheck.IsChecked.ToString();
            hsci.housingAssistance = section8Check.IsChecked.ToString();
            hsci.none = noneCheck.IsChecked.ToString();
            hsci.homeless = homelessCheck.IsChecked.ToString();
            hsci.agedOutFosterCare = agedOutCheck.IsChecked.ToString();
            hsci.outOfWorkforce = workforceCheck.IsChecked.ToString();
            hsci.reducedLunch = reducedLunchCheck.IsChecked.ToString();
            hsci.parentSignature = signature;

            if (hsci.foodStamps == "False" && hsci.dependentChildrenAid == "False" && hsci.supplementaryIncome == "False" && hsci.housingAssistance == "False" && hsci.none == "False")
            {
                ErrorMessage error = new ErrorMessage("Please select all that apply to your family, if none apply --- select \'None of these apply to me or my family\'");

                error.ShowDialog();

                return;
            }

            if ((hsci.foodStamps == "True" || hsci.dependentChildrenAid == "True" || hsci.supplementaryIncome == "True" || hsci.housingAssistance == "True") && hsci.none == "True")
            {
                ErrorMessage error = new ErrorMessage("You cannot select \'None apply\' and also check other boxes.");

                error.ShowDialog();

                return;
            }

            if (!sigError)
            {
                HighSchoolLogin hsl = (from m in _db.HighSchoolLogins where m.Id == LoginPage.highschoolCheck.Id select m).FirstOrDefault();

                hsl.submitted = "Yes";

                _db.SaveChanges();

                Information_Page.hsciuc.Visibility = Visibility.Hidden;

                Information_Page.SuccessMessageAndShutdown();
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Information_Page.hsciuc.Visibility = Visibility.Hidden;

            Information_Page.selectedIndex = 3;

            Information_Page.lv.SelectedIndex = 3;

            Information_Page.hsspuc.Visibility = Visibility.Visible;
        }

        private void SigClear_Click(object sender, RoutedEventArgs e)
        {
            signatureCanvas.Strokes.Clear();
        }
    }
}
