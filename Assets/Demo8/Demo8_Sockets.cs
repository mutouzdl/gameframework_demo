
using System.Net.Sockets;
using System;
using ProtoBuf;
using System.Net;
using System.Threading;
using GameFramework;
using protobuf_net;

namespace QuickStart
{
    static class Sockets
    {

        const int PORT = 12345;

        /// <summary>
        /// Demonstrates cor sockets functionality for sending data;
        /// RPC is not covered by this sample.
        /// Note that this example should not be taken as best practice
        /// for working with sockets more generally - it is simply
        /// intended to illustrate basic reading/writing to/from a socket.
        /// </summary>
        internal static void ShowSockets()
        {
            TcpListener server = new TcpListener(IPAddress.Loopback, PORT);
            server.Start();
            server.BeginAcceptTcpClient(ClientConnected, server);
            Log.Info("SERVER: Waiting for client...");

            // ThreadPool.QueueUserWorkItem(RunClient);
            // allDone.WaitOne();
            // server.Stop();
            
            
        }
        static ManualResetEvent allDone = new ManualResetEvent(false);

        static void ClientConnected(IAsyncResult result) {
            Log.Warning("ClientConnected");
            try
            {
                TcpListener server = (TcpListener)result.AsyncState;
                using (TcpClient client = server.EndAcceptTcpClient(result))
                using (NetworkStream stream = client.GetStream())
                {
                    Log.Info("SERVER: Client connected; reading customer len:" + stream.Length);
                    Person cust = Serializer.DeserializeWithLengthPrefix<Person>(stream, PrefixStyle.Fixed32);
                    Log.Info("SERVER: Got person:" + cust.Name);

                    Serializer.SerializeWithLengthPrefix(stream, 123, PrefixStyle.Base128);
                    Serializer.SerializeWithLengthPrefix(stream, cust, PrefixStyle.Base128);

                    // int final = stream.ReadByte();
                    
                    Log.Info("SERVER: Closing connection...");
                    stream.Close();
                    client.Close();
                }
            }
            finally
            {
                allDone.Set();
            }

        }

        static void RunClient(object state)
        {
            Customer cust = Customer.Invent();
            Log.Info("CLIENT: Opening connection...");
            using (TcpClient client = new TcpClient())
            {
                client.Connect(new IPEndPoint(IPAddress.Loopback, PORT));
                using (NetworkStream stream = client.GetStream())
                {
                    Log.Info("CLIENT: Got connection; sending data...");
                    Serializer.SerializeWithLengthPrefix(stream, cust, PrefixStyle.Base128);
                    
                    Log.Info("CLIENT: Attempting to read data...");
                    int header = Serializer.DeserializeWithLengthPrefix<int>(stream, PrefixStyle.Base128);
                    Log.Error("header:" + header);
                    Customer newCust = Serializer.DeserializeWithLengthPrefix<Customer>(stream, PrefixStyle.Base128);
                    Log.Info("CLIENT: Got customer:");
                    newCust.ShowCustomer();

                    Log.Info("CLIENT: Sending happy...");
                    stream.WriteByte(123); // just to show all bidirectional comms are OK
                    Log.Info("CLIENT: Closing...");
                    stream.Close();
                }
                client.Close();
            }
        }
    }
}
