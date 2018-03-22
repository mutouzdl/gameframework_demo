using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class DemoSF_ProcedureMenu : ProcedureBase {
    private bool m_StartGame = false;
    private DemoSF_MenuFormLogic m_MenuForm = null;

    protected override void OnEnter (ProcedureOwner procedureOwner) {
        base.OnEnter (procedureOwner);

        DemoSF_GameEntry.Event.Subscribe (OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

        DemoSF_GameEntry.UI.OpenUIForm ("Assets/DemoStarForce/UI/UIForms/MenuForm.prefab", "DefaultGroup", this);
    }

    protected override void OnUpdate (ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds) {
        base.OnUpdate (procedureOwner, elapseSeconds, realElapseSeconds);

        if (m_StartGame) {
            m_StartGame = false;

            // 切换到游戏场景
            procedureOwner.SetData<VarString> ("NextSceneName", "DemoSF_Game");
            ChangeState<DemoSF_ProcedureChangeScene> (procedureOwner);
        }
    }

    protected override void OnLeave (ProcedureOwner procedureOwner, bool isShutdown) {
        base.OnLeave (procedureOwner, isShutdown);

        // 离开时取消事件订阅
        DemoSF_GameEntry.Event.Unsubscribe (OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

        // 离开时关闭UI
        if (m_MenuForm != null) {
            DemoSF_GameEntry.UI.CloseUIForm (m_MenuForm.UIForm);
            m_MenuForm = null;
        }
    }

    private void OnOpenUIFormSuccess (object sender, GameEventArgs e) {
        OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs) e;

        // 判断userData是否为自己
        if (ne.UserData != this) {
            return;
        }

        Log.Debug ("UI_Menu：恭喜你，成功地召唤了我。");

        m_MenuForm = (DemoSF_MenuFormLogic) ne.UIForm.Logic;
    }

    public void StartGame () {
        m_StartGame = true;
    }
}