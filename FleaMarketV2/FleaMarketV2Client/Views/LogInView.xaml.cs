using FleaMarketV2Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace FleaMarketV2Client.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LogInView : Window
    {
        private StringBuilder realPassword = new StringBuilder();
        public LogInView()
        {
           
            InitializeComponent();


            LogInViewModel loginviewModel = ServiceIoC.Container.GetViewModel<LogInViewModel>();
            
          
            DataContext = loginviewModel;
        }
        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // 입력이 변경될 때마다 커서를 맨 끝으로 이동
            pwbox.Focus(); // PasswordBox에 포커스를 주고
            Keyboard.Focus(pwbox);
            
        }

    }
}
