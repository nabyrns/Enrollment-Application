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
    public partial class HighSchoolBasicInformationUC : UserControl
    {
        EnrollmentDBEntities _db = new EnrollmentDBEntities();

        HighSchoolBasicInfoTextValidation validCheck;

        HighSchoolBasicInfo hbi;

        #region Constructor --- initializes variables and assigns datacontext for textfields in this UC
        public HighSchoolBasicInformationUC()
        {
            InitializeComponent();

            hbi = (from m in _db.HighSchoolBasicInfoes where m.Id == LoginPage.highschoolCheck.Id select m).FirstOrDefault();

            textFields.DataContext = hbi;
        }
        #endregion

        #region Code executes when NextBtn is clicked
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            hbi.lastName = lastNameText.Text;
            hbi.firstName = firstNameText.Text;
            hbi.middleInitial = middleInitialText.Text;
            hbi.program = programText.Text;
            hbi.streetAddress = streetAddressText.Text;
            hbi.city = cityText.Text;
            hbi.state = stateCombo.Text;
            hbi.zipCode = zipCodeText.Text;
            hbi.primaryPhoneNum = primaryPhoneText.Text;
            hbi.cellPhoneNum = cellPhoneText.Text;
            hbi.hispanicOrLatino = ethnicityCombo.Text;
            hbi.race = raceCombo.Text;
            hbi.gender = genderCombo.Text;
            hbi.dateOfBirth = birthdateCalendar.SelectedDate;
            hbi.sendingHS = sendingHSText.Text;
            hbi.currentEdLevel = educationLevelCombo.Text;
            hbi.filloutDate = DateTime.Now;

            validCheck = new HighSchoolBasicInfoTextValidation()
            {
                _lastName = hbi.lastName,
                _firstName = hbi.firstName,
                _middleInitial = hbi.middleInitial,
                _program = hbi.program,
                _streetAddress = hbi.streetAddress,
                _city = hbi.city,
                _state = hbi.state,
                _zip = hbi.zipCode,
                _primaryPhoneNum = hbi.primaryPhoneNum,
                _cellPhoneNum = hbi.cellPhoneNum,
                _hispanicOrLatino = hbi.hispanicOrLatino,
                _race = hbi.race,
                _gender = hbi.gender,
                _dateOfBirth = hbi.dateOfBirth,
                _currentEdLevel = hbi.currentEdLevel,
                _sendingHS = hbi.sendingHS
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


                Information_Page.hsbiuc.Visibility = Visibility.Hidden;

                Information_Page.hiuc.Visibility = Visibility.Visible;

                Information_Page.selectedIndex = 1;

                Information_Page.lv.SelectedIndex = 1;
            }
        }

        #endregion
    }
}
