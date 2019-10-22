using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

    public partial class AdultBasicInformationUC : UserControl
    {
        // declare variables that will be used in the class

        AdultBasicInfoTextValidation validCheck = new AdultBasicInfoTextValidation();

        AdultBasicInfoClass abi;

        #region Constructor --- initializes variables and assigns datacontext for textfields in this UC
        public AdultBasicInformationUC()
        {
            InitializeComponent();

            // create DataAccess object to pull all the information stored in the database for this user and autofill fields
            DataAccess db = new DataAccess();

            abi = db.GetABI(LoginPage.adultCheck.Id);

            textFields.DataContext = abi;
        }
        #endregion

        #region Code executes when NextBtn is clicked
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {

            // update BasicInfo object values
            abi.lastName = lastNameText.Text.Trim();
            abi.firstName = firstNameText.Text.Trim();
            abi.middleInitial = middleInitialText.Text.Trim();
            abi.program = programText.Text;
            abi.streetAddress = streetAddressText.Text.Trim();
            abi.city = cityText.Text.Trim();
            abi.state = stateCombo.Text;
            abi.zipCode = zipCodeText.Text.Trim();
            abi.primaryPhoneNum = primaryPhoneText.Text.Trim();
            abi.cellPhoneNum = cellPhoneText.Text.Trim();
            abi.hispanicOrLatino = ethnicityCombo.Text;
            abi.race = raceCombo.Text;
            abi.gender = genderCombo.Text;
            abi.dateOfBirth = birthdateCalendar.SelectedDate;
            abi.SSN = SSNtext.Text;
            abi.completedEdLevel = educationLevelCombo.Text;
            abi.attendedCollegeOrTech = attendedCollegeCombo.Text;
            abi.liveWithParent = liveWithParentCombo.Text;


            // update validCheck to contain the newly updated values in the UC being used

            validCheck.UpdateValues(abi);

            // update the datacontext to be validCheck if it was not already
            if (textFields.DataContext != validCheck)
            {
                textFields.DataContext = validCheck;
            }

            // if no errors are found, save changes to database and change visibility of UC to hidden
            // also change selected index for Information_Page --- this is what controls the moving cursor grid on that page
            DataAccess db = new DataAccess();

            db.SaveABI(abi, validCheck);

            if (validCheck.IsValid.Count == 0)
            {

                Information_Page.abiuc.Visibility = Visibility.Hidden;

                Information_Page.hiuc.Visibility = Visibility.Visible;

                Information_Page.selectedIndex = 1;

                Information_Page.lv.SelectedIndex = 1;
            }
        }


        #endregion

        #region This method controls the automatically added hyphens when typing a SSN
        private void SSNtext_KeyDown(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(SSNtext.Text) && e.Key != Key.Back && SSNtext.Text.Length < 10)
            {
                if (SSNtext.Text.Length == 3)
                {
                    SSNtext.Text += "-";
                }

                else if (SSNtext.Text.Length == 6)
                {
                    SSNtext.Text += "-";
                }

                SSNtext.SelectionStart = SSNtext.Text.Length;
            }
        }
        #endregion
    }
}
