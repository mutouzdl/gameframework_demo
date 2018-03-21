using System.Collections;
using System.Collections.Generic;
using GameFramework.Procedure;
using UnityEngine;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class DemoSF_ProcedureLaunch : ProcedureBase {
	protected override void OnEnter (ProcedureOwner procedureOwner) {
		base.OnEnter (procedureOwner);

		// 切换到菜单场景
		DemoSF_GameEntry.Scene.LoadScene ("DemoSF_Menu", this);

		// 切换到菜单流程
		ChangeState<DemoSF_ProcedureMenu> (procedureOwner);
	}
}