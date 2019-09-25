using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
        public string _metabolic;
        public string _jointMuscle;
        public string _chronicIllness;
        public string _migraines;
        public string _neurological;
        public string _pulmonary;
        public string _asthma;
        public string _other;
        public string _otherMeds;
        public string _specificFirstAidNeeds;
        public string _repPermissionForTreatment;

        // private static readonly string[] ValidatedProperties = { "primaryPhysician", "otherPhysician", "pPhysicianPhoneNum", "oPhysicianPhoneNum", "diabeticType", "allergies", "heartIssues", "metabolic", "jointMuscle", "chronicIllness", "migraines", "neurological", "pulmonary", "asthma", "other", "otherMeds", "specificFirstAidNeeds", "repPermissionForTreatment" };

        private static readonly string[] ValidatedProperties = { "primaryPhysician", "pPhysicianPhoneNum", "diabeticType", "repPermissionForTreatment"};

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

        /*public string otherPhysician
        {
            get
            {
                return _otherPhysician;
            }

            set
            {
                _otherPhysician = value;
            }
        }*/

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

        /*public string oPhysicianPhoneNum
        {
            get
            {
                return _oPhysicianPhoneNum;
            }

            set
            {
                _oPhysicianPhoneNum = value;
            }
        }*/

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

        /*public string allergies
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

        public string metabolic
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

        public string jointMuscle
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

        public string chronicIllness
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

        public string migraines
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

        public string neurological
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

        public string pulmonary
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

        public string asthma
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

        public string other
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
        }*/

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

                    break;

                case "pPhysicianPhoneNum":

                    if (string.IsNullOrWhiteSpace(pPhysicianPhoneNum))
                    {
                        result = "Field cannot be empty.";
                    }

                    break;

                case "diabeticType":

                    if (string.IsNullOrWhiteSpace(diabeticType))
                    {
                        result = "Field cannot be empty.";
                    }

                    else if (string.IsNullOrEmpty(diabeticType))
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
