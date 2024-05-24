using ConsoleApp1;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args) { Server("Server"); }


        public static void Server(string name)
        {
            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Any, 0);

            Console.WriteLine("Сервер ждет сообщение от клиента");

            while (true)
            {
                byte[] buffer = udpClient.Receive(ref iPEndPoint);

                if (buffer == null)
                    break;
                var messageText = Encoding.UTF8.GetString(buffer);

                Message message = Message.DeserializeFromJson(messageText);
                message.Print();

                // Отправка подтверждение об успешном получении сообщения
                byte[] confirmationData = Encoding.UTF8.GetBytes("Сообщение успешно получено");
                udpClient.Send(confirmationData, confirmationData.Length, iPEndPoint);
            }
        }
    }
}