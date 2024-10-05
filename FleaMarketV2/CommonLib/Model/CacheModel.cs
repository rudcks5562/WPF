using FleaMarketV2Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model
{
    public class CacheModel: Base
    {
        public ObservableCollection<UserModel> cacheData;
        public ObservableCollection<UserModel> CacheData
        {
            get { return cacheData; }
            set
            {
                cacheData = value;
                OnPropertyChanged("UserName");
                OnPropertyChanged("ItemModels");
            }
        }
    }
}
