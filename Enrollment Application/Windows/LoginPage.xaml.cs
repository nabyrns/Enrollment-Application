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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        EnrollmentDBEntities _db = new EnrollmentDBEntities();

        // declare both types of login variables that are possible for later use
        public static HighSchoolLogin highschoolCheck;
        public static AdultLogin adultCheck;

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
            // assign value from password box to temp variable
            string pw = PasswordText.Password;

            // perform searches to see if the user exists in either login table in the db
            highschoolCheck = (from m in _db.HighSchoolLogins where m.email.ToLower().Equals(EmailText.Text.ToLower()) select m).FirstOrDefault();

            adultCheck = (from m in _db.AdultLogins where m.email.ToLower().Equals(EmailText.Text.ToLower()) select m).FirstOrDefault();


            // declare byte array that will hold the saltedHash of the user entered password
            // this will later be compared to the stored values to check if the login credentials are correct
            byte[] saltedHash;

            // if the email was found in the highschoollogin table, this code executes
            if (highschoolCheck != null)
            {
                // saltedHash is assigned the value of the salted hash that is returned by the called method
                saltedHash = GenerateSaltedHash(Encoding.UTF8.GetBytes(pw), Encoding.UTF8.GetBytes(highschoolCheck.passwordSalt));

                // if the byte array that is stored matches the byte array produced by the user input, this code executes
                if (CompareByteArrays(saltedHash, Convert.FromBase64String(highschoolCheck.passwordHash)))
                {
                    this.Visibility = Visibility.Hidden;

                    Information_Page ip = new Information_Page(highschoolCheck);
                    ip.ShowDialog();

                    this.Close();
                }

                // otherwise an error message is displayed
                else
                {
                    MessageBox.Show("Incorrect password.");
                }
            }


            // if the email is found in the adultlogins table, this code executes
            else if (adultCheck != null)
            {
                // salted hash is generated and assigned to saltedHash variable by calling a method
                saltedHash = GenerateSaltedHash(Encoding.UTF8.GetBytes(pw), Encoding.UTF8.GetBytes(adultCheck.passwordSalt));

                // if the byte arrays match, this code executes
                if (CompareByteArrays(saltedHash, Convert.FromBase64String(adultCheck.passwordHash)))
                {
                    this.Visibility = Visibility.Hidden;

                    Information_Page ip = new Information_Page(adultCheck);
                    ip.ShowDialog();

                    this.Close();
                }

                // otherwise, error message displays
                else
                {
                    MessageBox.Show("Incorrect password.");
                }
            }

            // if user is not found in either table, error message displays
            else
            {
                textFieldBoxes.DataContext = new BasicInfoTextValidation();

                MessageBox.Show("No account exists with that email.");
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

        #region Method to generate salted hash of user input for password comparison
        // method generates the salted hash that will be compared to the stored value
        static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }
        #endregion

        #region Method compares the stored values of the password for the user to the entered password
        // compares the byte arrays of the stored password and the entered password
        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
