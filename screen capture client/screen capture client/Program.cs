using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace screen_capture_client
{
    class Program
    {
        static void Main(string[] args)
        {
            screen_capture_client.FTClient klien = new screen_capture_client.FTClient();
            klien.GetFile();
            Console.ReadLine();
        }
    }
}
