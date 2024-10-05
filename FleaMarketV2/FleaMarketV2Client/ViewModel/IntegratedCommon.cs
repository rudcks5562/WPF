using CommonLib.DB;
using CommonLib.Service;
using FleaMarketV2Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleaMarketV2Client.ViewModel
{
    public class IntegratedCommon
    {
        public static IntegratedCommon instance = new IntegratedCommon();
        
        public SockClient sockClient= new SockClient();
        public Repository repo;
        public ViewModelLocator VMLoc;

        private IntegratedCommon()
        { }

        public void Init()
        {
            repo = new Repository();
            VMLoc = new ViewModelLocator();
            sockClient.DataReceived += ServiceIoC.Container.GetViewModel<SearchViewModel>().InitDataRCVD;
            sockClient.DataReceived += ServiceIoC.Container.GetViewModel<MySearchViewModel>().InitDataRCVD2;
        }

        }















    }
