﻿using FleaMarketV2Client.ViewModel;
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
using System.Windows.Shapes;

namespace FleaMarketV2Client.Views
{
    /// <summary>
    /// MyOfferView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MyOfferView : Window
    {
        public MyOfferView()
        {
            InitializeComponent();
            DataContext = ServiceIoC.Container.GetViewModel<MyOfferViewModel>();
        }
    }
}
