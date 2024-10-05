using CommonLib.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FleaMarketV2Client.Model
{
    public class UserModel : Base
    {
        private string userName = string.Empty;
        public string UserName
        {
            get{return userName;}
            set
            {
                UserName = value;
                OnPropertyChanged("UserName");
                OnPropertyChanged("ItemModels");
            }
        }
        public ObservableCollection<ItemModel> ItemModels { get; set; }
        public UserModel(string username)
        {
            userName = username;
            ItemModels = new ObservableCollection<ItemModel>();
        }

        public UserModel()
        {
        }
    }
}