using CommonLib.Model;
using FleaMarketV2Client.Service;
using FleaMarketV2Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace FleaMarketV2Client.Views
{


    public partial class MyItemListView : UserControl
    {


        public MyItemListView()
        {
            InitializeComponent();
            DataContext = ServiceIoC.Container.GetViewModel<MyItemListViewModel>();

        }

        public void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedItem = e.AddedItems[0] as SItemModel;
                if (selectedItem != null)
                {
                    NavigationService.Instance.AddDetailView(new DetailView(selectedItem));
                }
            }
        }
    }
}