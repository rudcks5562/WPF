using FleaMarketV2Client.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FleaMarketV2Client.Service
{
    public interface INavigationService
    {
        void NavigateTo(string viewName);

    }

    public class NavigationService : INavigationService
    {
        private static readonly NavigationService _instance = new NavigationService();
        public Window currentView;
        private List<Window> openWindows = new List<Window>();


        private NavigationService()
        {
            
        }
        public static NavigationService Instance => _instance;

        public void AddDetailView(Window window)
        {
            window.Show();
            openWindows.Add(window);

        }
        public void AddRegistView(Window window)
        {
            window.Show();
            openWindows.Add(window);

        }
        public void CloseSubWindow(Window window)
        {
            if (openWindows.Contains(window))
            {
                window.Close(); // 창 닫기
                openWindows.Remove(window); // 리스트에서 제거
            }
        }


        public void NavigateTo(string pageKey)
        {
            // 모든 창 닫기
            CloseCurrentView();

            switch (pageKey)
            {
                case "Main":
                    if (currentView is null)
                        currentView = new MainMarket();// .                    
                    break;

                case "SignIn":
                    if (currentView is null)
                    currentView = new SignInView();
                    //currentView.Show();
                    break;

                case "MyOffer":
                    if (currentView is null)
                        currentView = new MyOfferView();
                    break;

                case "LogIn":
                    if (currentView is null)
                    currentView = new LogInView();
                    break;



                default:
                    MessageBox.Show("페이지를 찾을 수 없습니다.");
                    break;
            }
            if(currentView is not null)
            {
                currentView.WindowStartupLocation = WindowStartupLocation.CenterScreen;

                try
                {
                    currentView.Show();

                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                }

            }
        }

        public void CloseCurrentView()
        {
            if (currentView != null)
            {
                currentView.Close(); // 현재 열린 뷰를 닫음
                currentView = null; // 인스턴스를 초기화
            }
            
        }
        public void MinimizeCurrentView()
        {
            if(currentView!=null)
            currentView.WindowState = WindowState.Minimized;
        }

    }

}
