using FleaMarketV2Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// WindowStyleControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WindowStyleControl : UserControl
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // 의존성 프로퍼티 정의
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(WindowStyleControl), new PropertyMetadata(string.Empty));

        public WindowStyleControl()
        {
            InitializeComponent();

            // DataContext 설정
            DataContext = new WindowStyleControlViewModel();
        }


        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {


            Window.GetWindow(this).DragMove();



        }






    }
}
