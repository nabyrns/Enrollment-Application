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
    /// Interaction logic for AdultConfidentialInfoUC.xaml
    /// </summary>
    public partial class AdultConfidentialInfoUC : UserControl
    {
        // declare variables that will be used
        EnrollmentDBEntities _db = new EnrollmentDBEntities();

        AdultConfidentialInfo aci;

        public AdultConfidentialInfoUC()
        {
            InitializeComponent();

            // initialize AdultEmergencyContact variable to contain matching information pulled from the database
            aci = (from m in _db.AdultConfidentialInfoes where m.Id == LoginPage.adultCheck.Id select m).FirstOrDefault();

            // set the datacontext of the text fields to be the AdultEmergencyContact variable
            textFields.DataContext = aci;
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            aci.foodStamps = foodStampsCheck.IsChecked.ToString();
            aci.dependentChildrenAid = dependentChildrenCheck.IsChecked.ToString();
            aci.supplementaryIncome = SSIcheck.IsChecked.ToString();
            aci.housingAssistance = section8Check.IsChecked.ToString();
            aci.none = noneCheck.IsChecked.ToString();
            aci.homeless = homelessCheck.IsChecked.ToString();
            aci.agedOutFosterCare = agedOutCheck.IsChecked.ToString();
            aci.outOfWorkforce = workforceCheck.IsChecked.ToString();

            if (aci.foodStamps == "False" && aci.dependentChildrenAid == "False" && aci.supplementaryIncome == "False" && aci.housingAssistance == "False" && aci.none == "False")
            {
                ErrorMessage error = new ErrorMessage("Please select all that apply to your family, if none apply --- select \'None of these apply to me or my family\'");

                error.ShowDialog();

                return;
            }

            if ((aci.foodStamps == "True" || aci.dependentChildrenAid == "True" || aci.supplementaryIncome == "True" || aci.housingAssistance == "True") && aci.none == "True")
            {
                ErrorMessage error = new ErrorMessage("You cannot select \'None apply\' and also check other boxes.");

                error.ShowDialog();

                return;
            }

            _db.SaveChanges();

            Information_Page.aciuc.Visibility = Visibility.Hidden;

            Information_Page.SuccessMessageAndShutdown();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Information_Page.aciuc.Visibility = Visibility.Hidden;

            Information_Page.selectedIndex = 3;

            Information_Page.lv.SelectedIndex = 3;

            Information_Page.aspuc.Visibility = Visibility.Visible;
        }
    }
}
