using CommonLib.Service;
using System;

namespace FleaMarketV2Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            SocketServer sockInstance = SocketServer.GetInstance();
            sockInstance.Init<object>();
            Console.WriteLine("서버를 종료하려면 엔터 키를 누르세요.");
            Console.ReadLine();
        }
    }
}
