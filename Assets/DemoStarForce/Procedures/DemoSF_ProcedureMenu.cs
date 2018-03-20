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

    protected override void OnEnter (ProcedureOwner procedureOwner) {
        base.OnEnter (procedureOwner);

        GameEntry.Event.Subscribe (OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

        GameEntry.UI.OpenUIForm ("Assets/DemoStarForce/UI/UIForms/MenuForm.prefab", "DefaultGroup", this);
    }
    protected override void OnUpdate (ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds) {
        base.OnUpdate (procedureOwner, elapseSeconds, realElapseSeconds);

        if (m_StartGame) {
            m_StartGame = false;
            
            // 卸载所有场景
            string[] loadedSceneAssetNames = GameEntry.Scene.GetLoadedSceneAssetNames();
            for (int i = 0; i < loadedSceneAssetNames.Length; i++)
            {
                GameEntry.Scene.UnloadScene(loadedSceneAssetNames[i]);
            }

            // 切换到游戏场景
            GameEntry.Scene.LoadScene ("DemoSF_Game", this);

            // 切换到游戏流程
            ChangeState<DemoSF_ProcedureGame> (procedureOwner);
        }
    }
    private void OnOpenUIFormSuccess (object sender, GameEventArgs e) {
        OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs) e;

        // 判断userData是否为自己
        if (ne.UserData != this) {
            return;
        }

        Log.Debug ("UI_Menu：恭喜你，成功地召唤了我。");
    }

    public void StartGame () {
        m_StartGame = true;
    }
}