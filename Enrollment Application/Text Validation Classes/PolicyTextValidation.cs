using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment_Application
{
    public class PolicyTextValidation : IDataErrorInfo, INotifyPropertyChanged
    {
        #region Declare variables of the class
        public bool _attendance;
        public bool _tobacco;
        public bool _internetAccess;
        public bool _studentInsurance;
        public bool _fieldTrips;
        public bool _drugTesting;
        public bool _noticeOfDisclosures;
        public bool _cellPhoneContact;
        public bool _releaseForPhotography;
        #endregion

        #region String array that holds the values that will be validated
        // string array that holds the values of all the variables declared above
        // they do not have underscores because it is technically a different variable whose value is given by the declared variables above
        private static readonly string[] ValidatedProperties = { "attendance", "tobacco", "internetAccess", "studentInsurance", "fieldTrips", "drugTesting", "noticeOfDisclosures" };
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

        #region Methods update the values of the object of this class
        public void UpdateValues(AdultPolicyClass ap)
        {
            _attendance = bool.Parse(ap.attendance);
            _tobacco = bool.Parse(ap.tobacco);
            _internetAccess = bool.Parse(ap.internetAccess);
            _studentInsurance = bool.Parse(ap.studentInsurance);
            _fieldTrips = bool.Parse(ap.fieldTrips);
            _drugTesting = bool.Parse(ap.drugTesting);
            _noticeOfDisclosures = bool.Parse(ap.noticeOfDisclosures);
            _cellPhoneContact = bool.Parse(ap.cellPhoneContact);
            _releaseForPhotography = bool.Parse(ap.releaseForPhotography);
        }

        public void UpdateValues(HighSchoolPolicyClass hsp)
        {
            _attendance = bool.Parse(hsp.attendance);
            _tobacco = bool.Parse(hsp.tobacco);
            _internetAccess = bool.Parse(hsp.internetAccess);
            _studentInsurance = bool.Parse(hsp.studentInsurance);
            _fieldTrips = bool.Parse(hsp.fieldTrips);
            _drugTesting = bool.Parse(hsp.drugTesting);
            _noticeOfDisclosures = bool.Parse(hsp.noticeOfDisclosures);
            _cellPhoneContact = bool.Parse(hsp.cellPhoneContact);
            _releaseForPhotography = bool.Parse(hsp.releaseForPhotography);
        }
        #endregion
    }
}
