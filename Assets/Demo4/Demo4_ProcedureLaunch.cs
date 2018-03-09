using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class Demo4_ProcedureLaunch : ProcedureBase {
	protected override void OnEnter(ProcedureOwner procedureOwner)
	{
		base.OnEnter(procedureOwner);

		SceneComponent scene
			= UnityGameFramework.Runtime.GameEntry.GetComponent<SceneComponent>();

		// 切换场景
		scene.LoadScene("Demo4_Menu", this);

		// 切换流程
        ChangeState<Demo4_ProcedureMenu>(procedureOwner);
	}
}

