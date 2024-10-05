using FleaMarketV2Client.ViewModel;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleaMarketV2Client.ViewModel
{
    public class ViewModelLocator : ServiceIoC
    {

        public ViewModelLocator()
        {
            Container.Register<LogInViewModel>();
            Container.Register<SignInViewModel>();
            Container.Register<MainMarketViewModel>();
            //Container.Register<RegistViewModel>();
            Container.Register<StatusBarViewModel>();
            //Container.Register<DetailViewModel>();
            Container.Register<MyOfferViewModel>();
            Container.Register<SearchViewModel>(Lifestyle.Singleton);
            Container.Register<ItemListViewModel>(Lifestyle.Singleton);
            Container.Register<MySearchViewModel>(Lifestyle.Singleton);
            Container.Register<MyItemListViewModel>(Lifestyle.Singleton);
        }
        // viewmodel properties


      public LogInViewModel LogInViewModel
        {
            get { return Container.GetViewModel<LogInViewModel>(); }
        }
        public SignInViewModel SignInViewModel
        {
            get { return Container.GetViewModel<SignInViewModel>(); }
        }
        public MainMarketViewModel MainMarketViewModel
        {
            get { return Container.GetViewModel<MainMarketViewModel>(); }
        }
        public SearchViewModel SearchViewModel
        {
            get { return Container.GetViewModel<SearchViewModel>(); }
        }
        public MySearchViewModel MySearchViewModel
        {
            get { return Container.GetViewModel<MySearchViewModel>(); }
        }
        public ItemListViewModel ItemListViewModel
        {
            get { return Container.GetViewModel<ItemListViewModel>(); }
        }

        public MyItemListViewModel MyItemListViewModel
        {
            get { return Container.GetViewModel<MyItemListViewModel>(); }
        }
        public RegistViewModel RegistViewModel
        {
            get { return Container.GetViewModel<RegistViewModel>(); }
        }

        public StatusBarViewModel StatusBarViewModel
        {
            get { return Container.GetViewModel<StatusBarViewModel>(); }
        }
        public DetailViewModel DetailViewModel
        {
            get
            {
                return Container.GetViewModel<DetailViewModel>();
            }
        }
        public MyOfferViewModel MyOfferViewModel
        {
            get
            {
                return Container.GetViewModel<MyOfferViewModel>();
            }
        }
    }
    }

