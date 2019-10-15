using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    public partial class LoginPage : Window
    {

        // declare both types of login variables that are possible for later use
        public static Login highschoolCheck;
        public static Login adultCheck;

        public LoginPage()
        {
            InitializeComponent();
        }

        #region Code executes when CreateAccBtn is clicked
        // opens the "Create Account" Window
        private void CreateAccBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateAccountPage CAPage = new CreateAccountPage();
            CAPage.ShowDialog();
        }
        #endregion

        #region Code executes when signin button is clicked
        private void SignInBtn_Click(object sender, RoutedEventArgs e)
        {
            DataAccess db = new DataAccess();

            // assign value from password box to temp variable
            string pw = PasswordText.Password;

            // perform searches to see if the user exists in either login table in the db
            highschoolCheck = db.HSCheck(EmailText.Text);

            adultCheck = db.AdultCheck(EmailText.Text);


            // declare byte array that will hold the saltedHash of the user entered password
            // this will later be compared to the stored values to check if the login credentials are correct
            byte[] saltedHash;

            // if the email was found in the highschoollogin table, this code executes
            if (highschoolCheck != null)
            {
                // saltedHash is assigned the value of the salted hash that is returned by the called method
                saltedHash = CommonMethods.GenerateSaltedHash(Encoding.UTF8.GetBytes(pw), Encoding.UTF8.GetBytes(highschoolCheck.passwordSalt));

                // if the byte array that is stored matches the byte array produced by the user input, this code executes
                if (CommonMethods.CompareByteArrays(saltedHash, Convert.FromBase64String(highschoolCheck.passwordHash)))
                {
                    this.Visibility = Visibility.Hidden;

                    Information_Page ip = new Information_Page(highschoolCheck);
                    ip.ShowDialog();

                    this.Close();
                }

                // otherwise an error message is displayed
                else
                {
                    ErrorMessage error = new ErrorMessage("Incorrect password.");

                    error.ShowDialog();
                }
            }


            // if the email is found in the adultlogins table, this code executes
            else if (adultCheck != null)
            {
                // salted hash is generated and assigned to saltedHash variable by calling a method
                saltedHash = CommonMethods.GenerateSaltedHash(Encoding.UTF8.GetBytes(pw), Encoding.UTF8.GetBytes(adultCheck.passwordSalt));

                // if the byte arrays match, this code executes
                if (CommonMethods.CompareByteArrays(saltedHash, Convert.FromBase64String(adultCheck.passwordHash)))
                {
                    this.Visibility = Visibility.Hidden;

                    if (EmailText.Text.ToLower().Equals("cvtcadmin"))
                    {
                        AdminWindow aw = new AdminWindow();

                        aw.ShowDialog();
                    }

                    else
                    {
                        Information_Page ip = new Information_Page(adultCheck);
                        ip.ShowDialog();
                    }

                    this.Close();
                }

                // otherwise, error message displays
                else
                {
                    ErrorMessage error = new ErrorMessage("Incorrect password.");

                    error.ShowDialog();
                }
            }

            // if user is not found in either table, error message displays
            else
            {

                ErrorMessage error = new ErrorMessage("No account exists with that email.");

                error.ShowDialog();
            }
        }

        #endregion

        #region Code executes when CloseBtn is clicked
        // closes the window
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
