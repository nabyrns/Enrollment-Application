using Dapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Enrollment_Application
{


    public class AdultBasicInfoTextValidation : IDataErrorInfo, INotifyPropertyChanged
    {

        string IDataErrorInfo.Error { get { return null; } }

        #region Variable names
        // variable names that are used for initialization when creating a new instance of the AdultBasicInfoTextValidation class
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

        public int _Id;
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

        public int Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

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

                case "SSN":

                    string SSNpattern = @"^\d{3}-\d{2}-\d{4}$";

                    Regex reg = new Regex(SSNpattern);

                    if (string.IsNullOrWhiteSpace(SSN))
                    {
                        result = "Field cannot be empty.";
                    }

                    else if (!reg.IsMatch(SSN))
                    {
                        result = "Invalid Social Security Number.";
                    }

                    else if (SSN.Length > 11)
                    {
                        result = "Too many characters.";
                    }

                    else
                    {

                        List<AdultBasicInfoClass> ab;

                        using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("EnrollmentDB")))
                        {
                            ab = connection.Query<AdultBasicInfoClass>($"Select * From AdultBasicInfo Where SSN != 'NULL' And Id != '{ Id }'").ToList();

                            foreach (var v in ab)
                            {
                                if (SSN.Equals(v.SSN))
                                {
                                    result = "An account already exists with that SSN.";

                                    break;
                                }
                            }
                        }
                        
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

                    else
                    {
                        int dateResult = DateTime.Compare(Convert.ToDateTime(dateOfBirth), DateTime.Now.AddYears(-14));

                        if (!(dateResult < 0))
                        {
                            result = "Invalid date.";
                        }
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
        public List<string> IsValid
        {
            get
            {
                List<string> errorList = new List<string>();

                foreach (string s in ValidatedProperties)
                {
                    if (GetValidationError(s) != null)
                    {
                        errorList.Add(s);
                    }
                }

                return errorList;
            }

        }
        #endregion

        #region Method is for updating the values of an object of this class, used when trying to advance the form
        public void UpdateValues(AdultBasicInfoClass abi)
        {
            _lastName = abi.lastName;
            _firstName = abi.firstName;
            _middleInitial = abi.middleInitial;
            _program = abi.program;
            _streetAddress = abi.streetAddress;
            _city = abi.city;
            _state = abi.state;
            _zip = abi.zipCode;
            _primaryPhoneNum = abi.primaryPhoneNum;
            _cellPhoneNum = abi.cellPhoneNum;
            _hispanicOrLatino = abi.hispanicOrLatino;
            _race = abi.race;
            _gender = abi.gender;
            _dateOfBirth = abi.dateOfBirth;
            _completedEdLevel = abi.completedEdLevel;
            _attendedCollegeOrTech = abi.attendedCollegeOrTech;
            _liveWithParent = abi.liveWithParent;
            _SSN = abi.SSN;
            _Id = abi.Id;
        }
        #endregion
    }
}