﻿using System;
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

        public static HealthInfoUC hiuc;

        public static AdultECUC aecuc = null;

        // public static HighSchoolECUC hsecuc = null;

        // this variable will prevent clicking the listview to change which part of the form is displayed
        // the forms may be navigated only using buttons in the user control
        public static int selectedIndex = 0;

        #region Constructors
        // Constructor for window with adult login parameter
        public Information_Page(AdultLogin logIn)
        {
            InitializeComponent();

            buc = BasicUC;

            hiuc = HealthUC;

            aecuc = AdultECUC;

            BasicUC.Visibility = Visibility.Visible;

            lv = ListViewMenu;

            lv.DataContext = selectedIndex;
        }

        // Constructor for window with highschool login parameter
        public Information_Page(HighSchoolLogin logIn)
        {
            InitializeComponent();

            buc = BasicUC;

            hiuc = HealthUC;

            // hsecuc = HighSchoolECUC;

            BasicUC.Visibility = Visibility.Visible;

            lv = ListViewMenu;
        }
        #endregion

        #region Handles the moving of the listview cursor
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // if the selectedIndex variable was not changed before the lv.SelectedIndex was changed, the lv.SelectedIndex is reset, as this means that the listview was clicked
            if (lv.SelectedIndex != selectedIndex)
            {
                lv.SelectedIndex = selectedIndex;
                return;
            }

            // otherwise the index is assigned and passed into the method that moves the cursor that shows what listviewitem is currently displayed
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

        #region Code that allows for dragging the window
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        #endregion
    }
}
