using CommonLib.DB;
using CommonLib.Model;
using CommonLib.Service;
using FleaMarketV2Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FleaMarketV2Client.ViewModel
{
    public class MySearchViewModel : BaseViewModel
    {
        private Category selectedCategory;
        private ObservableCollection<SItemModel> allItem; // 모든 데이터 집합소 
        private ObservableCollection<Category> resultList;// 검색결과 
        private ObservableCollection<SItemModel> selectedItems;// 선택된 집합.
        private string searchQuery;
        public event Action SelectChanged;
        //private MyItemListViewModel myItemVM;
        public Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                if (selectedCategory != value)
                {
                    selectedCategory = value;
                   
                    UpdateSelectedItems();
                    OnPropertyChanged(nameof(SelectedCategory));
                }
            }
        }
        public string SearchQuery
        {
            get
            {
                return searchQuery;
            }
            set
            {
                if (searchQuery != value)
                {
                    searchQuery = value;
                    OnPropertyChanged(nameof(SearchQuery));

                    UpdateRes();
                }
            }
        }
        public ObservableCollection<Category> ResultList
        {
            get
            {
                return resultList;
            }
            set
            {
                if (resultList != value)
                {
                    resultList = value;
                    OnPropertyChanged(nameof(ResultList));
                   // UpdateItemDetails();
                }
            }
        }
        public ObservableCollection<SItemModel> SelectedItems
        {
            get => selectedItems;
            set
            {
                if (selectedItems != value)
                {
                    selectedItems = value;
                    OnPropertyChanged(nameof(SelectedItems));
                   
                }
            }
        }

        private void UpdateSelectedItems()
        {
            SelectedItems.Clear();
            if (SelectedCategory != null)
            {
                foreach (var item in SelectedCategory.Datas)
                {
                    SelectedItems.Add(item);
                    Trace.WriteLine(item.ItemName);
                }
            }
            SelectChanged.Invoke();
        }
    

        public ObservableCollection<SItemModel> AllItem
        {
            get => allItem;
            set
            {
                allItem = value;
                OnPropertyChanged(nameof(AllItem));
                UpdateRes();
            }

        }

        // 변화요소에 대한 afterinteraction함수.
        public void AllItemChecker()// 변화: 외부에서+내부 -> 모두 이벤트로 연결됨.
        {
            OnPropertyChanged(nameof(AllItem));

            ResListChecker();

        }
        public void ResListChecker()// all이 바뀌어서 발생하거나 검색어가 바뀌거나->  검색어 기반 서치 돌리기 - res에 갱신. 
        {// 조건: 검색어

            OnPropertyChanged(nameof(ResultList));
            SelectedItemsChecker();

        }
        public void SelectedItemsChecker()// res 변경, 검색어변경, all변경에 모두 영향받음.
        {// 조건 검색어 + 사용자선택 

            OnPropertyChanged(nameof(SelectedItems));
        }
        public void SearchQueryChecker()
        {// 검색어임.

            ResListChecker();

        }


        public void SearchItem()
        {
            ResultList = new ObservableCollection<Category>();
            AllItem = new ObservableCollection<SItemModel>();

   
            if ((SearchQuery == null || SearchQuery == string.Empty) && IntegratedCommon.instance.sockClient.ClientSocket is not null)
            {
                // ResultList = new ObservableCollection<SItemModel>(repo.SearchItemByName(""));
                //IntegratedCommon.instance.sockClient.Send(IntegratedCommon.instance.sockClient.ClientSocket, new SUserModel() { UserID = IntegratedCommon.instance.sockClient.UserId, Cmd = "Refresh" });
               IntegratedCommon.instance.sockClient.Send(IntegratedCommon.instance.sockClient.ClientSocket, new SUserModel() { UserID = IntegratedCommon.instance.sockClient.UserId, Cmd = "MyList",ItemModels= new ObservableCollection<SItemModel>() });
                Trace.WriteLine("my searchview에서 나의 리스트 정보를 요청함");

            }
            else
            {
               // ResultList = new ObservableCollection<SItemModel>(repo.SearchItemByName(SearchQuery));
               // allitems에서 추리는 로직 추가.. << resultList에 대입 후 옆의 뷰모델에 이벤트 전달.

            }
        }
        public MySearchViewModel()
        {
            AllItem = new ObservableCollection<SItemModel>();
            ResultList = new ObservableCollection<Category>();
            SelectedItems = new ObservableCollection<SItemModel>();
            //SelectedItems = null;
            SearchQuery = string.Empty;
            SearchItem();


        }

        public void UpdateRes()
        {
            this.ResultList = new ObservableCollection<Category>();

            ObservableCollection<SItemModel> tmp_list = new ObservableCollection<SItemModel>();

            foreach (SItemModel item in AllItem)
            {
                if (item.ItemName.Contains(this.SearchQuery))
                {
                    tmp_list.Add(item);
                }
            }

            var groupedItems = tmp_list.GroupBy(item => item.ItemName)
                                       .Select(group => new Category(new ObservableCollection<SItemModel>(group)))
                                       .ToList();

            foreach (var category in groupedItems)
            {
                this.ResultList.Add(category);
            }
        }

        public void UpdateAllItem(SItemModel target,int way)
        {
            int cnt = 0;
            switch (way)
            {
                case 1: // ADD

                    AllItem.Add(target);

                break;
                case 2:// DELETE
                    
                    foreach (SItemModel pastModel in AllItem)
                    {

                        if (pastModel.ItemCode.Equals(target.ItemCode))
                        {
                            AllItem.RemoveAt(cnt);
                            break;
                        }
                        cnt++;
                    }

                    break;
                case 3:// PURCHASE

                    foreach (SItemModel pastModel in AllItem){

                        if (pastModel.ItemCode.Equals(target.ItemCode))
                        {
                            AllItem.RemoveAt(cnt);
                            AllItem.Add(target);
                            break;
                        }
                        cnt++;
                    }
                break;

                default:

                    break;

            }

            UpdateRes();
        }



        public void InitDataRCVD2(object data)// 
        {

            try
            {
                SUserModel tmp2 = (SUserModel)Function.Deserialize((byte[])data);
                if (tmp2.Cmd.Equals("Regist"))
                {
                    UpdateAllItem(tmp2.ItemModels.ElementAt(0), 1);
                }
                else if (tmp2.Cmd.Equals("Purchase"))
                {
                    UpdateAllItem(tmp2.ItemModels.ElementAt(0), 3);
                }
                else if (tmp2.Cmd.Equals("Delete"))
                {
                    UpdateAllItem(tmp2.ItemModels.ElementAt(0), 2);
                }
                else if (tmp2.Cmd.Equals("MyList"))
                {
                    AllItem.Clear();
                    var aa = tmp2.ItemModels;

                    foreach (var bb in aa)
                    {

                        AllItem.Add(bb);
                    }
                    OnPropertyChanged(nameof(ResultList));
                }

                UpdateRes();
                Trace.WriteLine("MYDATA RCVD COMPLETE!" + ResultList.Count);
            }
            catch(Exception e)
            {
               // return;
            }
             
           
            
        }











    }
}
