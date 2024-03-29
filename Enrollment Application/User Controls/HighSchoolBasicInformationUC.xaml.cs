﻿using System;
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
        // declare variables that will be used
        HighSchoolBasicInfoTextValidation validCheck = new HighSchoolBasicInfoTextValidation();

        HighSchoolBasicInfoClass hbi;

        #region Constructor --- initializes variables and assigns datacontext for textfields in this UC
        public HighSchoolBasicInformationUC()
        {
            InitializeComponent();

            // DataAccess variable to retrieve and autofill previously saved information
            DataAccess db = new DataAccess();

            hbi = db.GetHSBI(LoginPage.highschoolCheck.Id);

            textFields.DataContext = hbi;
        }
        #endregion

        #region Code executes when NextBtn is clicked
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // update object variables to be current
            hbi.lastName = lastNameText.Text.Trim();
            hbi.firstName = firstNameText.Text.Trim();
            hbi.middleInitial = middleInitialText.Text.Trim();
            hbi.program = programText.Text;
            hbi.streetAddress = streetAddressText.Text.Trim();
            hbi.city = cityText.Text.Trim();
            hbi.state = stateCombo.Text;
            hbi.zipCode = zipCodeText.Text.Trim();
            hbi.primaryPhoneNum = primaryPhoneText.Text.Trim();
            hbi.cellPhoneNum = cellPhoneText.Text.Trim();
            hbi.hispanicOrLatino = ethnicityCombo.Text;
            hbi.race = raceCombo.Text;
            hbi.gender = genderCombo.Text;
            hbi.dateOfBirth = birthdateCalendar.SelectedDate;
            hbi.sendingHS = sendingHSText.Text.Trim();
            hbi.currentEdLevel = educationLevelCombo.Text;

            // update values of validCheck
            validCheck.UpdateValues(hbi);

            // update the datacontext to be validCheck if it was not already
            if (textFields.DataContext != validCheck)
            {
                textFields.DataContext = validCheck;
            }

            // if no errors are found, save changes to database and change visibility of UC to hidden
            // also change selected index for Information_Page --- this is what controls the moving cursor grid on that page
            // DataAccess variable to save information to the database
            DataAccess db = new DataAccess();

            db.SaveHSBI(hbi, validCheck);

            if (validCheck.IsValid.Count == 0)
            {

                Information_Page.hsbiuc.Visibility = Visibility.Hidden;

                Information_Page.hiuc.Visibility = Visibility.Visible;

                Information_Page.selectedIndex = 1;

                Information_Page.lv.SelectedIndex = 1;
            }
        }

        #endregion
    }
}
