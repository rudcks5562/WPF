using CommonLib.Model;
using FleaMarketV2Client.Model;
using FleaMarketV2Client.Service;
using FleaMarketV2Client.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace FleaMarketV2Client.ViewModel
{
    public class ItemListViewModel :BaseViewModel
    {
        private ObservableCollection<SItemModel> ilSelectedItems;
        private ObservableCollection<Category> ilResultList;// 검색결과 

        public ObservableCollection<Category> IlResultList
        {
            get => ilResultList;
            
            set
            {
                //if (ilResultList != value)
                {
                    ilResultList = value;
                    OnPropertyChanged(nameof(IlResultList));
                   // UpdateItemDetails();
                }
            }
        }
        public ObservableCollection<SItemModel> ISelectedItems
        {
            get
            {
                return ilSelectedItems;
            }
            set
            {
               // if (ilResultList != value)
                {
                    ilSelectedItems = value;
                    OnPropertyChanged(nameof(ISelectedItems));
                    // UpdateItemDetails();
                }
            }
        }

        public ICommand PurchaseBtnCommand { get; }




        public ItemListViewModel()
        {
            IlResultList = new ObservableCollection<Category>();
            ilSelectedItems = new ObservableCollection<SItemModel>();
            PurchaseBtnCommand = new DelegateCommand<object>(PurchaseBtnClicked);
            ItemListDataRefresh();
            ServiceIoC.Container.GetViewModel<SearchViewModel>().SelectChanged += ItemListDataRefresh;
        }

        public void PurchaseBtnClicked(object obj)
        {
            if(obj is SItemModel)
            {
                SItemModel tmpModel = (SItemModel)obj;
                SUserModel tmpUMdoel = new SUserModel();
                tmpUMdoel.Cmd = "Purchase";
                tmpUMdoel.UserID = IntegratedCommon.instance.sockClient.UserId;
                tmpUMdoel.ItemModels = new ObservableCollection<SItemModel>();
                tmpUMdoel.ItemModels.Add(tmpModel);

                IntegratedCommon.instance.sockClient.Send(IntegratedCommon.instance.sockClient.ClientSocket,tmpUMdoel);
                // isonmarket의 변경과 소유자 변경에 대한 로직 
            }

        }

        public void ItemListDataRefresh()
        {
            this.IlResultList=ServiceIoC.Container.GetViewModel<SearchViewModel>().ResultList;
            this.ISelectedItems = ServiceIoC.Container.GetViewModel<SearchViewModel>().SelectedItems;


        }








    }
}
