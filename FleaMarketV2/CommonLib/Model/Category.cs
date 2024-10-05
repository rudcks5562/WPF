using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Model
{
    public class Category
    {
        public string CategoryName { get; set; }
        public int Count { get; set; }
        public ObservableCollection<SItemModel> Datas { get; set; }

        public Category(ObservableCollection<SItemModel> needDatas)
        {
            this.Datas = needDatas;
            this.CategoryName = needDatas.ElementAt(0).ItemName;
            this.Count = needDatas.Count;
        }
    }
}
