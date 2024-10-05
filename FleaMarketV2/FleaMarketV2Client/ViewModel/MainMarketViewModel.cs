using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleaMarketV2Client.ViewModel
{
    public class MainMarketViewModel : BaseViewModel 
    {

        private string toolTips="";



        public MainMarketViewModel()
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
