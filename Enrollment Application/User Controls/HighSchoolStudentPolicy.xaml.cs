using Enrollment_Application.Text_Validation_Classes;
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

namespace Enrollment_Application.User_Controls
{
    /// <summary>
    /// Interaction logic for HighSchoolStudentPolicy.xaml
    /// </summary>
    /// 

    

    #region Constructor --- initializes variables and assigns datacontext for textfields in this UC
    public partial class HighSchoolStudentPolicy : UserControl
    {

        EnrollmentDBEntities _db = new EnrollmentDBEntities();

        HighSchoolPolicyTextValidation validCheck;

        HighSchoolPolicy hbi;
        public HighSchoolStudentPolicy()
        {
            InitializeComponent();

            hbi = (from m in _db.HighSchoolBasicInfoes where m.Id == LoginPage.highschoolCheck.Id select m).FirstOrDefault();

            textFields.DataContext = hbi;
        }
    }
    #endregion

    #region Code executes when NextBtn is clicked
    private void NextBtn_Click(object sender, RoutedEventArgs e)
    {
        hbi.lastName = lastNameText.Text;
        

        validCheck = new HighSchoolPolicyTextValidation()
        {
            
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
