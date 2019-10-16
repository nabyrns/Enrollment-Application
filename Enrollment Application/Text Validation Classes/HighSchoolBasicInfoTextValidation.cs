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


    class HighSchoolBasicInfoTextValidation : IDataErrorInfo, INotifyPropertyChanged
    {
        #region IDataErrorInfo.Error
        string IDataErrorInfo.Error { get { return null; } }
        #endregion

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
        public string _currentEdLevel;
        public string _sendingHS;
        #endregion

        #region String array that holds the values of the variables to be validated later
        // string array that holds the values of all the variables declared above
        // they do not have underscores because it is technically a different variable whose value is given by the declared variables above
        private static readonly string[] ValidatedProperties = { "currentEdLevel", "sendingHS", "lastName", "firstName", "middleInitial", "program", "streetAddress", "city", "state", "zipCode", "primaryPhoneNum", "cellPhoneNum", "hispanicOrLatino", "race", "gender", "dateOfBirth" };
        #endregion

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

        public string currentEdLevel
        {
            get
            {
                return _currentEdLevel;
            }

            set
            {
                _currentEdLevel = value;
            }
        }

        public string sendingHS
        {
            get
            {
                return _sendingHS;
            }

            set
            {
                _sendingHS = value;
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

                case "currentEdLevel":

                    if (string.IsNullOrWhiteSpace(currentEdLevel))
                    {
                        result = "Field cannot be empty.";
                    }

                    break;

                case "sendingHS":

                    if (string.IsNullOrWhiteSpace(sendingHS))
                    {
                        result = "Field cannot be empty.";
                    }

                    else if (sendingHS.Length >= 50)
                    {
                        result = "Too many characters.";
                    }

                    break;

                case "lastName":

                    if (string.IsNullOrWhiteSpace(lastName))
                    {
                        result = "Name field cannot be empty.";
                    }

                    else if (lastName.Length >= 50)
                    {
                        result = "Too many characters.";
                    }

                    break;

                case "firstName":

                    if (string.IsNullOrWhiteSpace(firstName))
                    {
                        result = "Name field cannot be empty.";
                    }

                    else if (firstName.Length >= 50)
                    {
                        result = "Too many characters.";
                    }

                    break;

                case "middleInitial":

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

                    else if (program.Length >= 50)
                    {
                        result = "Too many characters.";
                    }

                    break;

                case "streetAddress":

                    if (string.IsNullOrWhiteSpace(streetAddress))
                    {

                        result = "Field must not be empty.";
                    }

                    else if (streetAddress.Length >= 50)
                    {
                        result = "Too many characters.";
                    }

                    break;

                case "city":

                    if (string.IsNullOrWhiteSpace(city))
                    {

                        result = "Field must not be empty.";
                    }

                    else if (city.Length >= 50)
                    {
                        result = "Too many characters.";
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

        #region Method updates the values of the object of this class
        public void UpdateValues(HighSchoolBasicInfoClass hbi)
        {
            _lastName = hbi.lastName;
            _firstName = hbi.firstName;
            _middleInitial = hbi.middleInitial;
            _program = hbi.program;
            _streetAddress = hbi.streetAddress;
            _city = hbi.city;
            _state = hbi.state;
            _zip = hbi.zipCode;
            _primaryPhoneNum = hbi.primaryPhoneNum;
            _cellPhoneNum = hbi.cellPhoneNum;
            _hispanicOrLatino = hbi.hispanicOrLatino;
            _race = hbi.race;
            _gender = hbi.gender;
            _dateOfBirth = hbi.dateOfBirth;
            _currentEdLevel = hbi.currentEdLevel;
            _sendingHS = hbi.sendingHS;
        }
        #endregion
    }
}