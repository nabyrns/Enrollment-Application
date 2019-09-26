﻿using System;
using System.Collections.Generic;
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
    public partial class BasicInformationUC : UserControl
    {
        EnrollmentDBEntities _db = new EnrollmentDBEntities();

        BasicInfoTextValidation validCheck;

        HighSchoolBasicInfo hbi;
        AdultBasicInfo abi;

        #region Constructor --- initializes variables based on student login type and assigns datacontext for textfields in this UC to whichever variable is used
        public BasicInformationUC()
        {
            InitializeComponent();

            hbi = null;
            abi = null;

            if (LoginPage.highschoolCheck != null)
            {
                hbi = (from m in _db.HighSchoolBasicInfoes where m.Id == LoginPage.highschoolCheck.Id select m).FirstOrDefault();

                textFields.DataContext = hbi;
            }

            else
            {
                abi = (from m in _db.AdultBasicInfoes where m.Id == LoginPage.adultCheck.Id select m).FirstOrDefault();

                textFields.DataContext = abi;
            }
        }
        #endregion

        #region Code executes when NextBtn is clicked
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // check what kind of student was logged in
            if (abi != null)
            {
                // reassign the values of the HealthInfo object to be what is in the control for that variable
                abi.lastName = lastNameText.Text;
                abi.firstName = firstNameText.Text;
                abi.middleInitial = abi.middleInitial;
                abi.program = programText.Text;
                abi.streetAddress = streetAddressText.Text;
                abi.city = cityText.Text;
                abi.state = stateCombo.Text;
                abi.zipCode = zipCodeText.Text;
                abi.primaryPhoneNum = primaryPhoneText.Text;
                abi.cellPhoneNum = cellPhoneText.Text;
                abi.hispanicOrLatino = ethnicityCombo.Text;
                abi.race = raceCombo.Text;
                abi.gender = genderCombo.Text;
                abi.dateOfBirth = birthdateCalendar.SelectedDate;


                // initialize or update validCheck to contain the newly updated values in the UC being used
                // the connection between validCheck and the UC being used is what allows for saving to the database
                validCheck = new BasicInfoTextValidation()
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
                    _dateOfBirth = abi.dateOfBirth
                };
            }

            // same as above code
            else
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

                validCheck = new BasicInfoTextValidation()
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
                    _dateOfBirth = hbi.dateOfBirth
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


                Information_Page.buc.Visibility = Visibility.Hidden;

                Information_Page.hiuc.Visibility = Visibility.Visible;

                Information_Page.selectedIndex = 1;

                Information_Page.lv.SelectedIndex = 1;
            }
        }

        #endregion
    }
}
