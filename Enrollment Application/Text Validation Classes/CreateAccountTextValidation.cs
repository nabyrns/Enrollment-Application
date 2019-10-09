using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Enrollment_Application
{
    class CreateAccountTextValidation : IDataErrorInfo, INotifyPropertyChanged
    {
        EnrollmentDBEntities _db = new EnrollmentDBEntities();

        string IDataErrorInfo.Error { get { return null; } }

        #region Property Changed stuff
        // property changed event handler
        public event PropertyChangedEventHandler PropertyChanged;

        // look up how this works exactly
        private void OnPropertyChanged(string p)
        {
            PropertyChangedEventHandler ph = PropertyChanged;

            if (ph != null)
            {
                ph(this, new PropertyChangedEventArgs(p));
            }
        }
        #endregion

        public string _email;
        public string _studentType;

        // string array that holds the values of all the variables declared above
        // they do not have underscores because it is technically a different variable whose value is given by the declared variables above
        private static readonly string[] ValidatedProperties = { "email", "studentType" };

        #region Variables that get values from above values
        public string email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
            }
        }

        public string studentType
        {
            get
            {
                return _studentType;
            }

            set
            {
                _studentType = value;
            }
        }
        #endregion

        #region This region checks for errors in each variable above
        private string GetValidationError(string text)
        {
            string result = null;

            switch (text)
            {
                case "email":

                    var check = (from m in _db.HighSchoolLogins where m.email.ToLower().Equals(email.ToLower()) select m).FirstOrDefault();
                    var checkTwo = (from n in _db.AdultLogins where n.email.ToLower().Equals(email.ToLower()) select n).FirstOrDefault();

                    if (string.IsNullOrWhiteSpace(email))
                    {
                        result = "Field must not be empty.";
                    }

                    else if (check != null || checkTwo != null)
                    {
                        result = "Account already exists with that email.";
                    }

                    else if (!CommonMethods.IsEmail(email))
                    {
                        result = "Invalid email.";
                    }

                    else if (email.Length >= 50)
                    {
                        result = "Too many characters.";
                    }

                    break;

                case "studentType":

                    if (string.IsNullOrWhiteSpace(studentType))
                    {
                        result = "Field must not be empty.";
                    }

                    break;
            }

            return result;
        }

        string IDataErrorInfo.this[string text]
        {
            get
            {
                return GetValidationError(text);
            }
        }

        #endregion

        #region Method checks if there are no errors returned in any of the variables being checked
        public bool IsValid
        {
            get
            {
                foreach (string s in ValidatedProperties)
                {
                    if (GetValidationError(s) != null)
                    {
                        return false;
                    }
                }

                return true;
            }

        }
        #endregion
    }
}
