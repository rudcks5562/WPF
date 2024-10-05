using CommonLib.Service;
using FleaMarketV2Client.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CommonLib.Service
{
    public class SocketServer
    {
        private static SocketServer instance;
        public Dictionary<TCPClientHandler, string> clientList = new Dictionary<TCPClientHandler, string>();
        public delegate void ReceiveHandler(object obj);
        public event ReceiveHandler Received;
        private string date = ""; // 날짜
        private const int PORT = 9000; // 포트 정보
        public TcpListener Server { get; set; }
        private SocketServer()
        {
            //Init<DataCompanion>();// 움직이는 정보의 단위를 일반화 명시. 단 캐시 전체의 움직임은 따로 캐시매니저에서 취급하도록 한다.
        }
        public static SocketServer GetInstance()// singletone 
        {
            if (instance == null)
            {
                instance = new SocketServer();
            }
            return instance;
        }
        public void Init<T>()
        {
            ThreadPool.QueueUserWorkItem(InitSocket<T>);
        }
        private static string GetLocalIP()// 로컬아이피 가져오기.
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());// 호스트이름을 받아서 그것을 기반으로 iphostentry 객체 반환. 이 객체는 호스트의 넷웤 주소를 포함.
            string localIP = string.Empty;
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = host.AddressList[i].ToString();
                    break;
                }
            }
            return localIP;
        }
        public void SendAll<T>(T data)
        {
            foreach (TCPClientHandler tcpClient in clientList.Keys.ToList())
            {
                Send(tcpClient, data);
            }
        }
        public void Send<T>(TCPClientHandler clientSocket, T data)
        {
            try
            {
                NetworkStream stream = clientSocket.ClientSocket.GetStream();
                byte[] buffer = Service.Function.Serialize(data);
                int dataSize = buffer.Length;
                byte[] sizeBuffer = BitConverter.GetBytes(dataSize);
                stream.Write(sizeBuffer, 0, sizeBuffer.Length);
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"데이터 전송 중 오류 발생: {ex.Message}");
            }
        }
        private void InitSocket<T>(object obj)// 백그라운드에서 돌아갈 함수 
        {
            String temp_IP = "";
            temp_IP = GetLocalIP();
           
            if (Server == null)
            {
                Server = new TcpListener(IPAddress.Parse(temp_IP), PORT); // 서버 객체 생성 및 IP주소와 Port번호를 할당
                Server.Start(); // 서버 시작   
            }
            while (true)// 무한루프  listen 
            {
                try// 예외처리 
                {
                    TCPClientHandler tmp_handler = new TCPClientHandler();
                    tmp_handler.ClientSocket = Server.AcceptTcpClient(); // client 소켓 접속 허용- 클라이언트 객체 반환 
                    string UserIdFirst = "";
                    NetworkStream stream = tmp_handler.ClientSocket.GetStream();
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

                        object des = CommonLib.Service.Function.Deserialize(receivedData);
                        SUserModel tmp = new SUserModel();
                        string tmp_name="";
                        if (des is string str)
                        {
                            tmp_name = (string)des;
                        }
                        else
                        {
                            tmp = (SUserModel)des;
                        }
                        // 데이터 역직렬화



                        UserIdFirst = tmp_name;
                    }
                    if (!clientList.ContainsKey(tmp_handler))
                    {

                        clientList.Add(tmp_handler, UserIdFirst);// 최초접근에 한하여 이름은 이 후에 받는 데이터로 정의한다.                                       //Received += Myhandler.ReceiveData<T>;
                        tmp_handler.Data.UserID = UserIdFirst;// 핸들러의 유저 네임은 유저 아이디이다!!
                        tmp_handler.Start();
                       


                        Trace.WriteLine($"ADD{UserIdFirst}");
                        //lock?


                    }// 2회이상 연결에 대한 예외처리.

                }
                catch (SocketException se)
                {
                    break;// 에러 
                }
            }
        }






        public void OnDisconnected(TCPClientHandler tmp_handler)
        {
            if (clientList.ContainsKey(tmp_handler))
            {
                clientList.Remove(tmp_handler);
            }
            //ViewModelMediator.Instance.ClientDisconnected(tmp_handler);
        }
    }
}