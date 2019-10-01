﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Enrollment_Application
{
    class HighSchoolECUCTextValidation : IDataErrorInfo, INotifyPropertyChanged
    {
        public string _parentNameOne;
        public string _parentOneRelationship;
        public string _parentOneAddress;
        public string _parentOneCity;
        public string _parentOneState;
        public string _parentOneZip;
        public string _parentOnePrimaryNum;
        public string _parentOneCellNum;
        public string _parentOneEmail;
        public string _parentNameTwo;
        public string _parentTwoRelationship;
        public string _parentTwoAddress;
        public string _parentTwoCity;
        public string _parentTwoState;
        public string _parentTwoZip;
        public string _parentTwoPrimaryNum;
        public string _parentTwoCellNum;
        public string _parentTwoEmail;
        public string _eContactName;
        public string _eContactRelationship;
        public string _eContactPrimaryNum;
        public string _eContactCellNum;
        public string _residesWithP1;
        public string _residesWithP2;

        // string array that holds the values of all the variables declared above
        // they do not have underscores because it is technically a different variable whose value is given by the declared variables above
        private static readonly string[] ValidatedProperties = { "parentNameOne", "parentOneRelationship", "parentOneAddress", "parentOneCity", "parentOneState", "parentOneZip",
                                                                 "parentOnePrimaryNum", "parentOneCellNum", "parentOneEmail", "parentNameTwo", "parentTwoRelationship", "parentTwoAddress",
                                                                 "parentTwoCity", "parentTwoState", "parentTwoZip", "parentTwoPrimaryNum", "parentTwoCellNum", "parentTwoEmail",
                                                                 "EContactName", "EContactRelationship", "EContactPrimaryNum", "EContactCellNum", "residesWithP1", "residesWithP2" };

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
        public string parentNameOne
        {
            get
            {
                return _parentNameOne;
            }

            set
            {
                _parentNameOne = value;
            }
        }

        public string parentOneRelationship
        {
            get
            {
                return _parentOneRelationship;
            }

            set
            {
                _parentOneRelationship = value;
            }
        }

        public string parentOneAddress
        {
            get
            {
                return _parentOneAddress;
            }

            set
            {
                _parentOneAddress = value;
            }
        }

        public string parentOneCity
        {
            get
            {
                return _parentOneCity;
            }

            set
            {
                _parentOneCity = value;
            }
        }

        public string parentOneState
        {
            get
            {
                return _parentOneState;
            }

            set
            {
                _parentOneState = value;
            }
        }

        public string parentOneZip
        {
            get
            {
                return _parentOneZip;
            }

            set
            {
                _parentOneZip = value;
            }
        }

        public string parentOnePrimaryNum
        {
            get
            {
                return _parentOnePrimaryNum;
            }

            set
            {
                _parentOnePrimaryNum = value;
            }
        }

        public string parentOneCellNum
        {
            get
            {
                return _parentOneCellNum;
            }

            set
            {
                _parentOneCellNum = value;
            }
        }

        public string parentOneEmail
        {
            get
            {
                return _parentOneEmail;
            }

            set
            {
                _parentOneEmail = value;
            }
        }

        public string parentNameTwo
        {
            get
            {
                return _parentNameTwo;
            }

            set
            {
                _parentNameTwo = value;
            }
        }

        public string parentTwoRelationship
        {
            get
            {
                return _parentTwoRelationship;
            }

            set
            {
                _parentTwoRelationship = value;
            }
        }

        public string parentTwoAddress
        {
            get
            {
                return _parentTwoAddress;
            }

            set
            {
                _parentTwoAddress = value;
            }
        }

        public string parentTwoCity
        {
            get
            {
                return _parentTwoCity;
            }

            set
            {
                _parentTwoCity = value;
            }
        }

        public string parentTwoState
        {
            get
            {
                return _parentTwoState;
            }

            set
            {
                _parentTwoState = value;
            }
        }

        public string parentTwoZip
        {
            get
            {
                return _parentTwoZip;
            }

            set
            {
                _parentTwoZip = value;
            }
        }

        public string parentTwoPrimaryNum
        {
            get
            {
                return _parentTwoPrimaryNum;
            }

            set
            {
                _parentTwoPrimaryNum = value;
            }
        }

        public string parentTwoCellNum
        {
            get
            {
                return _parentTwoCellNum;
            }

            set
            {
                _parentTwoCellNum = value;
            }
        }

        public string parentTwoEmail
        {
            get
            {
                return _parentTwoEmail;
            }

            set
            {
                _parentTwoEmail = value;
            }
        }

        public string EContactName
        {
            get
            {
                return _eContactName;
            }

            set
            {
                _eContactName = value;
            }
        }

        public string EContactRelationship
        {
            get
            {
                return _eContactRelationship;
            }

            set
            {
                _eContactRelationship = value;
            }
        }

        public string EContactPrimaryNum
        {
            get
            {
                return _eContactPrimaryNum;
            }

            set
            {
                _eContactPrimaryNum = value;
            }
        }

        public string EContactCellNum
        {
            get
            {
                return _eContactCellNum;
            }

            set
            {
                _eContactCellNum = value;
            }
        }

        public string residesWithP1
        {
            get
            {
                return _residesWithP1;
            }

            set
            {
                _residesWithP1 = value;
            }
        }

        public string residesWithP2
        {
            get
            {
                return _residesWithP2;
            }

            set
            {
                _residesWithP2 = value;
            }
        }
        #endregion

        #region This region checks for errors in each variable above
        private string GetValidationError(string text)
        {
            string result = null;

            switch (text)
            {

                case "parentNameOne":

                    if (string.IsNullOrWhiteSpace(parentNameOne))
                    {
                        result = "Field cannot be empty.";
                    }

                    break;

                case "parentOneRelationship":

                    if (string.IsNullOrWhiteSpace(parentOneRelationship))
                    {
                        result = "Field cannot be empty.";
                    }

                    break;

                case "parentOneAddress":

                    if (string.IsNullOrWhiteSpace(parentOneAddress))
                    {

                        result = "Field must not be empty.";
                    }

                    break;

                case "parentOneCity":

                    if (string.IsNullOrWhiteSpace(parentOneCity))
                    {

                        result = "Field must not be empty.";
                    }

                    break;

                case "parentOneState":

                    if (string.IsNullOrWhiteSpace(parentOneState))
                    {
                        result = "Field cannot be empty.";
                    }

                    break;

                case "parentOneZip":

                    if (string.IsNullOrWhiteSpace(parentOneZip))
                    {

                        result = "Field must not be empty.";
                    }

                    else if (!CommonMethods.IsZipCode(parentOneZip))
                    {

                        result = "Invalid ZipCode.";
                    }

                    break;

                case "parentOnePrimaryNum":

                    if (string.IsNullOrWhiteSpace(parentOnePrimaryNum))
                    {

                        result = "Field must not be empty.";
                    }

                    else if (!CommonMethods.IsPhoneNumber(parentOnePrimaryNum))
                    {

                        result = "Invalid phone number.";
                    }

                    break;

                case "parentOneCellNum":

                    if (string.IsNullOrWhiteSpace(parentOneCellNum))
                    {

                        result = "Field must not be empty.";
                    }

                    else if (!CommonMethods.IsPhoneNumber(parentOneCellNum))
                    {

                        result = "Invalid phone number.";
                    }

                    break;

                /*case "parentNameTwo":

                    if (string.IsNullOrWhiteSpace(parentNameTwo))
                    {
                        result = "Field cannot be empty.";
                    }

                    break;

                case "parentTwoRelationship":

                    if (string.IsNullOrWhiteSpace(parentTwoRelationship))
                    {
                        result = "Field cannot be empty.";
                    }

                    break;

                case "parentTwoAddress":

                    if (string.IsNullOrWhiteSpace(parentTwoAddress))
                    {

                        result = "Field must not be empty.";
                    }

                    break;

                case "parentTwoCity":

                    if (string.IsNullOrWhiteSpace(parentTwoCity))
                    {

                        result = "Field must not be empty.";
                    }

                    break;

                case "parentTwoState":

                    if (string.IsNullOrWhiteSpace(parentTwoState))
                    {
                        result = "Field cannot be empty.";
                    }

                    break;*/

                case "parentTwoZip":

                    if (!string.IsNullOrWhiteSpace(parentTwoZip))
                    {
                        if (!CommonMethods.IsZipCode(parentTwoZip))
                        {
                            result = "Invalid ZipCode.";
                        }
                    }

                    break;

                case "parentTwoPrimaryNum":

                    if (!string.IsNullOrWhiteSpace(parentTwoPrimaryNum))
                    {
                        if (!CommonMethods.IsPhoneNumber(parentTwoPrimaryNum))
                        {
                            result = "Invalid phone number.";
                        }
                    }

                    break;

                case "parentTwoCellNum":

                    if (!string.IsNullOrWhiteSpace(parentTwoCellNum))
                    {
                        if (!CommonMethods.IsPhoneNumber(parentTwoCellNum))
                        {
                            result = "Invalid phone number.";
                        }
                    }

                    break;

                case "EContactName":

                    if (string.IsNullOrWhiteSpace(EContactName))
                    {
                        result = "Field must not be empty.";
                    }

                    break;

                case "EContactRelationship":

                    if (string.IsNullOrWhiteSpace(EContactRelationship))
                    {
                        result = "Field must not be empty.";
                    }

                    break;

                case "EContactPrimaryNum":

                    if (string.IsNullOrWhiteSpace(EContactPrimaryNum))
                    {

                        result = "Field must not be empty.";
                    }

                    else if (!CommonMethods.IsPhoneNumber(EContactPrimaryNum))
                    {

                        result = "Invalid phone number.";
                    }

                    break;

                case "EContactCellNum":

                    if (string.IsNullOrWhiteSpace(EContactCellNum))
                    {

                        result = "Field must not be empty.";
                    }

                    else if (!CommonMethods.IsPhoneNumber(EContactCellNum))
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