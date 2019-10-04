using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment_Application
{
    class PolicyTextValidation : IDataErrorInfo, INotifyPropertyChanged
    {
        public bool _attendance;
        public bool _tobacco;
        public bool _internetAccess;
        public bool _studentInsurance;
        public bool _fieldTrips;
        public bool _drugTesting;
        public bool _noticeOfDisclosures;
        public bool _cellPhoneContact;
        public bool _releaseForPhotography;

        // string array that holds the values of all the variables declared above
        // they do not have underscores because it is technically a different variable whose value is given by the declared variables above
        private static readonly string[] ValidatedProperties = { "attendance", "tobacco", "internetAccess", "studentInsurance", "fieldTrips", "drugTesting", "noticeOfDisclosures" };

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

        public bool attendance
        {
            get
            {
                return _attendance;
            }

            set
            {
                _attendance = value;
            }
        }

        public bool tobacco
        {
            get
            {
                return _tobacco;
            }

            set
            {
                _tobacco = value;
            }
        }

        public bool internetAccess
        {
            get
            {
                return _internetAccess;
            }

            set
            {
                _internetAccess = value;
            }
        }

        public bool studentInsurance
        {
            get
            {
                return _studentInsurance;
            }

            set
            {
                _studentInsurance = value;
            }
        }

        public bool fieldTrips
        {
            get
            {
                return _fieldTrips;
            }

            set
            {
                _fieldTrips = value;
            }
        }

        public bool drugTesting
        {
            get
            {
                return _drugTesting;
            }

            set
            {
                _drugTesting = value;
            }
        }

        public bool noticeOfDisclosures
        {
            get
            {
                return _noticeOfDisclosures;
            }

            set
            {
                _noticeOfDisclosures = value;
            }
        }

        public bool cellPhoneContact
        {
            get
            {
                return _cellPhoneContact;
            }

            set
            {
                _cellPhoneContact = value;
            }
        }

        public bool releaseForPhotography
        {
            get
            {
                return _releaseForPhotography;
            }

            set
            {
                _releaseForPhotography = value;
            }
        }
        #endregion

        #region This region checks for errors in each variable above
        private string GetValidationError(string text)
        {
            string result = null;

            switch (text)
            {

                case "attendance":

                    if (!attendance)
                    {
                        result = "Box must be checked";
                    }

                    break;

                case "tobacco":

                    if (!tobacco)
                    {
                        result = "Box must be checked";
                    }

                    break;

                case "internetAccess":

                    if (!internetAccess)
                    {
                        result = "Box must be checked";
                    }

                    break;

                case "studentInsurance":

                    if (!studentInsurance)
                    {
                        result = "Box must be checked";
                    }

                    break;

                case "fieldTrips":

                    if (!fieldTrips)
                    {
                        result = "Box must be checked";
                    }

                    break;

                case "drugTesting":

                    if (!drugTesting)
                    {
                        result = "Box must be checked";
                    }

                    break;

                case "noticeOfDisclosures":

                    if (!noticeOfDisclosures)
                    {
                        result = "Box must be checked";
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
