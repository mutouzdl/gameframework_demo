using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using ProtoBuf;
using ProtoBuf.Meta;
using StarForce;

namespace protobuf_net {
    [Serializable, ProtoContract(Name = @"Person")]
    public class Person : CSPacketBase {
        public override int Id {
            get {
                return 5;
            }
        }

        [ProtoMember (1)]
        public string Name { get; set; }

        public override void Clear() {

        }
    }

    [Serializable, ProtoContract(Name = @"Address")]
    public class Address : SCPacketBase {
        public override int Id {
            get {
                return 10;
            }
        }

        [ProtoMember (1)]
        public string Line1 { get; set; }

        public override void Clear() {
            
        }
    }

    public class Program {
        private static ManualResetEvent allDone = new ManualResetEvent (false);

        public static void beginDemo () {
            //启动服务端
            TcpListener server = new TcpListener (IPAddress.Parse ("127.0.0.1"), 9527);
            server.Start ();
            server.BeginAcceptTcpClient (clientConnected, server);
            Console.WriteLine ("SERVER : 等待数据 ---");

            allDone.WaitOne ();

            Console.WriteLine ("SERVER : 退出 ---");
            server.Stop ();
        }

        //服务端处理
        private static void clientConnected (IAsyncResult result) {
            try {
                TcpListener server = (TcpListener) result.AsyncState;
                using (TcpClient client = server.EndAcceptTcpClient (result))
                using (NetworkStream stream = client.GetStream ()) {
                    //获取
                    Console.WriteLine ("SERVER : 客户端已连接，读取数据 ---");
                    //proto-buf 使用 Base128 Varints 编码
                    Person myRequest = Serializer.DeserializeWithLengthPrefix<Person> (stream, PrefixStyle.Base128);

                    Console.WriteLine ("SERVER :获取成功:" + myRequest.Name);

                    //响应(MyResponse)
                    Address myResponse = new Address ();
                    myResponse.Line1 = "14";
                    Serializer.SerializeWithLengthPrefix (stream, myResponse, PrefixStyle.Base128);
                    Console.WriteLine ("SERVER : 响应成功 ---");

                    Console.WriteLine ("SERVER: 关闭连接 ---");
                    stream.Close ();
                    client.Close ();
                }
            } finally {
                allDone.Set ();
            }
        }

    } //end class
}