using CommonLib.DB;
using FleaMarketV2Client.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FleaMarketV2Client.ViewModel
{
    public class SignInViewModel : BaseViewModel
    {
        private string inputID;
        private string inputPW;
        private string inputPWConfirm;
        private string notificationBySign;
        public ICommand CreateCommand { get; }

        private readonly INavigationService _navigationService;

        public SignInViewModel()
        {
            _navigationService = NavigationService.Instance;
            CreateCommand = new DelegateCommand(CreateBtnClicked);
            NotificationBySign = "Insert new account info";

            
        }
        public string InputID
        {
            get => inputID;
            set
            {
                inputID = value;
                OnPropertyChanged(nameof(InputID)); 
            }
        }
        public string InputPW
        {
            get => inputPW;
            set
            {
                inputPW = value;
                OnPropertyChanged(nameof(InputPW));
            }
        }
        public string InputPWConfirm
        {

            get => inputPWConfirm;
            set
            {
                inputPWConfirm = value;
                OnPropertyChanged(nameof(InputPWConfirm));
            }
        }
        public string NotificationBySign
        {
            get => notificationBySign;
            set
            {
                notificationBySign = value;
                OnPropertyChanged(nameof(NotificationBySign));
            }

        }

        private void CreateBtnClicked(object obj)
        {
            Repository repo = new Repository();
            bool res=repo.CheckUserAccountDuplicate(InputID);

            if (res)
            {
                NotificationBySign = "Duplicated Account Exist!";
                OnPropertyChanged(nameof(NotificationBySign));
            }
            else
            {

                if (!InputPW.Equals(InputPWConfirm))
                {
                    NotificationBySign = "PW and PWCONFIRM IS NOT SAME!";
                    OnPropertyChanged(nameof(NotificationBySign));
                    return;
                }



                repo.InsertUser(InputID, InputPW);
                MessageBox.Show("Create Account Complete!");
                _navigationService.NavigateTo("LogIn");
            }

        }










    }
}
