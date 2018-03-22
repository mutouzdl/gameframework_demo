using System.Collections;
using System.Collections.Generic;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class DemoSF_ProcedureLaunch : ProcedureBase {
	protected override void OnEnter (ProcedureOwner procedureOwner) {
		base.OnEnter (procedureOwner);

		// 切换到菜单场景
		procedureOwner.SetData<VarString>("NextSceneName", "DemoSF_Menu");
		ChangeState<DemoSF_ProcedureChangeScene> (procedureOwner);
	}
}