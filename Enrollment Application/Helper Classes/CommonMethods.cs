using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Enrollment_Application
{
    public class CommonMethods
    {
        static EnrollmentDBEntities _db = new EnrollmentDBEntities();

        public static bool IsPhoneNumber(string phoneNumber)
        {
            string PhonePattern = @"^\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$";

            Regex reg = new Regex(PhonePattern);

            if (!reg.IsMatch(phoneNumber))
            {

                return false;
            }

            return true;
        }

        public static bool IsEmail(string email)
        {
            string pattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                      + "@"
                                      + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

            Regex r = new Regex(pattern);

            if (!r.IsMatch(email))
            {
                return false;
            }

            return true;
        }

        public static bool IsZipCode(string zip)
        {
            string zipPattern = "^[0-9]{5}(?:-[0-9]{4})?$";

            Regex reg = new Regex(zipPattern);

            if (!reg.IsMatch(zip))
            {
                return false;
            }

            return true;
        }

        #region Method to generate salted hash of user input for password comparison
        // method generates the salted hash that will be compared to the stored value
        public static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
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

        #region Method that generates new salt to be used in storing of the password
        public static String CreateSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[size];

            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }
        #endregion
    }
}
