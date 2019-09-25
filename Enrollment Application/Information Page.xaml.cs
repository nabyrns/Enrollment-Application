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
    /// Interaction logic for Information_Page.xaml
    /// </summary>
    public partial class Information_Page : Window
    {
        public static ListView lv;

        public static BasicInformationUC buc;

        #region Constructors
        // Constructor for window with adult login parameter
        public Information_Page(AdultLogin logIn)
        {
            InitializeComponent();

            buc = BasicUC;

            BasicUC.Visibility = Visibility.Visible;

            lv = ListViewMenu;
        }

        // Constructor for window with highschool login parameter
        public Information_Page(HighSchoolLogin logIn)
        {
            InitializeComponent();

            buc = BasicUC;

            BasicUC.Visibility = Visibility.Visible;

            lv = ListViewMenu;
        }
        #endregion

        #region Handles the moving of the listview cursor
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;

            MoveCursorMenu(index);
        }

        // method that executes the moving of the transitioning grid
        private void MoveCursorMenu(int index)
        {
            TransitionSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(10, 55 + (40 * index), 0, 0);
        }
        #endregion

        #region Code executes when CloseBtn is clicked
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
