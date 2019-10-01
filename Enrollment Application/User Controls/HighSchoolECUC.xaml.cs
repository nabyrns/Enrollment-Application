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
    /// Interaction logic for HighSchoolECUC.xaml
    /// </summary>
    public partial class HighSchoolECUC : UserControl
    {
        // declare variables that will be used
        EnrollmentDBEntities _db = new EnrollmentDBEntities();

        HighSchoolEmergencyContact hsec;

        HighSchoolECUCTextValidation validCheck;

        public HighSchoolECUC()
        {
            InitializeComponent();

            // initialize AdultEmergencyContact variable to contain matching information pulled from the database
            hsec = (from m in _db.HighSchoolEmergencyContacts where m.Id == LoginPage.highschoolCheck.Id select m).FirstOrDefault();

            // set the datacontext of the text fields to be the AdultEmergencyContact variable
            textFields.DataContext = hsec;
        }

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

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // update aec information to contain what is in the text fields
            hsec.parentNameOne = parentOneNameText.Text;
            hsec.parentOneRelationship = parentOneRelationshipText.Text;
            hsec.parentOneAddress = parentOneAddressText.Text;
            hsec.parentOneCity = parentOneCityText.Text;
            hsec.parentOneState = stateCombo.Text;
            hsec.parentOneZip = zipText.Text;
            hsec.parentOnePrimaryNum = parentOnePrimaryNumText.Text;
            hsec.parentOneCellNum = parentOneCellNumText.Text;
            hsec.parentOneEmail = parentOneEmailText.Text;
            hsec.residesWithP1 = residesWithP1Check.IsChecked.ToString();
            hsec.parentNameTwo = parentTwoNameText.Text;
            hsec.parentTwoRelationship = parentTwoRelationshipText.Text;
            hsec.parentTwoAddress = parentTwoAddressText.Text;
            hsec.parentTwoCity = parentTwoCityText.Text;
            hsec.parentTwoState = stateComboTwo.Text;
            hsec.parentTwoZip = zipTextTwo.Text;
            hsec.parentTwoPrimaryNum = parentTwoPrimaryNumText.Text;
            hsec.parentTwoCellNum = parentTwoCellNumText.Text;
            hsec.parentTwoEmail = parentTwoEmailText.Text;
            hsec.residesWithP2 = residesWithP2Check.IsChecked.ToString();
            hsec.EContactName = EContactNameText.Text;
            hsec.EContactRelationship = EContactRelationshipText.Text;
            hsec.EContactPrimaryNum = EContactPrimaryNumText.Text;
            hsec.EContactCellNum = EContactCellNumText.Text;


            // initialize or update the validCheck variable, which is a text validation variable
            // the connection between the AdultEmergencyContact variable and the validCheck variable is what allows for updating in the database
            validCheck = new HighSchoolECUCTextValidation()
            {
                _parentNameOne = hsec.parentNameOne,
                _parentNameTwo = hsec.parentNameTwo,
                _parentOneRelationship = hsec.parentOneRelationship,
                _parentTwoRelationship = hsec.parentTwoRelationship,
                _parentOnePrimaryNum = hsec.parentOnePrimaryNum,
                _parentTwoPrimaryNum = hsec.parentTwoPrimaryNum,
                _parentOneCellNum = hsec.parentOneCellNum,
                _parentTwoCellNum = hsec.parentTwoCellNum,
                _parentOneAddress = hsec.parentOneAddress,
                _parentTwoAddress = hsec.parentTwoAddress,
                _parentOneCity = hsec.parentOneCity,
                _parentTwoCity = hsec.parentTwoCity,
                _parentOneState = hsec.parentOneState,
                _parentTwoState = hsec.parentTwoState,
                _parentOneZip = hsec.parentOneZip,
                _parentTwoZip = hsec.parentTwoZip,
                _parentOneEmail = hsec.parentOneEmail,
                _parentTwoEmail = hsec.parentTwoEmail,
                _residesWithP1 = bool.Parse(hsec.residesWithP1),
                _residesWithP2 = bool.Parse(hsec.residesWithP2),
                _eContactName = hsec.EContactName,
                _eContactRelationship = hsec.EContactRelationship,
                _eContactPrimaryNum = hsec.EContactPrimaryNum,
                _eContactCellNum = hsec.EContactCellNum
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


                Information_Page.hsecuc.Visibility = Visibility.Hidden;

                Information_Page.selectedIndex = 3;

                Information_Page.lv.SelectedIndex = 3;
            }
        }
    }
}
