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
    public partial class AdultECUC : UserControl
    {
        // declare variables that will be used

        AdultEmergencyContactClass aec;

        AdultECUCTextValidation validCheck = new AdultECUCTextValidation();

        #region Constructor
        public AdultECUC()
        {
            InitializeComponent();

            // create DataAccess variable to retrieve stored information for the user, and autofill
            DataAccess db = new DataAccess();

            aec = db.GetAEC(LoginPage.adultCheck.Id);

            // set the datacontext of the text fields
            textFields.DataContext = aec;
        }
        #endregion

        #region Code executes when the NextBtn is clicked
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // update aec variables
            aec.contactName = EmergencyContactNameText.Text.Trim();
            aec.relationship = ECRelationshipText.Text.Trim();
            aec.primaryNum = ECPrimaryNumText.Text.Trim();
            aec.alternateNum = altNumberText.Text.Trim();
            aec.nameNearestRelative = NearestRelativeNameText.Text.Trim();
            aec.NRrelationship = NRRelationshipText.Text.Trim();
            aec.NRstreetAddress = NRAddressText.Text.Trim();
            aec.NRcity = NRcityText.Text.Trim();
            aec.NRstate = stateCombo.Text;
            aec.NRzip = zipText.Text.Trim();
            aec.NRprimaryNum = NRPrimaryNumText.Text.Trim();
            aec.NRworkNum = workNumberText.Text.Trim();
            aec.NRcellNum = cellNumberText.Text.Trim();

            // update the validCheck variable

            validCheck.UpdateValues(aec);

            // update the datacontext to be validCheck if it was not already
            if (textFields.DataContext != validCheck)
            {
                textFields.DataContext = validCheck;
            }

            // if no errors are found, save changes to database and change visibility of UC to hidden
            // also change selected index for Information_Page --- this is what controls the moving cursor grid on that page
            if (validCheck.IsValid)
            {
                // DataAccess variable does the saving to the database
                DataAccess db = new DataAccess();

                db.SaveAEC(aec);

                Information_Page.aecuc.Visibility = Visibility.Hidden;

                Information_Page.aspuc.Visibility = Visibility.Visible;

                Information_Page.selectedIndex = 3;

                Information_Page.lv.SelectedIndex = 3;
            }
        }
        #endregion

        #region Code executes when the BackBtn is clicked
        // changes UC visibility in Information_Page
        // also changes the selected index of the listview to match the UC that is being displayed
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Information_Page.aecuc.Visibility = Visibility.Hidden;

            Information_Page.selectedIndex = 1;

            Information_Page.lv.SelectedIndex = 1;

            Information_Page.hiuc.Visibility = Visibility.Visible;
        }
        #endregion
    }
}