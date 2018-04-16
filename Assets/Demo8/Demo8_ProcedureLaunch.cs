using System.Net;
using System.Text;
using GameFramework;
using GameFramework.Event;
using GameFramework.Procedure;
using StarForce;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using System;
using System.IO;
using protobuf_net;
using ProtoBuf;
using ProtoBuf.Meta;
using QuickStart;

public class Demo8_ProcedureLaunch : ProcedureBase {
	public static bool isClose = false;
	private GameFramework.Network.INetworkChannel m_Channel;
	private NetworkChannelHelper m_NetworkChannelHelper;

	private float time = 0;
	protected override void OnEnter (ProcedureOwner procedureOwner) {
		base.OnEnter (procedureOwner);

		// Sockets.ShowSockets();

		// Address myResponse = new Address ();
		// myResponse.Line1 = "Server:测试地址！";
		// byte[] datas = null;
		// using (MemoryStream memoryStream = new MemoryStream ()) {
		// 	SCPacketHeader packetHeader = ReferencePool.Acquire<SCPacketHeader> ();
		// 	packetHeader.Id = 1;
		// 	packetHeader.PacketLength = 123;
		// 	Serializer.SerializeWithLengthPrefix (memoryStream, packetHeader, PrefixStyle.Fixed32);
		// 	// Serializer.SerializeWithLengthPrefix (memoryStream, myResponse, PrefixStyle.Fixed32);

		// 	ReferencePool.Release (packetHeader);
		// 	Log.Error ("memoryStream len:" + memoryStream.Length);

		// 	memoryStream.Position = 0;
		// 	SCPacketHeader header = Serializer.DeserializeWithLengthPrefix<SCPacketHeader> (memoryStream, PrefixStyle.Fixed32);
		// 	Log.Error ("header:" + header);
		// }

		// 启动服务器
		Demo8_SocketServer.Start ();
		// Program.beginDemo();

		// 获取框架事件组件
		EventComponent Event
			= UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent> ();

		Event.Subscribe (NetworkConnectedEventArgs.EventId, OnConnected);

		// RuntimeTypeModel.Default.Deserialize (new MemoryStream (datas),
		// 	ReferencePool.Acquire<SCPacketHeader> (), typeof (SCPacketHeader));

		// 获取框架网络组件
		NetworkComponent Network
			= UnityGameFramework.Runtime.GameEntry.GetComponent<NetworkComponent> ();

		// 创建频道
		m_NetworkChannelHelper = new NetworkChannelHelper ();
		m_Channel = Network.CreateNetworkChannel ("testName", m_NetworkChannelHelper);

		// 连接服务器
		m_Channel.Connect (IPAddress.Parse ("127.0.0.1"), 8098);
	}

	protected override void OnUpdate (ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds) {
		if (m_Channel == null) {
			return;
		}
		time += elapseSeconds;
		if (time > 1 && Demo8_SocketServer.isOpen) {
			Demo8_SocketServer.isOpen = false;
			m_Channel.Close ();
		}
	}

	private void OnConnected (object sender, GameEventArgs e) {
		NetworkConnectedEventArgs ne = (NetworkConnectedEventArgs) e;

		Log.Debug ("连接成功");

		// 发送消息给服务端
		m_Channel.Send (new Person () {
			Name = "helloworld!!!!!!!!!!!!!!",
		});
	}
}