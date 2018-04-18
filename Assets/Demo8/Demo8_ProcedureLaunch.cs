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
using ProtoBuf;
using ProtoBuf.Meta;

public class Demo8_ProcedureLaunch : ProcedureBase {
	public static bool isClose = false;
	private GameFramework.Network.INetworkChannel m_Channel;
	private NetworkChannelHelper m_NetworkChannelHelper;

	private float time = 0;
	protected override void OnEnter (ProcedureOwner procedureOwner) {
		base.OnEnter (procedureOwner);

		// 启动服务器(服务端的代码随便找随便改的，大家可以忽略，假设有个服务端就行了)
		Demo8_SocketServer.Start ();

		// 获取框架事件组件
		EventComponent Event
			= UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent> ();

		Event.Subscribe (NetworkConnectedEventArgs.EventId, OnConnected);

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

		// 2秒后关闭连接（只是演示用的，实际中根据情况关闭连接）
		time += elapseSeconds;
		if (time > 2 && Demo8_SocketServer.isOpen) {
			Demo8_SocketServer.isOpen = false;
			m_Channel.Close ();
		}
	}

	private void OnConnected (object sender, GameEventArgs e) {
		NetworkConnectedEventArgs ne = (NetworkConnectedEventArgs) e;

		// 发送消息给服务端
		m_Channel.Send (new CSHello () {
			Name = "服务器你好吗？",
		});
	}
}