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
    public partial class AdultConfidentialInfoUC : UserControl
    {
        // declare variables that will be used

        AdultConfidentialInfoClass aci;

        #region Constructor
        public AdultConfidentialInfoUC()
        {
            InitializeComponent();

            DataAccess db = new DataAccess();

            aci = db.GetACI(LoginPage.adultCheck.Id);

            // set the datacontext of the text fields
            textFields.DataContext = aci;
        }
        #endregion

        #region Code executes when SubmitBtn is clicked
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            // update values of AdultConfidentialInfo object
            aci.foodStamps = foodStampsCheck.IsChecked.ToString();
            aci.dependentChildrenAid = dependentChildrenCheck.IsChecked.ToString();
            aci.supplementaryIncome = SSIcheck.IsChecked.ToString();
            aci.housingAssistance = section8Check.IsChecked.ToString();
            aci.none = noneCheck.IsChecked.ToString();
            aci.homeless = homelessCheck.IsChecked.ToString();
            aci.agedOutFosterCare = agedOutCheck.IsChecked.ToString();
            aci.outOfWorkforce = workforceCheck.IsChecked.ToString();
            aci.formCompletionDate = DateTime.Now;

            // if no boxes are checked, display error message
            if (aci.foodStamps == "False" && aci.dependentChildrenAid == "False" && aci.supplementaryIncome == "False" && aci.housingAssistance == "False" && aci.none == "False")
            {
                ErrorMessage error = new ErrorMessage("Please select all that apply to your family, if none apply --- select \'None of these apply to me or my family\'");

                error.ShowDialog();

                return;
            }

            // if the 'None' box is checked, and another box is also check, error message displays
            if ((aci.foodStamps == "True" || aci.dependentChildrenAid == "True" || aci.supplementaryIncome == "True" || aci.housingAssistance == "True") && aci.none == "True")
            {
                ErrorMessage error = new ErrorMessage("You cannot select \'None apply\' and also check other boxes.");

                error.ShowDialog();

                return;
            }

            // create DataAccess variable to then save the information to the DataBase
            DataAccess db = new DataAccess();

            db.SaveACI(aci);

            // change UserControl Visibility and display success message

            Information_Page.aciuc.Visibility = Visibility.Hidden;

            Information_Page.SuccessMessageAndShutdown();
        }

        #endregion

        #region Code executes when BackBtn is clicked
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            // change selected index and alter UC visibility to act accordingly
            Information_Page.aciuc.Visibility = Visibility.Hidden;

            Information_Page.selectedIndex = 3;

            Information_Page.lv.SelectedIndex = 3;

            Information_Page.aspuc.Visibility = Visibility.Visible;
        }

        #endregion
    }
}
