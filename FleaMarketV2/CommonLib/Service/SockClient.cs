using FleaMarketV2Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.IdentityModel.Protocols;

namespace CommonLib.Service
{
    public class SockClient
    {
        

        public string UserId;
        public delegate void ReceiveHandler(object obj);
        public event ReceiveHandler ClientReceived;
        private const int PORT = 9000; // 포트 정보                               // class  따로 만들어서 관리하기., 전달방법. 
        private TcpClient clientSocket { get; set; }
        public TcpClient ClientSocket { get { return clientSocket; } }
        private NetworkStream Stream { get; set; }

        public event Action<object> DataReceived;
      

        public SockClient(string uid)
        {
            
           // this.UserId = uid;
            //Init();
        }

        public SockClient()
        {
            //Init();
        }

        public void Init()
        {
         
            ThreadPool.QueueUserWorkItem(InitSocket);
        }
        public static string GetLocalIP()// 로컬아이피 가져오기.
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
    private void InitSocket(object obj)// 백그라운드에서 돌아갈 함수 생성자의 목표인 연결시작과, 리시브에 대한 적용 .
    {
        string temp_IP = GetLocalIP();
            string temp_PORT = ConfigurationManager.AppSettings["ServerPort"];
        clientSocket = default(TcpClient); // 소켓 설정 null? 
        while (true)// 무한루프 
        {
            try// 예외처리 
            {
                clientSocket = new TcpClient();
                clientSocket.Connect(temp_IP, int.Parse(temp_PORT));
                if (clientSocket.Connected)
                {
                        try
                        {
                            
                            NetworkStream stream = clientSocket.GetStream();
                            byte[] buffer = Service.Function.Serialize(this.UserId);
                            int dataSize = buffer.Length;
                            byte[] sizeBuffer = BitConverter.GetBytes(dataSize);
                            stream.Write(sizeBuffer, 0, sizeBuffer.Length);
                            stream.Write(buffer, 0, buffer.Length);
                            stream.Flush();
                            
                        }
                        catch(Exception e)
                        {

                        }
                        ThreadPool.QueueUserWorkItem(OnReceived<object>);
                    break;
                }
            }
            catch (SocketException se)
            {
                ClientSocket.Close(); // client 소켓 닫기
                break;
            }// 에러 

            catch (Exception ex)
            {
                ClientSocket.Close(); // client 소켓 닫기
                break;
            }
        }// 에러나면 종료하기 아래 
    }
        public void Send<T>(TcpClient clientSocket, T data)
        {
            try// Serialization Send
            {
                NetworkStream stream = clientSocket.GetStream();
                byte[] buffer = Service.Function.Serialize(data);
                int dataSize = buffer.Length;
                byte[] sizeBuffer = BitConverter.GetBytes(dataSize);
                stream.Write(sizeBuffer,0,sizeBuffer.Length);
                stream.Write(buffer, 0, buffer.Length);
                stream.Flush();
                Trace.WriteLine($"클라에서 서버로 데이터 전송:");
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"데이터 전송 중 오류 발생: {ex.Message}");
            }
        }
        public void OnReceived<T>(object obj)//서버로부터 받은 것-> 캐시참조 갱신요청을 통해 
        {
            while (true)
            {
                try
                {
                    // push는 바인딩요소 바꿔주고 캐시전체 받은거면 ..
                    NetworkStream stream = ClientSocket.GetStream();
                    byte[] sizeBuffer = new byte[4]; // Assuming the size of data is sent as 4 bytes
                    int bytesRead = stream.Read(sizeBuffer, 0, sizeBuffer.Length);
                    int totalBytesRead = 0;
                    if (bytesRead == 4)
                    {
                        int dataSize = BitConverter.ToInt32(sizeBuffer, 0);
                        byte[] receivedData = new byte[dataSize];
                        while (totalBytesRead < dataSize)
                        {
                            bytesRead = stream.Read(receivedData, totalBytesRead, dataSize - totalBytesRead);
                            if (bytesRead == 0)
                            {
                                throw new IOException("Connection closed unexpectedly");
                            }
                            totalBytesRead += bytesRead;
                        }
                        // 0925 이곳부터 이벤트처리를 밖에서 할지(대공유클래스?) 여기서 분류할지 선택.
                        Trace.WriteLine("서버로부터 클라이언트가 데이터 받음");






                        //SUserModel DesData = (SUserModel) Function.Deserialize(receivedData);
                        DataReceived?.Invoke(receivedData);// event call

                    }
                    else
                    {
                        Trace.WriteLine("Invalid data size received");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e}");
                    break;
                }
            }
        }
    }
}
