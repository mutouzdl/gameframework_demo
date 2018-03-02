using GameFramework;
using GameFramework.Procedure;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

/// <summary>
/// Demo1-HelloWorld
/// </summary>
public class Demo1_ProcedureHelloWorld : ProcedureBase {
	protected override void OnEnter (ProcedureOwner procedureOwner) {
		base.OnEnter (procedureOwner);

		string welcomeMessage = "HelloWorld!";
		Log.Info (welcomeMessage);
		Log.Warning (welcomeMessage);
		Log.Error (welcomeMessage);
	}
}