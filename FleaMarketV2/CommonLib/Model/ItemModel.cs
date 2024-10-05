namespace CommonLib.Model
{
    public class ItemModel : Base
    {
        private string itemCode;
        private string itemName;
        private byte[] imageData;
        private int cost;
        private string description;
        private string seller;
        private string regLog;
        private int isOnMarket;

        public string ItemCode
        { 
            get {return itemCode;}  
            set
            {
                itemCode = value;
                OnPropertyChanged("ItemCode");
            }
        }
        public string ItemName
        {
            get {return itemName;}
            set
            {
                itemName = value;
                OnPropertyChanged("ItemName");
            }
        }
        public byte[] ImageData
        {
            get {return imageData;}
            set
            {
                imageData = value;
                OnPropertyChanged("ImageData");
            }
        }
        public int Cost
        {
            get {return cost;}
            set
            {
                cost = value;
                OnPropertyChanged("Cost");
            }
        }
        public string Description
        {
            get {return description;}
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public string Seller
        {
            get {return seller;}
            set
            {
                seller = value;
                OnPropertyChanged("Seller");
            }
        }
        public string RegLog
        {
            get {return regLog;}
            set
            {
                regLog = value;
                OnPropertyChanged("RegLog");
            }
        }
        public int IsOnMarket
        {
            get { return isOnMarket; }
            set
            {
                isOnMarket = value;
                OnPropertyChanged("IsOnMarket");
            }
        }
        public ItemModel(string itemCode, string itemName, byte[] imageData, int cost, string description, string seller, string regLog,int isOnMarket)
        {
            ItemCode = itemCode;
            ItemName = itemName;
            ImageData = imageData;
            Cost = cost;
            Description = description;
            Seller = seller;
            RegLog = regLog;
            IsOnMarket = isOnMarket;

        }
    }
}