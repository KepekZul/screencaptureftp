using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;

namespace screen_capture_client
{
    class FTClient
    {
        public  void GetFile()
        {
            try
            {
                IPAddress[] ipAddress = Dns.GetHostAddresses("127.0.0.1");
                IPEndPoint ipEnd = new IPEndPoint(ipAddress[0], 5656);
                Socket clientSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                #region transplant kodingan
                clientSock.Connect(ipEnd);
                //clientSock.Listen(100);
                //clientSock = clientSock.Accept();
                byte[] clientData = new byte[1024 * 10000];

                int receivedBytesLen = clientSock.Receive(clientData);


                int fileNameLen = BitConverter.ToInt32(clientData, 0);
                string fileName = Encoding.ASCII.GetString(clientData, 4, fileNameLen);

                BinaryWriter bWrite = new BinaryWriter(File.Open(fileName, FileMode.Append)); ;
                bWrite.Write(clientData, 4 + fileNameLen, receivedBytesLen - 4 - fileNameLen);

                System.Diagnostics.Debug.WriteLine("selesai");
                bWrite.Close();
                clientSock.Close();
                #endregion

            }
            catch (Exception ex)
            {
                if (ex.Message == "No connection could be made because the target machine actively refused it")
                    System.Diagnostics.Debug.WriteLine("File Sending fail. Because server not running.");
                else
                    System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
