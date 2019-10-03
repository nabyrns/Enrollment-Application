using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Enrollment_Application
{


    class AdultBasicInfoTextValidation : IDataErrorInfo, INotifyPropertyChanged
    {
        EnrollmentDBEntities _db = new EnrollmentDBEntities();

        string IDataErrorInfo.Error { get { return null; } }

        #region Variable names
        // variable names that are used for initialization when creating a new instance of the BasicInfoTextValidation class
        public string _lastName;
        public string _firstName;
        public string _middleInitial;
        public string _program;
        public string _streetAddress;
        public string _city;
        public string _state;
        public string _zip;
        public string _primaryPhoneNum;
        public string _cellPhoneNum;
        public string _hispanicOrLatino;
        public string _race;
        public string _gender;
        public Nullable<DateTime> _dateOfBirth;
        public string _completedEdLevel;
        public string _attendedCollegeOrTech;
        public string _liveWithParent;
        public string _SSN;
        #endregion

        // string array that holds the values of all the variables declared above
        // they do not have underscores because it is technically a different variable whose value is given by the declared variables above
        private static readonly string[] ValidatedProperties = { "completedEdLevel", "attendedCollegeOrTech", "liveWithParent", "lastName", "firstName", "middleInitial", "program", "streetAddress", "city", "state", "zipCode", "primaryPhoneNum", "cellPhoneNum", "hispanicOrLatino", "race", "gender", "dateOfBirth", "SSN" };

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

        #region These variables all get their values from the variables that were declared above and that are initialized at creation

        public string SSN
        {
            get
            {
                return _SSN;
            }

            set
            {
                _SSN = value;
            }
        }

        public string completedEdLevel
        {
            get
            {
                return _completedEdLevel;
            }

            set
            {
                _completedEdLevel = value;
            }
        }

        public string attendedCollegeOrTech
        {
            get
            {
                return _attendedCollegeOrTech;
            }

            set
            {
                _attendedCollegeOrTech = value;
            }
        }

        public string liveWithParent
        {
            get
            {
                return _liveWithParent;
            }

            set
            {
                _liveWithParent = value;
            }
        }

        public string lastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = value;
            }
        }

        public string firstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                _firstName = value;
            }
        }

        public string middleInitial
        {
            get
            {
                return _middleInitial;
            }

            set
            {
                _middleInitial = value;
            }
        }

        public string program
        {
            get
            {
                return _program;
            }

            set
            {
                _program = value;
            }
        }

        public string streetAddress
        {
            get
            {
                return _streetAddress;
            }

            set
            {
                _streetAddress = value;
            }
        }

        public string city
        {
            get
            {
                return _city;
            }

            set
            {
                _city = value;
            }
        }

        public string state
        {
            get
            {
                return _state;
            }

            set
            {
                _state = value;
            }
        }

        public string zipCode
        {
            get
            {
                return _zip;
            }

            set
            {
                _zip = value;
            }
        }

        public string primaryPhoneNum
        {
            get
            {
                return _primaryPhoneNum;
            }

            set
            {
                _primaryPhoneNum = value;
            }
        }

        public string cellPhoneNum
        {
            get
            {
                return _cellPhoneNum;
            }

            set
            {
                _cellPhoneNum = value;
            }
        }

        public string hispanicOrLatino
        {
            get
            {
                return _hispanicOrLatino;
            }

            set
            {
                _hispanicOrLatino = value;
            }
        }

        public string race
        {
            get
            {
                return _race;
            }

            set
            {
                _race = value;
            }
        }

        public string gender
        {
            get
            {
                return _gender;
            }

            set
            {
                _gender = value;
            }
        }

        public string dateOfBirth
        {
            get
            {
                return _dateOfBirth.ToString();
            }

            set
            {
                _dateOfBirth = Convert.ToDateTime(value);
            }
        }
        #endregion

        #region This region checks for errors in each variable above
        private string GetValidationError(string text)
        {
            string result = null;

            switch (text)
            {


                /* case "email":

                     string pattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                      + "@"
                                      + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

                     Regex r = new Regex(pattern);



                     if (string.IsNullOrWhiteSpace(email))
                     {
                         result = "Text field cannot be empty.";
                     }

                     else if (!r.IsMatch(email))
                     {
                         result = "Invalid email address.";
                     }

                     break;*/

                case "SSN":

                    if (string.IsNullOrWhiteSpace(SSN))
                    {
                        result = "Field cannot be empty.";
                    }

                    break;

                case "completedEdLevel":

                    if (string.IsNullOrWhiteSpace(completedEdLevel))
                    {
                        result = "Field cannot be empty.";
                    }

                    break;

                case "attendedCollegeOrTech":

                    if (string.IsNullOrWhiteSpace(attendedCollegeOrTech))
                    {
                        result = "Field cannot be empty.";
                    }

                    break;

                case "liveWithParent":

                    if (string.IsNullOrWhiteSpace(liveWithParent))
                    {
                        result = "Field cannot be empty.";
                    }

                    break;

                case "lastName":

                    if (string.IsNullOrWhiteSpace(lastName))
                    {
                        result = "Name field cannot be empty.";
                    }

                    break;

                case "firstName":

                    if (string.IsNullOrWhiteSpace(firstName))
                    {
                        result = "Name field cannot be empty.";
                    }

                    break;

                case "middleInitial":

                    if (string.IsNullOrWhiteSpace(middleInitial))
                    {
                        break;
                    }

                    if (middleInitial.Length > 5)
                    {
                        result = "Too many characters.";
                    }

                    break;

                case "program":

                    if (string.IsNullOrWhiteSpace(program))
                    {

                        result = "Please state the program you wish to attend.";
                    }

                    break;

                case "streetAddress":

                    if (string.IsNullOrWhiteSpace(streetAddress))
                    {

                        result = "Field must not be empty.";
                    }

                    break;

                case "city":

                    if (string.IsNullOrWhiteSpace(city))
                    {

                        result = "Field must not be empty.";
                    }

                    break;

                case "state":

                    if (string.IsNullOrWhiteSpace(state))
                    {

                        result = "Field must not be empty.";
                    }

                    break;

                case "zipCode":

                    if (string.IsNullOrWhiteSpace(zipCode))
                    {

                        result = "Field must not be empty.";
                    }

                    else if (!CommonMethods.IsZipCode(zipCode))
                    {

                        result = "Invalid ZipCode.";
                    }

                    break;

                case "primaryPhoneNum":

                    if (string.IsNullOrWhiteSpace(primaryPhoneNum))
                    {

                        result = "Field must not be empty.";
                    }

                    else if (!CommonMethods.IsPhoneNumber(primaryPhoneNum))
                    {

                        result = "Invalid phone number.";
                    }

                    break;

                case "cellPhoneNum":

                    if (string.IsNullOrWhiteSpace(cellPhoneNum))
                    {

                        result = "Field must not be empty.";
                    }

                    else if (!CommonMethods.IsPhoneNumber(cellPhoneNum))
                    {

                        result = "Invalid phone number.";
                    }

                    break;

                case "hispanicOrLatino":

                    if (string.IsNullOrWhiteSpace(hispanicOrLatino))
                    {

                        result = "Field must not be empty";
                    }

                    break;

                case "race":

                    if (string.IsNullOrWhiteSpace(race))
                    {

                        result = "Field must not be empty";
                    }

                    break;

                case "gender":

                    if (string.IsNullOrWhiteSpace(gender))
                    {

                        result = "Field must not be empty";
                    }

                    break;

                case "dateOfBirth":

                    if (string.IsNullOrWhiteSpace(dateOfBirth))
                    {

                        result = "Select a date.";
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