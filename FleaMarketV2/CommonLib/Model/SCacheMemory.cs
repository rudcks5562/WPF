
using CommonLib.Model;
using FleaMarketV2Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FleaMarketV2Client.Model
{
    [Serializable]
    public class SCacheMemory: DataCompanion
    {
        public ObservableCollection<SUserModel> SCacheData {get;set;}
    }
}
