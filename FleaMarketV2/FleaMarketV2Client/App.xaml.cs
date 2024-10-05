using FleaMarketV2Client.Service;
using FleaMarketV2Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FleaMarketV2Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IntegratedCommon.instance.Init();
            var mainwindow = new FleaMarketV2Client.Views.LogInView();
            NavigationService.Instance.NavigateTo("LogIn");
            // NavigationService.Instance.NavigateTo("Detail");
            //NavigationService.Instance.NavigateTo("Detail");
        }

    }
}
