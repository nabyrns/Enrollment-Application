using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Enrollment_Application
{
    class AdultECUCTextValidation : IDataErrorInfo, INotifyPropertyChanged
    {
        public string _contactName;
        public string _relationship;
        public string _primaryNum;
        public string _alternateNum;
        public string _nameNearestRelative;
        public string _NRrelationship;
        public string _NRstreetAddress;
        public string _NRcity;
        public string _NRstate;
        public string _NRzip;
        public string _NRprimaryNum;
        public string _NRworkNum;
        public string _NRcellNum;

        // string array that holds the values of all the variables declared above
        // they do not have underscores because it is technically a different variable whose value is given by the declared variables above
        private static readonly string[] ValidatedProperties = { "contactName", "relationship", "primaryNum", "alternateNum", "nameNearestRelative", "NRrelationship", "NRstreetAddress", "NRcity", "NRstate", "NRzip", "NRprimaryNum", "NRworkNum", "NRcellNum"};

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


        #region These variables all get their values from the variables that were declared above and that are initialized at creation
        public string contactName
        {
            get
            {
                return _contactName;
            }

            set
            {
                _contactName = value;
            }
        }

        public string relationship
        {
            get
            {
                return _relationship;
            }

            set
            {
                _relationship = value;
            }
        }

        public string primaryNum
        {
            get
            {
                return _primaryNum;
            }

            set
            {
                _primaryNum = value;
            }
        }

        public string alternateNum
        {
            get
            {
                return _alternateNum;
            }

            set
            {
                _alternateNum = value;
            }
        }

        public string nameNearestRelative
        {
            get
            {
                return _nameNearestRelative;
            }

            set
            {
                _nameNearestRelative = value;
            }
        }

        public string NRrelationship
        {
            get
            {
                return _NRrelationship;
            }

            set
            {
                _NRrelationship = value;
            }
        }

        public string NRstreetAddress
        {
            get
            {
                return _NRstreetAddress;
            }

            set
            {
                _NRstreetAddress = value;
            }
        }

        public string NRcity
        {
            get
            {
                return _NRcity;
            }

            set
            {
                _NRcity = value;
            }
        }

        public string NRstate
        {
            get
            {
                return _NRstate;
            }

            set
            {
                _NRstate = value;
            }
        }

        public string NRzip
        {
            get
            {
                return _NRzip;
            }

            set
            {
                _NRzip = value;
            }
        }

        public string NRprimaryNum
        {
            get
            {
                return _NRprimaryNum;
            }

            set
            {
                _NRprimaryNum = value;
            }
        }

        public string NRworkNum
        {
            get
            {
                return _NRworkNum;
            }

            set
            {
                _NRworkNum = value;
            }
        }

        public string NRcellNum
        {
            get
            {
                return _NRcellNum;
            }

            set
            {
                _NRcellNum = value;
            }
        }
        #endregion

        #region This region checks for errors in each variable above
        private string GetValidationError(string text)
        {
            string result = null;

            switch (text)
            {

                case "contactName":

                    if (string.IsNullOrWhiteSpace(contactName))
                    {
                        result = "Field cannot be empty.";
                    }

                    else if (contactName.Length >= 50)
                    {
                        result = "Too many characters.";
                    }

                    break;

                case "relationship":

                    if (string.IsNullOrWhiteSpace(relationship))
                    {
                        result = "Field cannot be empty.";
                    }

                    else if (relationship.Length >= 50)
                    {
                        result = "Too many characters.";
                    }

                    break;

                case "primaryNum":

                    if (string.IsNullOrWhiteSpace(primaryNum))
                    {

                        result = "Field must not be empty.";
                    }

                    else if (!CommonMethods.IsPhoneNumber(primaryNum))
                    {

                        result = "Invalid phone number.";
                    }

                    break;

                case "alternateNum":

                    if (string.IsNullOrWhiteSpace(alternateNum))
                    {

                        result = "Field must not be empty.";
                    }

                    else if (!CommonMethods.IsPhoneNumber(alternateNum))
                    {

                        result = "Invalid phone number.";
                    }

                    break;

                case "nameNearestRelative":

                    if (string.IsNullOrWhiteSpace(nameNearestRelative))
                    {
                        result = "Field cannot be empty.";
                    }

                    else if (nameNearestRelative.Length >= 50)
                    {
                        result = "Too many characters.";
                    }

                    break;

                case "NRrelationship":

                    if (string.IsNullOrWhiteSpace(NRrelationship))
                    {

                        result = "Field cannot be empty.";
                    }

                    else if (NRrelationship.Length >= 50)
                    {
                        result = "Too many characters.";
                    }

                    break;

                case "NRstreetAddress":

                    if (string.IsNullOrWhiteSpace(NRstreetAddress))
                    {

                        result = "Field cannot not be empty.";
                    }

                    else if (NRstreetAddress.Length >= 50)
                    {
                        result = "Too many characters.";
                    }

                    break;

                case "NRcity":

                    if (string.IsNullOrWhiteSpace(NRcity))
                    {

                        result = "Field cannot not be empty.";
                    }

                    else if (NRcity.Length >= 50)
                    {
                        result = "Too many characters.";
                    }

                    break;

                case "NRstate":

                    if (string.IsNullOrWhiteSpace(NRstate))
                    {

                        result = "Field cannot not be empty.";
                    }

                    break;

                case "NRzip":

                    if (string.IsNullOrWhiteSpace(NRzip))
                    {

                        result = "Field must not be empty.";
                    }

                    else if (!CommonMethods.IsZipCode(NRzip))
                    {

                        result = "Invalid ZipCode.";
                    }

                    break;

                case "NRprimaryNum":

                    if (string.IsNullOrWhiteSpace(NRprimaryNum))
                    {

                        result = "Field must not be empty.";
                    }

                    else if (!CommonMethods.IsPhoneNumber(NRprimaryNum))
                    {

                        result = "Invalid phone number.";
                    }

                    break;

                case "NRworkNum":

                    if (string.IsNullOrWhiteSpace(NRworkNum))
                    {

                        result = "Field must not be empty.";
                    }

                    else if (!CommonMethods.IsPhoneNumber(NRworkNum))
                    {

                        result = "Invalid phone number.";
                    }

                    break;

                case "NRcellNum":

                    if (string.IsNullOrWhiteSpace(NRcellNum))
                    {

                        result = "Field must not be empty.";
                    }

                    else if (!CommonMethods.IsPhoneNumber(NRcellNum))
                    {

                        result = "Invalid phone number.";
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
