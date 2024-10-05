using FleaMarketV2Client.Service;
using FleaMarketV2Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FleaMarketV2Client.ViewModel
{
    public class StatusBarViewModel : BaseViewModel 
    {

        public ICommand MyOfferMoveCommand { get; }
        public ICommand MainOfferMoveCommand { get; }
        public ICommand AddOfferRegSpawnCommand { get; }
        NavigationService ns;

        public StatusBarViewModel()
        {

            MyOfferMoveCommand = new DelegateCommand(MoveMyOffer);
            MainOfferMoveCommand = new DelegateCommand(MoveMainOffer);
            AddOfferRegSpawnCommand = new DelegateCommand(AddOfferRegSpawns);
            ns = NavigationService.Instance;


        }


        public void MoveMainOffer(object obj)
        {
            ns.NavigateTo("Main");

            ServiceIoC.Container.GetViewModel<SearchViewModel>().SearchItem();

        }
        public void MoveMyOffer(object obj)
        {
            ns.NavigateTo("MyOffer");
            ServiceIoC.Container.GetViewModel<MySearchViewModel>().SearchItem();
        }
        public void AddOfferRegSpawns(object obj)
        {

                        NavigationService.Instance.AddRegistView(new RegistView());
                    
                
            


        }








    }
}
