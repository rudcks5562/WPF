using CommonLib.DB;
using FleaMarketV2Client.Model;
using CommonLib.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CommonLib.Service
{
    public class TCPClientHandler// 하나로 관리 가능.
    {
        public TcpClient ClientSocket {get; set;}
        public SUserModel Data {get; set;}
        //public event Action<object> DataReceived;
        public TCPClientHandler(TcpClient clientSocket)
        {
            ClientSocket = clientSocket;
            Data = new SUserModel();
        }
        public TCPClientHandler()
        {
            Data = new SUserModel();
            //DataReceived += ReceiveData<object>;

        }
        public void Start()
        {

           ThreadPool.QueueUserWorkItem(ReceiveData);
            //ViewModelMediator.Instance.ClientConnected(this);

            Repository repo = new Repository();

            SocketServer.GetInstance().Send<ObservableCollection<SUserModel>>(this, repo.GetAllData());
            



        }
        public void ReceiveData(object obj)//action 등록? , 받은 데이터 캐시랑 비교후 갱신후 send
        {
            while (true)
            {
                try
                {

                    NetworkStream stream = ClientSocket.GetStream();

                    try
                    {
                        byte[] sizeBuffer = new byte[4]; // Assuming the size of data is sent as 4 bytes
                        int bytesRead = stream.Read(sizeBuffer, 0, sizeBuffer.Length);
                        int totalBytesRead = 0;
                        if (bytesRead == 4)
                        {
                            int dataSize = BitConverter.ToInt32(sizeBuffer, 0);
                            // 데이터를 담을 배열 생성
                            byte[] receivedData = new byte[dataSize];
                            // 데이터를 모두 읽을 때까지 반복해서 읽기
                            while (totalBytesRead < dataSize)
                            {
                                bytesRead = stream.Read(receivedData, totalBytesRead, dataSize - totalBytesRead);
                                if (bytesRead == 0)
                                {
                                    throw new IOException("Connection closed unexpectedly");
                                }
                                totalBytesRead += bytesRead;
                            }
                            // 데이터 역직렬화
                            object DesData = CommonLib.Service.Function.Deserialize(receivedData);
                            SUserModel userModel = DesData as SUserModel;// 역직렬화시에 object로 캐스팅된 요소는 어떻게 되는가?
                            SocketServer server = SocketServer.GetInstance();
                            Repository repo = new Repository();
                            Trace.WriteLine("서버는 데이터를 받았다.");
                            if (DesData != null)
                            {
                                if (userModel.Query == null)
                                {
                                    switch (userModel.Cmd)
                                    {
                                        case "Regist":// 등록 시도
                                            repo.Identify(userModel);
                                            repo.RecentDBToCache();
                                            server.SendAll<SUserModel>(userModel);
                                            Trace.WriteLine("등록 요청 전달");
                                            break;
                                        case "Delete":// 삭제 시도 
                                            repo.Identify(userModel);
                                            repo.RecentDBToCache();
                                            server.SendAll<SUserModel>(userModel);
                                            Trace.WriteLine("삭제 요청 전달");

                                            break;
                                        case "Purchase":// 구매 시도 
                                            repo.Identify(userModel);
                                            repo.RecentDBToCache();
                                            server.SendAll<SUserModel>(userModel);
                                            Trace.WriteLine("구매 요청 전달");

                                            break;
                                        case "MyList":// 나의 판매목록 요청
                                            SUserModel cpMine = new SUserModel();
                                            cpMine.UserID = userModel.UserID;
                                            cpMine.Cmd = userModel.Cmd;
                                            cpMine.ItemModels = (repo.SearchMyItem(cpMine.UserID));
                                            server.Send<SUserModel>(this, cpMine);
                                            Trace.WriteLine("나의 리스트 요청 전달");

                                            break;
                                        case "Refresh":
                                            server.Send<ObservableCollection<SUserModel>>(this, repo.GetAllData());// 수신 클라이언트에게 디비데이터 받아와서 전달
                                            Trace.WriteLine("server는 새로고침 요청을 받아 다시 원상태로 보내준다.");
                                            break;
                                    }
                                }
                                else
                                {
                                    //search
                                    SUserModel searchList = new SUserModel();
                                    searchList.UserID = "searchlistresult";
                                    searchList.Cmd = Data.Cmd;
                                    searchList.ItemModels = (repo.SearchItemByName(Data.Query));
                                    searchList.Query = "SearchResult";
                                    server.Send<SUserModel>(this, searchList);// 검색어에 기반한 리스트 전달.
                                }
                            }
                            else
                            {
                                Console.WriteLine("Deserialized data is not of type SUserModel");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid data size received");
                        }
                    }
                    catch (Exception ee)
                    {
                        Trace.WriteLine($"{ee}");
                    }
                }

                catch (SocketException e)
                {
                    SocketServer.GetInstance().OnDisconnected(this);
                    Trace.WriteLine($"EXCEPTION {e}");
                    break;
                }
            }
        }
    }
}

