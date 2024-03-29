﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Enrollment_Application
{
    public class HealthInfoTextValidation : IDataErrorInfo, INotifyPropertyChanged
    {
        #region Declare variables for the class
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
        #endregion

        #region String array that contains the values of the object that will be validated
        // string array that holds the values of all the variables declared above
        // they do not have underscores because it is technically a different variable whose value is given by the declared variables above
        private static readonly string[] ValidatedProperties = { "primaryPhysician", "pPhysicianPhoneNum", "oPhysicianPhoneNum", "diabeticType", "repPermissionForTreatment"};
        #endregion

        #region IDataErrorInfo.Error
        string IDataErrorInfo.Error { get { return null; } }
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

        #region Methods update values of the object of this class
        public void UpdateValues(HighSchoolHealthInfoClass hshi)
        {
            _primaryPhysician = hshi.primaryPhysician;
            _otherPhysician = hshi.otherPhysician;
            _pPhysicianPhoneNum = hshi.pPhysicianPhoneNum;
            _oPhysicianPhoneNum = hshi.oPhysicianPhoneNum;
            _diabeticType = hshi.diabeticType;
            _allergies = hshi.allergies;
            _heartIssues = hshi.heartIssues;
            _metabolic = bool.Parse(hshi.metabolic);
            _jointMuscle = bool.Parse(hshi.jointMuscle);
            _chronicIllness = bool.Parse(hshi.chronicIllness);
            _migraines = bool.Parse(hshi.migraines);
            _neurological = bool.Parse(hshi.neurological);
            _pulmonary = bool.Parse(hshi.pulmonary);
            _asthma = bool.Parse(hshi.asthma);
            _other = bool.Parse(hshi.other);
            _otherMeds = hshi.otherMeds;
            _specificFirstAidNeeds = hshi.specificFirstAidNeeds;
            _repPermissionForTreatment = hshi.repPermissionForTreatment;
        }

        public void UpdateValues(AdultHealthInfoClass ahi)
        {
            _primaryPhysician = ahi.primaryPhysician;
            _otherPhysician = ahi.otherPhysician;
            _pPhysicianPhoneNum = ahi.pPhysicianPhoneNum;
            _oPhysicianPhoneNum = ahi.oPhysicianPhoneNum;
            _diabeticType = ahi.diabeticType;
            _allergies = ahi.allergies;
            _heartIssues = ahi.heartIssues;
            _metabolic = bool.Parse(ahi.metabolic);
            _jointMuscle = bool.Parse(ahi.jointMuscle);
            _chronicIllness = bool.Parse(ahi.chronicIllness);
            _migraines = bool.Parse(ahi.migraines);
            _neurological = bool.Parse(ahi.neurological);
            _pulmonary = bool.Parse(ahi.pulmonary);
            _asthma = bool.Parse(ahi.asthma);
            _other = bool.Parse(ahi.other);
            _otherMeds = ahi.otherMeds;
            _specificFirstAidNeeds = ahi.specificFirstAidNeeds;
            _repPermissionForTreatment = ahi.repPermissionForTreatment;
        }
        #endregion
    }
}
