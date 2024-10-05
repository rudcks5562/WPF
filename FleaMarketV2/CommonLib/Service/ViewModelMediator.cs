using CommonLib.Model;
using CommonLib.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Service
{
    public class ViewModelMediator
    {
        private static ViewModelMediator instance  = new ViewModelMediator();
        public string UserId="";
        public static ViewModelMediator Instance
        {
            get { return instance; }
        }
        private ViewModelMediator(){
        }
        // 이벤트 정의
        public event EventHandler<string> SearchDataReceived;
        public event EventHandler<SItemModel> ItemDataReceived;
        public event EventHandler<string> ImgFilePathReceived;
        public event EventHandler<SItemModel> RegistItemDataReceived;
        public event EventHandler<TCPClientHandler> OnClientConnected;
        public event EventHandler<TCPClientHandler> OnClientDisconnected;
        public event EventHandler<object> AfterInteraction;

        public void SendCompleteInteration(string str)// 상호작용 끝나고 새로고침 할 곳에
        {
            AfterInteraction?.Invoke(this, str);
        }


        // 데이터 전송 메서드
        public void SendStringData(string data)
        {
            SearchDataReceived?.Invoke(this, data);
        }
        public void SendItemData(SItemModel item)// detailview를 위한 
        {
            ItemDataReceived?.Invoke(this,item);
        }
        public void SendImgFilePath(string filepath)
        {
            ImgFilePathReceived?.Invoke(this, filepath);
        }
        public void SendRegistItemData(SItemModel item)
        {
            RegistItemDataReceived?.Invoke(this, item);
        }
        public void ClientConnected(TCPClientHandler clientHandler)
        {
            OnClientConnected?.Invoke(this, clientHandler);
        }

        // 클라이언트 연결 해제 이벤트 발송
        public void ClientDisconnected(TCPClientHandler clientHandler)
        {
            OnClientDisconnected?.Invoke(this, clientHandler);
        }



    }
}
