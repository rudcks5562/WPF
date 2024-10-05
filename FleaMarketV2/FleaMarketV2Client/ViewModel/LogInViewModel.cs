using CommonLib.DB;
using CommonLib.Service;
using FleaMarketV2Client.Model;
using FleaMarketV2Client.Service;
using FleaMarketV2Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FleaMarketV2Client.ViewModel
{
    public class UserAuth
    {
        public string UserID { get; set; }
        public string UserPW { get; set; }

        public string UserRealPW { get; set; }
        public string Notification { get; set; }

    }
    public class LogInViewModel :BaseViewModel
    {
        private string userID;
        private string userPW;
        private string notification;
        private bool isAuthenticated=false;
        private string userrealPW;

        public ICommand LogInBtnCommand { get; }
        public ICommand SignInCommand { get; }

        private readonly INavigationService _navigationService;

        public LogInViewModel()
        {
            _navigationService = NavigationService.Instance;
            

            LogInBtnCommand = new DelegateCommand(LogInBtnClicked);
            SignInCommand = new DelegateCommand(SignInBtnClicked);
            Notification = "Insert your account info";


        }
        public string UserID
        {
            get => userID;
            set
            {
                userID = value;
                OnPropertyChanged(nameof(UserID));
            }
        }
        public string UserPW
        {
            get => userPW;
            set
            {
                userPW = value;
                OnPropertyChanged(nameof(UserPW));
            }
        }
        public string UserRealPW
        {
            get => userrealPW;
            set
            {
                userrealPW = value;
                OnPropertyChanged(nameof(UserRealPW));
            }
        }
        public string Notification
        {
            get => notification;
            set
            {
                notification = value;
                OnPropertyChanged(nameof(Notification));
            }
        }
        public bool IsAuthenticated
        {
            get => isAuthenticated;
            set
            {
                isAuthenticated = value;
                OnPropertyChanged(nameof(IsAuthenticated));
            }
        }

        private void LogInBtnClicked(object obj)
        {
            // connect
            IntegratedCommon.instance.sockClient.UserId = UserID;
            IntegratedCommon.instance.sockClient.Init();

            string tmpPW = new string(UserPW.Reverse().ToArray());
            bool result = IntegratedCommon.instance.repo.CheckUserAccount(UserID, tmpPW);
            if (result)
            {


                //IntegratedCommon.instance.sockClient.Send(IntegratedCommon.instance.sockClient.ClientSocket, new SUserModel() { UserID = IntegratedCommon.instance.sockClient.UserId, Cmd = "Refresh" });




                _navigationService.NavigateTo("Main");
                
            }
            else
            {
                //notified alert
                Notification = "Check your ID or PW!";
                OnPropertyChanged(nameof(Notification));
            }


        }
        private void SignInBtnClicked(object obj)
        {


            
            _navigationService.NavigateTo("SignIn");



        }


    }
}