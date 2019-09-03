using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassDemoEchoServer
{
    class Worker
    {
        public void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Loopback, 3002); //Loopback = lokalmaskine
            server.Start();

            while (true)
            {


                Task.Run(() =>
                    {
                        TcpClient socket = server.AcceptTcpClient(); //Venter på client
                        DoClient(socket);
                    }
                );
            }

        }

        public void DoClient(TcpClient socket)
        {
            using (StreamReader sr = new StreamReader(socket.GetStream()))
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            {
                string str = sr.ReadLine();
                string[] cwords = new string[] { };
                if (str != null)
                {
                    cwords = str.Split(' ');
                }

                List<int> resarr2 = new List<int>();
                for (int i = 0; i < cwords.Length; i++)
                {
                    resarr2.Add((Convert.ToInt32(cwords[i])));
                }

                //int count = cwords.Length;
                //sw.WriteLine(str + " Number of words: " + count);
                int total = 0;
                for (int i = 0; i < resarr2.Count; i++)
                {
                    total = total + resarr2[i];
                }

                sw.WriteLine(total);

                Thread.Sleep(5000);
                sw.Flush(); //Samler til en tilpas mængde og sender afsted - Flush ignorererererr dette og gør det med det samme (skyller ud).
            }
        }
    }
}
