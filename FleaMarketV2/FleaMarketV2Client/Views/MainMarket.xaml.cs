using FleaMarketV2Client.ViewModel;
using System.Windows;

namespace FleaMarketV2Client.Views
{
    /// <summary>
    /// MainMarket.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainMarket : Window
    {
        public MainMarket()
        {
            InitializeComponent();
            DataContext = ServiceIoC.Container.GetViewModel<MainMarketViewModel>();
        }
    }
}
