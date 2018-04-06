using System.Net;
using System.Text;
using GameFramework;
using GameFramework.Event;
using GameFramework.Procedure;
using UnityEngine;
using StarForce;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
using protobuf_net;

public class Demo8_ProcedureLaunch : ProcedureBase {
	private GameFramework.Network.INetworkChannel m_Channel;
	private NetworkChannelHelper m_NetworkChannelHelper;

	protected override void OnEnter (ProcedureOwner procedureOwner) {
		base.OnEnter (procedureOwner);

		// 启动服务器
		Demo8_SocketServer.Start();
		// Program.beginDemo();

		// 获取框架事件组件
		EventComponent Event
			= UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent> ();

		Event.Subscribe (NetworkConnectedEventArgs.EventId, OnConnected);

		// 获取框架网络组件
		NetworkComponent Network
			= UnityGameFramework.Runtime.GameEntry.GetComponent<NetworkComponent> ();

		// 创建频道
		m_NetworkChannelHelper = new NetworkChannelHelper();
		m_Channel = Network.CreateNetworkChannel ("testName", m_NetworkChannelHelper);

		// 连接服务器
		m_Channel.Connect (IPAddress.Parse ("127.0.0.1"), 8098);
		// m_Channel.Connect (IPAddress.Parse ("127.0.0.1"), 9527);
	}

	private void OnConnected (object sender, GameEventArgs e) {
		NetworkConnectedEventArgs ne = (NetworkConnectedEventArgs) e;

		Log.Debug ("连接成功");

		// 发送消息给服务端
		//m_Channel.Send(Encoding.UTF8.GetBytes("hello"));

		var data = m_NetworkChannelHelper.Serialize(new Person() {
			Name = "helloworld!!!!!!!!!!!!!!",
		});
		Log.Debug("data:" + data);
		m_Channel.Send(data);
	}
}