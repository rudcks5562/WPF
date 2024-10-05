using CommonLib.Model;
using FleaMarketV2Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Service
{
    public class CacheManager// 서버의 캐시를 관리해주는 캐시 매니저 클래스입니다. 
    {
        private static CacheManager instance = new CacheManager();// Singletone
        public CacheModel UICache;// UI에 바인딩을 하기 위한 자료구조 입니다. 
        public SCacheMemory SendCache;// 전송을 위한 클래스로 재구성한 자료구조 입니다.
        private CacheManager()
        {
            this.UICache = new CacheModel();
            this.SendCache = new SCacheMemory();//  생성자에 내부 구조 만들어지게 해서 test하기
            //DB에서 데이터 받아오기.
        }
        public static CacheManager GetInstance()
        {
            return instance;
        }
        public void CompareData(SUserModel data)// 데이터를 상호비교하여 틀린 점이 있으면 치환하고 데이터가 없으면 기본 틀을 제공하는 함수입니다.
        {
            foreach (var user in SendCache.SCacheData)// 캐시의 유저중
            {
                bool flag = false;
                if (user.UserID == data.UserID)// 변경점유저와 같은 유저면 
                {
                    flag = true;
                    user.ItemModels = data.ItemModels;// 최신의 데이터로 그냥 치환하자..
                }
                
                if (flag == false)
                {
                    this.SendCache.SCacheData=(new ObservableCollection<SUserModel> { data });
                    flag = true;
                    break;
                }
            }
        }
    }
}