using FleaMarketV2Client.ViewModel;
using System.Windows.Controls;

namespace FleaMarketV2Client.Views
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class MySearchView : UserControl
    {

        public MySearchView()
        {
            InitializeComponent();

            DataContext = ServiceIoC.Container.GetViewModel<MySearchViewModel>();


        }
    }
}
