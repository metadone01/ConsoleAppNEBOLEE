using AdvancedSharpAdbClient;
using AdvancedSharpAdbClient.DeviceCommands;
using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;


namespace AdbTest
{
    class Program
    {
        static AdbClient client;

        static DeviceData device;

        static void Main(string[] args)
        {
            if (!AdbServer.Instance.GetStatus().IsRunning)
            {
                AdbServer server = new AdbServer();
                StartServerResult result = server.StartServer(@"""C:\Users\я\OneDrive\Рабочий стол\platform-tools\adb.exe""", false);
                if (result != StartServerResult.Started)
                {
                    Console.WriteLine("Can't start adb server");
                    return;
                }
            }

            client = new AdbClient();
            client.Connect("127.0.0.1:62001"); // Ip Nox'а
            device = client.GetDevices().FirstOrDefault(); // Выбираем девайс из подключенных
            if (device == null)
            {
                Console.WriteLine("Can't connect to device");
                return;
            }

            client.StartApp(device, "org.telegram.messenger");
            client.Click(device, 516, 220);
            client.Click(device, 516, 220);
            client.SendText(device, "Mom, i'm jakal, i'm write on C#");

            
        }


    }
}