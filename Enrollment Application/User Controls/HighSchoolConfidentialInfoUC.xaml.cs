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
    public partial class HighSchoolConfidentialInfoUC : UserControl
    {
        // declare variables that will be used

        HighSchoolConfidentialInfoClass hsci;

        #region Constructor
        public HighSchoolConfidentialInfoUC()
        {
            InitializeComponent();

            // DataAccess variable to retrieve and autofill previously saved information for the user
            DataAccess db = new DataAccess();

            hsci = db.GetHSCI(LoginPage.highschoolCheck.Id);

            // code avoids an error when retrieving data to autofill if the signature has not been filled out yet
            if (hsci.parentSignature != null)
            {
                using (MemoryStream ms = new MemoryStream(hsci.parentSignature))
                {
                    signatureCanvas.Strokes = new System.Windows.Ink.StrokeCollection(ms);
                    ms.Close();
                }
            }

            // set datacontext
            textFields.DataContext = hsci;

            // set base color for signature border
            sigCanBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(33, 148, 243));
        }
        #endregion

        #region Code executes when SubmitBtn is clicked
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            // variable that will validate the signature field
            bool sigError = false;

            // if the signature has not been filled out, changes border color and alters value of sigError
            if (signatureCanvas.Strokes.Count == 0)
            {
                sigCanBorder.BorderBrush = Brushes.Red;
                sigError = true;
            }

            // changes signature border to base color if it has been filled out
            else
            {
                if (sigCanBorder.BorderBrush == Brushes.Red)
                {
                    sigCanBorder.BorderBrush = new SolidColorBrush(Color.FromRgb(33, 148, 243));
                }

                // sets sigError to false, as the signature has now been filled out
                sigError = false;
            }

            // byte array to hold the stroke values of the signature field
            byte[] signature;
            using (MemoryStream ms = new MemoryStream())
            {
                signatureCanvas.Strokes.Save(ms);
                signature = ms.ToArray();
            }

            // update ConfidentialInfo object to contain current values
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
            hsci.formCompletionDate = DateTime.Now;

            // if no boxes are selected, display error
            if (hsci.foodStamps == "False" && hsci.dependentChildrenAid == "False" && hsci.supplementaryIncome == "False" && hsci.housingAssistance == "False" && hsci.none == "False")
            {
                ErrorMessage error = new ErrorMessage("Please select all that apply to your family, if none apply --- select \'None of these apply to me or my family\'");

                error.ShowDialog();

                return;
            }

            // if 'None' box is checked, while others are also checked, display error
            if ((hsci.foodStamps == "True" || hsci.dependentChildrenAid == "True" || hsci.supplementaryIncome == "True" || hsci.housingAssistance == "True") && hsci.none == "True")
            {
                ErrorMessage error = new ErrorMessage("You cannot select \'None apply\' and also check other boxes.");

                error.ShowDialog();

                return;
            }

            // if there was no signature error, save
            if (!sigError)
            {
                // DataAccess variable to save information to database
                DataAccess db = new DataAccess();

                db.SaveHSCI(hsci);

                // alter UC visibility
                Information_Page.hsciuc.Visibility = Visibility.Hidden;

                Information_Page.SuccessMessageAndShutdown();
            }
        }
        #endregion

        #region Code executes when BackBtn is clicked
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Information_Page.hsciuc.Visibility = Visibility.Hidden;

            Information_Page.selectedIndex = 3;

            Information_Page.lv.SelectedIndex = 3;

            Information_Page.hsspuc.Visibility = Visibility.Visible;
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
