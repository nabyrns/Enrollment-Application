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
    public partial class AdminWindow : Window
    {
        public static string[] name = { "" };

        public AdminWindow()
        {
            InitializeComponent();

            DataAccess db = new DataAccess();

            myDataGrid.ItemsSource = db.AdultDGSearch();
            myHSDataGrid.ItemsSource = db.HSDGSearch();
        }

        private void ViewAndEditBtn_Click(object sender, RoutedEventArgs e)
        {
            AdultBasicInfoClass abi = myDataGrid.SelectedItem as AdultBasicInfoClass;

            AdminStudentInformationWindowA asiwA = new AdminStudentInformationWindowA(abi.Id);

            asiwA.ShowDialog();
        }

        private void ViewAndEditBtnHS_Click(object sender, RoutedEventArgs e)
        {
            HighSchoolBasicInfoClass hsbi = myHSDataGrid.SelectedItem as HighSchoolBasicInfoClass;

            AdminStudentInformationWindowHS asiw = new AdminStudentInformationWindowHS(hsbi.Id);

            asiw.ShowDialog();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void AdultSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            adultSearchText.Text = adultSearchText.Text.Trim();

            DataAccess db = new DataAccess();

            if (string.IsNullOrEmpty(adultSearchText.Text))
            {
                myDataGrid.ItemsSource = db.AdultDGSearch();
            }

            else
            {
                name = adultSearchText.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                if (name.Length < 2)
                {
                    myDataGrid.ItemsSource = db.AdultDGSearch(name[0]);
                }

                else
                {
                    myDataGrid.ItemsSource = db.AdultDGSearch(name[0], name[1]);
                }
            }
        }

        private void HighSchoolSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            highSchoolSearchText.Text = highSchoolSearchText.Text.Trim();

            DataAccess db = new DataAccess();

            if (string.IsNullOrEmpty(highSchoolSearchText.Text))
            {
                myHSDataGrid.ItemsSource = db.HSDGSearch();
            }

            else
            {
                name = highSchoolSearchText.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                if (name.Length < 2)
                {
                    myHSDataGrid.ItemsSource = db.HSDGSearch(name[0]);
                }

                else
                {
                    myHSDataGrid.ItemsSource = db.HSDGSearch(name[0], name[1]);
                }
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
