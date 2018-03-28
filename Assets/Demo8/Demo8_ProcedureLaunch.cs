using System.Net;
using System.Text;
using GameFramework;
using GameFramework.Event;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class Demo8_ProcedureLaunch : ProcedureBase {
	private GameFramework.Network.INetworkChannel m_Channel;

	protected override void OnEnter (ProcedureOwner procedureOwner) {
		base.OnEnter (procedureOwner);

		// 启动服务器
		Demo8_SocketServer.Start();

		// 获取框架事件组件
		EventComponent Event
			= UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent> ();

		Event.Subscribe (NetworkConnectedEventArgs.EventId, OnConnected);

		// 获取框架网络组件
		NetworkComponent Network
			= UnityGameFramework.Runtime.GameEntry.GetComponent<NetworkComponent> ();

		// 创建频道
		m_Channel = Network.CreateNetworkChannel ("testName", new Demo8_NetworkChannelHelper ());

		// 连接服务器
		m_Channel.Connect (IPAddress.Parse ("127.0.0.1"), 8098);
	}

	private void OnConnected (object sender, GameEventArgs e) {
		NetworkConnectedEventArgs ne = (NetworkConnectedEventArgs) e;

		Log.Debug ("连接成功");

		// 发送消息给服务端
		m_Channel.Send(Encoding.UTF8.GetBytes("hello"));
	}
}