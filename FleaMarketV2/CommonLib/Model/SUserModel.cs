
using CommonLib.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FleaMarketV2Client.Model
{
    [Serializable]
    public class SUserModel : DataCompanion
    {
        public string UserID { get; set; }// ID
        public ObservableCollection<SItemModel> ItemModels { get; set; }
    }
}
