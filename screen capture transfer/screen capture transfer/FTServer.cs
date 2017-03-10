using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace screen_capture_transfer
{
    class FTServer
    {
        IPEndPoint ipEnd;
        Socket sock;
        public FTServer()
        {
            ipEnd = new IPEndPoint(IPAddress.Any, 5656);
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            sock.Bind(ipEnd);
        }
        public static string curMsg = "Stopped";
        public void StartServer()
        {
            while (true)
            {
                try
                {
                    System.Diagnostics.Debug.WriteLine("sebelum listen");
                    sock.Listen(100);
                    System.Diagnostics.Debug.WriteLine("setelah listen");
                    Socket clientSock = sock.Accept();
                    System.Diagnostics.Debug.WriteLine("setelah accept");
                    #region transplant kodingan
                    ScreenShot.getScreenShot().Save("screenshot.png");
                    string fileName = "screenshot.png";

                    byte[] fileNameByte = Encoding.ASCII.GetBytes(fileName);
                    if (fileNameByte.Length > 850 * 1024)
                    {
                        return;
                    }

                    curMsg = "Buffering ...";
                    byte[] fileData = File.ReadAllBytes(fileName);
                    byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                    byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);

                    fileNameLen.CopyTo(clientData, 0);
                    fileNameByte.CopyTo(clientData, 4);
                    fileData.CopyTo(clientData, 4 + fileNameByte.Length);

                    clientSock.Send(clientData);

                    clientSock.Close();
                    System.Diagnostics.Debug.Write("apakah stuck di sini?");
                    #endregion
                }
                catch (Exception ex)
                {
                    curMsg = "File Receving error.";
                }
            }
        }
        public void StopServer()
        {
            sock.Close();
            System.Diagnostics.Debug.WriteLine("server distop");
        }
    }
}
