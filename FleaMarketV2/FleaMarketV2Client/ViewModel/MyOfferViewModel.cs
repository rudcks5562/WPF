using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleaMarketV2Client.ViewModel
{
    public class MyOfferViewModel:BaseViewModel
    {
        private string toolTips = "";
        public MyOfferViewModel()
        {
            ToolTips = "초기 툴팁입니다.";
        }
        public string ToolTips
        {
            get => toolTips;
            set
            {
                toolTips = value;
                OnPropertyChanged(nameof(ToolTips));
            }
        }



    }
}
