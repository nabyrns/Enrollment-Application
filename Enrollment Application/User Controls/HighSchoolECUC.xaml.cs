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
    public partial class HighSchoolECUC : UserControl
    {
        // declare variables that will be used

        HighSchoolEmergencyContactClass hsec;

        HighSchoolECUCTextValidation validCheck = new HighSchoolECUCTextValidation();

        #region Constructor
        public HighSchoolECUC()
        {
            InitializeComponent();

            // DataAccess variable to retrieve and autofill previously stored data for the user
            DataAccess db = new DataAccess();

            hsec = db.GetHSEC(LoginPage.highschoolCheck.Id);

            // set the datacontext of the text fields to be the AdultEmergencyContact variable
            textFields.DataContext = hsec;
        }
        #endregion

        #region Code executes when the BackBtn is clicked
        // changes UC visibility in Information_Page
        // also changes the selected index of the listview to match the UC that is being displayed
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Information_Page.hsecuc.Visibility = Visibility.Hidden;

            Information_Page.selectedIndex = 1;

            Information_Page.lv.SelectedIndex = 1;

            Information_Page.hiuc.Visibility = Visibility.Visible;
        }
        #endregion

        #region Code executes when the NextBtn is clicked
        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // update hsec to contain current information
            hsec.parentNameOne = parentOneNameText.Text.Trim();
            hsec.parentOneRelationship = parentOneRelationshipText.Text.Trim();
            hsec.parentOneAddress = parentOneAddressText.Text.Trim();
            hsec.parentOneCity = parentOneCityText.Text.Trim();
            hsec.parentOneState = stateCombo.Text;
            hsec.parentOneZip = zipText.Text.Trim();
            hsec.parentOnePrimaryNum = parentOnePrimaryNumText.Text.Trim();
            hsec.parentOneCellNum = parentOneCellNumText.Text.Trim();
            hsec.parentOneEmail = parentOneEmailText.Text.Trim();
            hsec.residesWithP1 = residesWithP1Check.IsChecked.ToString();
            hsec.parentNameTwo = parentTwoNameText.Text.Trim();
            hsec.parentTwoRelationship = parentTwoRelationshipText.Text.Trim();
            hsec.parentTwoAddress = parentTwoAddressText.Text.Trim();
            hsec.parentTwoCity = parentTwoCityText.Text.Trim();
            hsec.parentTwoState = stateComboTwo.Text;
            hsec.parentTwoZip = zipTextTwo.Text.Trim();
            hsec.parentTwoPrimaryNum = parentTwoPrimaryNumText.Text.Trim();
            hsec.parentTwoCellNum = parentTwoCellNumText.Text.Trim();
            hsec.parentTwoEmail = parentTwoEmailText.Text.Trim();
            hsec.residesWithP2 = residesWithP2Check.IsChecked.ToString();
            hsec.EContactName = EContactNameText.Text.Trim();
            hsec.EContactRelationship = EContactRelationshipText.Text.Trim();
            hsec.EContactPrimaryNum = EContactPrimaryNumText.Text.Trim();
            hsec.EContactCellNum = EContactCellNumText.Text.Trim();


            // update the validCheck variable
            validCheck.UpdateValues(hsec);

            // update the datacontext to be validCheck if it was not already
            if (textFields.DataContext != validCheck)
            {
                textFields.DataContext = validCheck;
            }

            // if no errors are found, save changes to database and change visibility of UC to hidden
            // also change selected index for Information_Page --- this is what controls the moving cursor grid on that page
            // DataAccess variable to save information to database
            DataAccess db = new DataAccess();

            db.SaveHSEC(hsec, validCheck);

            if (validCheck.IsValid.Count == 0)
            {

                Information_Page.hsecuc.Visibility = Visibility.Hidden;

                Information_Page.hsspuc.Visibility = Visibility.Visible;

                Information_Page.selectedIndex = 3;

                Information_Page.lv.SelectedIndex = 3;
            }
            #endregion

        }
    }
}
