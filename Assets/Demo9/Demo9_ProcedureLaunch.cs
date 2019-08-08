using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class Demo9_ProcedureLaunch : ProcedureBase
{
    private bool m_ResourceInitComplete = false;

    protected override void OnEnter (ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);

        ResourceComponent Resource
            = UnityGameFramework.Runtime.GameEntry.GetComponent<ResourceComponent>();

        // 初始化资源
        Resource.InitResources(() => {
            OnResourceInitComplete();
        });
    }

    protected override void OnUpdate (ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        if (!m_ResourceInitComplete)
        {
            return;
        }

        SceneComponent Scene
            = UnityGameFramework.Runtime.GameEntry.GetComponent<SceneComponent>();

        // 切换场景
        Scene.LoadScene("Assets/Demo9/Demo9_Menu.unity", this);

        // 切换流程
        ChangeState<Demo9_ProcedureMenu>(procedureOwner);
    }

    private void OnResourceInitComplete ()
    {
        m_ResourceInitComplete = true;

        Log.Info("初始化资源成功");
    }
}