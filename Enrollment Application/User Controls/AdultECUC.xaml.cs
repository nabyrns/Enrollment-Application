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
    /// <summary>
    /// Interaction logic for AdultECUC.xaml
    /// </summary>
    public partial class AdultECUC : UserControl
    {
        // declare variables that will be used
        EnrollmentDBEntities _db = new EnrollmentDBEntities();

        AdultEmergencyContact aec;

        AdultECUCTextValidation validCheck;

        public AdultECUC()
        {
            InitializeComponent();

            // initialize AdultEmergencyContact variable to contain matching information pulled from the database
            aec = (from m in _db.AdultEmergencyContacts where m.Id == LoginPage.adultCheck.Id select m).FirstOrDefault();

            // set the datacontext of the text fields to be the AdultEmergencyContact variable
            textFields.DataContext = aec;
        }

        #region Code executes when the NextBtn is clicked
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // update aec information to contain what is in the text fields
            aec.contactName = EmergencyContactNameText.Text;
            aec.relationship = ECRelationshipText.Text;
            aec.primaryNum = ECPrimaryNumText.Text;
            aec.alternateNum = altNumberText.Text;
            aec.nameNearestRelative = NearestRelativeNameText.Text;
            aec.NRrelationship = NRRelationshipText.Text;
            aec.NRstreetAddress = NRAddressText.Text;
            aec.NRcity = NRcityText.Text;
            aec.NRstate = stateCombo.Text;
            aec.NRzip = zipText.Text;
            aec.NRprimaryNum = NRPrimaryNumText.Text;
            aec.NRworkNum = workNumberText.Text;
            aec.NRcellNum = cellNumberText.Text;

            // initialize or update the validCheck variable, which is a text validation variable
            // the connection between the AdultEmergencyContact variable and the validCheck variable is what allows for updating in the database
            validCheck = new AdultECUCTextValidation()
            {
                _contactName = aec.contactName,
                _relationship = aec.relationship,
                _primaryNum = aec.primaryNum,
                _alternateNum = aec.alternateNum,
                _nameNearestRelative = aec.nameNearestRelative,
                _NRrelationship = aec.nameNearestRelative,
                _NRstreetAddress = aec.NRstreetAddress,
                _NRcity = aec.NRcity,
                _NRstate = aec.NRstate,
                _NRzip = aec.NRzip,
                _NRprimaryNum = aec.NRprimaryNum,
                _NRworkNum = aec.NRworkNum,
                _NRcellNum = aec.NRcellNum
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


                Information_Page.aecuc.Visibility = Visibility.Hidden;

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