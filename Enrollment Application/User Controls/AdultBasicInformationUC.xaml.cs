﻿using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Interaction logic for BasicInformationUC.xaml
    /// </summary>
    public partial class AdultBasicInformationUC : UserControl
    {
        EnrollmentDBEntities _db = new EnrollmentDBEntities();

        AdultBasicInfoTextValidation validCheck;

        AdultBasicInfo abi;

        #region Constructor --- initializes variables and assigns datacontext for textfields in this UC
        public AdultBasicInformationUC()
        {
            InitializeComponent();

            abi = (from m in _db.AdultBasicInfoes where m.Id == LoginPage.adultCheck.Id select m).FirstOrDefault();

            textFields.DataContext = abi;
        }
        #endregion

        #region Code executes when NextBtn is clicked
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {

            String salt = CommonMethods.CreateSalt(20);
            byte[] hashedSSN = CommonMethods.GenerateSaltedHash(Encoding.UTF8.GetBytes(SSNtext.Text), Encoding.UTF8.GetBytes(salt));

            // reassign the values of the BasicInfo object to be what is in the control for that variable
            abi.lastName = lastNameText.Text.Trim();
            abi.firstName = firstNameText.Text.Trim();
            abi.middleInitial = middleInitialText.Text.Trim();
            abi.program = programText.Text.Trim();
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
            abi.filloutDate = DateTime.Now;
            abi.SSNhashAndSalt = Convert.ToBase64String(hashedSSN);
            abi.SSNsalt = salt;
            abi.completedEdLevel = educationLevelCombo.Text;
            abi.attendedCollegeOrTech = attendedCollegeCombo.Text;
            abi.liveWithParent = liveWithParentCombo.Text;


            // initialize or update validCheck to contain the newly updated values in the UC being used
            // the connection between validCheck and the UC being used is what allows for saving to the database
            validCheck = new AdultBasicInfoTextValidation()
            {
                _lastName = abi.lastName,
                _firstName = abi.firstName,
                _middleInitial = abi.middleInitial,
                _program = abi.program,
                _streetAddress = abi.streetAddress,
                _city = abi.city,
                _state = abi.state,
                _zip = abi.zipCode,
                _primaryPhoneNum = abi.primaryPhoneNum,
                _cellPhoneNum = abi.cellPhoneNum,
                _hispanicOrLatino = abi.hispanicOrLatino,
                _race = abi.race,
                _gender = abi.gender,
                _dateOfBirth = abi.dateOfBirth,
                _completedEdLevel = abi.completedEdLevel,
                _attendedCollegeOrTech = abi.attendedCollegeOrTech,
                _liveWithParent = abi.liveWithParent,
                _SSN = SSNtext.Text
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


                Information_Page.abiuc.Visibility = Visibility.Hidden;

                Information_Page.hiuc.Visibility = Visibility.Visible;

                Information_Page.selectedIndex = 1;

                Information_Page.lv.SelectedIndex = 1;
            }
        }


        #endregion

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
    }
}
