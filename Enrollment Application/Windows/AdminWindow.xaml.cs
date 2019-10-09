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
using System.Windows.Shapes;

namespace Enrollment_Application
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public static string[] name = { "" };

        EnrollmentDBEntities _db = new EnrollmentDBEntities();

        public AdminWindow()
        {
            InitializeComponent();

            myDataGrid.ItemsSource = (from q in _db.AdultBasicInfoes where q.lastName != null && q.firstName != null select q).OrderBy(q => q.lastName).ToList();
            myHSDataGrid.ItemsSource = (from q in _db.HighSchoolBasicInfoes where q.lastName != null && q.firstName != null select q).OrderBy(q => q.lastName).ToList();
        }

        private void ViewAndEditBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ViewAndEditBtnHS_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void AdultSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            adultSearchText.Text = adultSearchText.Text.Trim();

            if (string.IsNullOrEmpty(adultSearchText.Text))
            {
                myDataGrid.ItemsSource = (from q in _db.AdultBasicInfoes where q.lastName != null && q.firstName != null select q).OrderBy(q => q.lastName).ToList();
            }

            else
            {
                name = adultSearchText.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                if (name.Length < 2)
                {
                    string search = name[0];

                    myDataGrid.ItemsSource = (from q in _db.AdultBasicInfoes where q.firstName.ToLower().Contains(search.ToLower()) || q.lastName.ToLower().Contains(search.ToLower()) select q).OrderBy(q => q.lastName).ToList();
                }

                else
                {
                    string firstSearch = name[0];
                    string secondSearch = name[1];

                    myDataGrid.ItemsSource = (from q in _db.AdultBasicInfoes where (q.firstName.ToLower().Contains(firstSearch.ToLower()) && q.lastName.ToLower().Contains(secondSearch.ToLower())) || (q.firstName.ToLower().Contains(secondSearch.ToLower()) && q.lastName.ToLower().Contains(firstSearch.ToLower())) select q).ToList();
                }
            }
        }

        private void HighSchoolSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            highSchoolSearchText.Text = highSchoolSearchText.Text.Trim();

            if (string.IsNullOrEmpty(highSchoolSearchText.Text))
            {
                myHSDataGrid.ItemsSource = (from q in _db.HighSchoolBasicInfoes where q.lastName != null && q.firstName != null select q).OrderBy(q => q.lastName).ToList();
            }

            else
            {
                name = highSchoolSearchText.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                if (name.Length < 2)
                {
                    string search = name[0];

                    myHSDataGrid.ItemsSource = (from q in _db.HighSchoolBasicInfoes where q.firstName.ToLower().Contains(search.ToLower()) || q.lastName.ToLower().Contains(search.ToLower()) select q).OrderBy(q => q.lastName).ToList();
                }

                else
                {
                    string firstSearch = name[0];
                    string secondSearch = name[1];

                    myHSDataGrid.ItemsSource = (from q in _db.HighSchoolBasicInfoes where (q.firstName.ToLower().Contains(firstSearch.ToLower()) && q.lastName.ToLower().Contains(secondSearch.ToLower())) || (q.firstName.ToLower().Contains(secondSearch.ToLower()) && q.lastName.ToLower().Contains(firstSearch.ToLower())) select q).ToList();
                }
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
