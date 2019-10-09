using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Enrollment_Application
{
    class HealthInfoTextValidation : IDataErrorInfo, INotifyPropertyChanged
    {
        public string _primaryPhysician;
        public string _otherPhysician;
        public string _pPhysicianPhoneNum;
        public string _oPhysicianPhoneNum;
        public string _diabeticType;
        public string _allergies;
        public string _heartIssues;
        public bool _metabolic;
        public bool _jointMuscle;
        public bool _chronicIllness;
        public bool _migraines;
        public bool _neurological;
        public bool _pulmonary;
        public bool _asthma;
        public bool _other;
        public string _otherMeds;
        public string _specificFirstAidNeeds;
        public string _repPermissionForTreatment;

        // string array that holds the values of all the variables declared above
        // they do not have underscores because it is technically a different variable whose value is given by the declared variables above
        private static readonly string[] ValidatedProperties = { "primaryPhysician", "pPhysicianPhoneNum", "oPhysicianPhoneNum", "diabeticType", "repPermissionForTreatment"};

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
        public string primaryPhysician
        {
            get
            {
                return _primaryPhysician;
            }

            set
            {
                _primaryPhysician = value;
            }
        }

        public string otherPhysician
        {
            get
            {
                return _otherPhysician;
            }

            set
            {
                _otherPhysician = value;
            }
        }

        public string pPhysicianPhoneNum
        {
            get
            {
                return _pPhysicianPhoneNum;
            }

            set
            {
                _pPhysicianPhoneNum = value;
            }
        }

        public string oPhysicianPhoneNum
        {
            get
            {
                return _oPhysicianPhoneNum;
            }

            set
            {
                _oPhysicianPhoneNum = value;
            }
        }

        public string diabeticType
        {
            get
            {
                return _diabeticType;
            }

            set
            {
                _diabeticType = value;
            }
        }

        public string allergies
        {
            get
            {
                return _allergies;
            }

            set
            {
                _allergies = value;
            }
        }

        public string heartIssues
        {
            get
            {
                return _heartIssues;
            }

            set
            {
                _heartIssues = value;
            }
        }

        public bool metabolic
        {
            get
            {
                return _metabolic;
            }

            set
            {
                _metabolic = value;
            }
        }

        public bool jointMuscle
        {
            get
            {
                return _jointMuscle;
            }

            set
            {
                _jointMuscle = value;
            }
        }

        public bool chronicIllness
        {
            get
            {
                return _chronicIllness;
            }

            set
            {
                _chronicIllness = value;
            }
        }

        public bool migraines
        {
            get
            {
                return _migraines;
            }

            set
            {
                _migraines = value;
            }
        }

        public bool neurological
        {
            get
            {
                return _neurological;
            }

            set
            {
                _neurological = value;
            }
        }

        public bool pulmonary
        {
            get
            {
                return _pulmonary;
            }

            set
            {
                _pulmonary = value;
            }
        }

        public bool asthma
        {
            get
            {
                return _asthma;
            }

            set
            {
                _asthma = value;
            }
        }

        public bool other
        {
            get
            {
                return _other;
            }

            set
            {
                _other = value;
            }
        }

        public string otherMeds
        {
            get
            {
                return _otherMeds;
            }

            set
            {
                _otherMeds = value;
            }
        }

        public string specificFirstAidNeeds
        {
            get
            {
                return _specificFirstAidNeeds;
            }

            set
            {
                _specificFirstAidNeeds = value;
            }
        }

        public string repPermissionForTreatment
        {
            get
            {
                return _repPermissionForTreatment;
            }

            set
            {
                _repPermissionForTreatment = value;
            }
        }
        #endregion

        #region This region checks for errors in each variable above
        private string GetValidationError(string text)
        {
            string result = null;

            switch (text)
            {

                case "primaryPhysician":

                    if (string.IsNullOrWhiteSpace(primaryPhysician))
                    {
                        result = "Field cannot be empty.";
                    }

                    else if (primaryPhysician.Length >= 50)
                    {
                        result = "Too many characters.";
                    }

                    break;

                case "pPhysicianPhoneNum":

                    string PhonePattern = @"^\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$";

                    Regex reg = new Regex(PhonePattern);

                    if (string.IsNullOrWhiteSpace(pPhysicianPhoneNum))
                    {

                        result = "Field cannot be empty.";
                    }

                    else if (!reg.IsMatch(pPhysicianPhoneNum))
                    {

                        result = "Invalid phone number.";
                    }

                    break;

                case "oPhysicianPhoneNum":

                    string oPhonePattern = @"^\(?\d{3}\)?[- ]?\d{3}[- ]?\d{4}$";

                    Regex reg2 = new Regex(oPhonePattern);

                    if (!reg2.IsMatch(oPhysicianPhoneNum) && !string.IsNullOrWhiteSpace(oPhysicianPhoneNum))
                    {

                        result = "Invalid phone number.";
                    }

                    break;

                case "diabeticType":

                    if (string.IsNullOrWhiteSpace(diabeticType))
                    {
                        result = "Field cannot be empty.";
                    }

                    break;

                case "repPermissionForTreatment":

                    if (string.IsNullOrWhiteSpace(repPermissionForTreatment))
                    {

                        result = "Field cannot be empty.";
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
