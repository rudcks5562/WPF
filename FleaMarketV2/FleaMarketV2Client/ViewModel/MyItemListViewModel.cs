using CommonLib.Model;
using CommonLib.Service;
using FleaMarketV2Client.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FleaMarketV2Client.ViewModel
{
    public class MyItemListViewModel : BaseViewModel
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

        public ICommand DeleteBtnCommand { get; }




        public MyItemListViewModel()
        {
            IlResultList = new ObservableCollection<Category>();
            ilSelectedItems = new ObservableCollection<SItemModel>();
            DeleteBtnCommand = new DelegateCommand<object>(DeleteBtnClicked);
            MyItemListDataRefresh();
            ServiceIoC.Container.GetViewModel<MySearchViewModel>().SelectChanged += MyItemListDataRefresh;
        }

        public void DeleteBtnClicked(object obj)
        {
            if (obj is SItemModel)
            {
                SItemModel tmpModel = (SItemModel)obj;
                SUserModel tmpUMdoel = new SUserModel();
                tmpUMdoel.Cmd = "Delete";
                tmpUMdoel.UserID = IntegratedCommon.instance.sockClient.UserId;
                tmpUMdoel.ItemModels = new ObservableCollection<SItemModel>();
                tmpUMdoel.ItemModels.Add(tmpModel);


                Trace.WriteLine("DELETE BTN CLICKED!!");


                //IntegratedCommon.instance.sockClient.Send(IntegratedCommon.instance.sockClient.ClientSocket, tmpUMdoel);
                // isonmarket의 변경과 소유자 변경에 대한 로직 
            }

        }

        public void MyItemListDataRefresh()
        {
            this.IlResultList = ServiceIoC.Container.GetViewModel<MySearchViewModel>().ResultList;
            this.ISelectedItems = ServiceIoC.Container.GetViewModel<MySearchViewModel>().SelectedItems;


        }








    }
}
