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
    public class WindowStyleControlViewModel :BaseViewModel
    {
        public ICommand MinimizeCommand { get; }
        public ICommand CloseCommand { get; }

        public WindowStyleControlViewModel()
        {
            MinimizeCommand = new DelegateCommand(Minimize);
            CloseCommand = new DelegateCommand(Close);

        }

        private void Minimize(object obj)
        {
            var window = Application.Current.MainWindow;
            if (window != null)
            {
                window.WindowState = WindowState.Minimized;
            }
            NavigationService ns=  NavigationService.Instance;
            ns.MinimizeCurrentView();

        }

        private void Close(object obj)
        {
            var window = Application.Current.MainWindow;
            if (window != null)
            {
                window.Close();
            }

            // Application.Current.Shutdown();
        }


    }
}
