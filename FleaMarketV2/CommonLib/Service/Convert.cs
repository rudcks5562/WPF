using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Service
{
    public static class Convert
    {

        public static K ModelToSModel<T,K>(T input) where K:new()
        {
            K output = new();// Send 전용 모델 




            return output;
        }
        public static K SModelToModel<T, K>(T input) where K : new()
        {
            K output = new();// binding 전용 모델 




            return output;
        }





    }
}
