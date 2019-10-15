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
    public partial class OverwriteConfirmation : Window
    {
        public OverwriteConfirmation(string errorMessage)
        {
            InitializeComponent();

            errorMessageText.Text = errorMessage;
        }

        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminStudentInformationWindowHS.overwrite = true;

            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
