using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
public class Demo4_ProcedureMenu : ProcedureBase {
	protected override void OnEnter(ProcedureOwner procedureOwner)
	{
		base.OnEnter(procedureOwner);

		// 加载框架Event组件
		EventComponent Event
			= UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();

		// 加载框架UI组件
		UIComponent UI
			= UnityGameFramework.Runtime.GameEntry.GetComponent<UIComponent>();

		// 订阅UI加载成功事件
		Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

		// 加载UI
		UI.OpenUIForm("Assets/Demo4/UI_Menu.prefab", "DefaultGroup", this);
	}
	
	private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
	{
		OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;

		// 判断userData是否为自己
		if (ne.UserData != this)
		{
			return;
		}

		Log.Debug("UI_Menu：恭喜你，成功地召唤了我。");
	}
}
