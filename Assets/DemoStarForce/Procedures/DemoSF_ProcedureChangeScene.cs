using System;
using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public partial class DemoSF_ProcedureChangeScene : ProcedureBase {
    private bool m_IsChangeSceneComplete = false;

    protected override void OnEnter (ProcedureOwner procedureOwner) {
        base.OnEnter (procedureOwner);

        m_IsChangeSceneComplete = false;

        DemoSF_GameEntry.Event.Subscribe (LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);

        // 卸载所有场景
        string[] loadedSceneAssetNames = DemoSF_GameEntry.Scene.GetLoadedSceneAssetNames ();
        for (int i = 0; i < loadedSceneAssetNames.Length; i++) {
            DemoSF_GameEntry.Scene.UnloadScene (loadedSceneAssetNames[i]);
        }

        string nextSceneName = procedureOwner.GetData<VarString> ("NextSceneName").Value;

        DemoSF_GameEntry.Scene.LoadScene (nextSceneName, this);
    }

    protected override void OnLeave (ProcedureOwner procedureOwner, bool isShutdown) {
        DemoSF_GameEntry.Event.Unsubscribe (LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);

        base.OnLeave (procedureOwner, isShutdown);
    }

    protected override void OnUpdate (ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds) {
        base.OnUpdate (procedureOwner, elapseSeconds, realElapseSeconds);

        if (!m_IsChangeSceneComplete) {
            return;
        }

        string nextSceneName = procedureOwner.GetData<VarString> ("NextSceneName").Value;
        switch(nextSceneName)
        {
            case "DemoSF_Menu":
                ChangeState<DemoSF_ProcedureMenu> (procedureOwner);
            break;
            case "DemoSF_Game":
                ChangeState<DemoSF_ProcedureGame> (procedureOwner);
            break;
        }
    }

    private void OnLoadSceneSuccess (object sender, GameEventArgs e) {
        LoadSceneSuccessEventArgs ne = (LoadSceneSuccessEventArgs) e;
        if (ne.UserData != this) {
            return;
        }

        m_IsChangeSceneComplete = true;
    }

}