
using System;

namespace CommonLib.Model
{
    [Serializable]
    public class SItemModel: DataCompanion
    {
        public string ItemCode { get; set; }
        public string ItemName{ get; set; }
        public byte[] ImageData{ get; set; }
        public int Cost{ get; set; }
        public string Description { get; set; }
        public string Seller { get; set; }
        public string RegLog { get; set; }
        public int IsOnMarket { get; set; }
    }
}
