using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Procedure;
using UnityEngine;

using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
public class Demo2_ProcedureMenu : ProcedureBase {
	protected override void OnEnter(ProcedureOwner procedureOwner)
	{
		base.OnEnter(procedureOwner);

		Log.Debug("进入菜单流程，可以在这里加载菜单UI。");
	}
}
