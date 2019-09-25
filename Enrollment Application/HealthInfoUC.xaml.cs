using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Enrollment_Application
{
    /// <summary>
    /// Interaction logic for HealthInfoUC.xaml
    /// </summary>
    public partial class HealthInfoUC : UserControl
    {
        EnrollmentDBEntities _db = new EnrollmentDBEntities();

        HighSchoolHealthInfo hshi;
        AdultHealthInfo ahi;

        HealthInfoTextValidation validCheck = new HealthInfoTextValidation();

        public HealthInfoUC()
        {
            InitializeComponent();

            hshi = null;
            ahi = null;

            if (LoginPage.highschoolCheck != null)
            {
                hshi = (from m in _db.HighSchoolHealthInfoes where m.Id == LoginPage.highschoolCheck.Id select m).FirstOrDefault();


                textFields.DataContext = hshi;
            }

            else
            {
                ahi = (from m in _db.AdultHealthInfoes where m.Id == LoginPage.adultCheck.Id select m).FirstOrDefault();

                textFields.DataContext = ahi;
            }
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ahi != null)
            {
                ahi.primaryPhysician = primaryPhysText.Text;
                ahi.otherPhysician = oPhysicianText.Text;
                ahi.pPhysicianPhoneNum = primaryPhysPhoneText.Text;
                ahi.oPhysicianPhoneNum = oPhysicianPhoneNum.Text;
                ahi.diabeticType = diabetesCombo.Text;
                ahi.allergies = allergiesText.Text;
                ahi.heartIssues = heartConditionsText.Text;
                ahi.metabolic = metabolicCheck.IsChecked.ToString();
                ahi.jointMuscle = jointMuscleCheck.IsChecked.ToString();
                ahi.chronicIllness = chronicIllnessCheck.IsChecked.ToString();
                ahi.migraines = migrainesCheck.IsChecked.ToString();
                ahi.neurological = neurologicalCheck.IsChecked.ToString();
                ahi.pulmonary = pulmonaryCheck.IsChecked.ToString();
                ahi.asthma = asthmaCheck.IsChecked.ToString();
                ahi.other = otherCheck.IsChecked.ToString();
                ahi.otherMeds = otherMedsText.Text;
                ahi.specificFirstAidNeeds = specificNeedsText.Text;
                ahi.repPermissionForTreatment = treatmentPermissionCombo.Text;

                validCheck = new HealthInfoTextValidation()
                {
                    _primaryPhysician = ahi.primaryPhysician,
                    _otherPhysician = ahi.otherPhysician,
                    _pPhysicianPhoneNum = ahi.pPhysicianPhoneNum,
                    _oPhysicianPhoneNum = ahi.oPhysicianPhoneNum,
                    _diabeticType = ahi.diabeticType,
                    _allergies = ahi.allergies,
                    _heartIssues = ahi.heartIssues,
                    _metabolic = ahi.metabolic,
                    _jointMuscle = ahi.jointMuscle,
                    _chronicIllness = ahi.chronicIllness,
                    _migraines = ahi.migraines,
                    _neurological = ahi.neurological,
                    _pulmonary = ahi.pulmonary,
                    _asthma = ahi.asthma,
                    _other = ahi.other,
                    _otherMeds = ahi.otherMeds,
                    _specificFirstAidNeeds = ahi.specificFirstAidNeeds,
                    _repPermissionForTreatment = ahi.repPermissionForTreatment
                };
            }

            else
            {
                hshi.primaryPhysician = primaryPhysText.Text;
                hshi.otherPhysician = oPhysicianText.Text;
                hshi.pPhysicianPhoneNum = primaryPhysPhoneText.Text;
                hshi.oPhysicianPhoneNum = oPhysicianPhoneNum.Text;
                hshi.diabeticType = diabetesCombo.Text;
                hshi.allergies = allergiesText.Text;
                hshi.heartIssues = heartConditionsText.Text;
                hshi.metabolic = metabolicCheck.IsChecked.ToString();
                hshi.jointMuscle = jointMuscleCheck.IsChecked.ToString();
                hshi.chronicIllness = chronicIllnessCheck.IsChecked.ToString();
                hshi.migraines = migrainesCheck.IsChecked.ToString();
                hshi.neurological = neurologicalCheck.IsChecked.ToString();
                hshi.pulmonary = pulmonaryCheck.IsChecked.ToString();
                hshi.asthma = asthmaCheck.IsChecked.ToString();
                hshi.other = otherCheck.IsChecked.ToString();
                hshi.otherMeds = otherMedsText.Text;
                hshi.specificFirstAidNeeds = specificNeedsText.Text;
                hshi.repPermissionForTreatment = treatmentPermissionCombo.Text;

                validCheck = new HealthInfoTextValidation()
                {
                    _primaryPhysician = hshi.primaryPhysician,
                    _otherPhysician = hshi.otherPhysician,
                    _pPhysicianPhoneNum = hshi.pPhysicianPhoneNum,
                    _oPhysicianPhoneNum = hshi.oPhysicianPhoneNum,
                    _diabeticType = hshi.diabeticType,
                    _allergies = hshi.allergies,
                    _heartIssues = hshi.heartIssues,
                    _metabolic = hshi.metabolic,
                    _jointMuscle = hshi.jointMuscle,
                    _chronicIllness = hshi.chronicIllness,
                    _migraines = hshi.migraines,
                    _neurological = hshi.neurological,
                    _pulmonary = hshi.pulmonary,
                    _asthma = hshi.asthma,
                    _other = hshi.other,
                    _otherMeds = hshi.otherMeds,
                    _specificFirstAidNeeds = hshi.specificFirstAidNeeds,
                    _repPermissionForTreatment = hshi.repPermissionForTreatment
                };
            }

            if (textFields.DataContext != validCheck)
            {
                textFields.DataContext = validCheck;
            }


            if (validCheck.IsValid)
            {
                _db.SaveChanges();

                Information_Page.hiuc.Visibility = Visibility.Hidden;

                

                Information_Page.lv.SelectedIndex = 2;
            }
        }
    }
}
