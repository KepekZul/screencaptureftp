using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.Sockets;

namespace screen_capture_transfer
{
    class Program
    {

        static void Main(string[] args)
        {
            FTServer serverObj = new FTServer();
            Thread serverThread = new Thread(new ThreadStart(serverObj.StartServer));
            while (true)
            {
                string command = Console.ReadLine();
                System.Diagnostics.Debug.WriteLine(command);
                if (command.ToLower() == "start")
                {
                    System.Diagnostics.Debug.WriteLine("masuk start");
                    serverThread.Start();
                    Console.WriteLine("Server Started....");
                    System.Diagnostics.Debug.WriteLine("hampir keluar start");
                }
                else if (command.ToLower() == "stop")
                {
                    serverObj.StopServer();
                    serverThread.Abort();
                    Console.WriteLine("Server Stoped....");
                    break;
                }
            }
            Console.ReadLine();
        }
    }
}
