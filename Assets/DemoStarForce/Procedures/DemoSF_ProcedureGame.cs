

using GameFramework.Procedure;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class DemoSF_ProcedureGame : ProcedureBase {

    protected override void OnEnter (ProcedureOwner procedureOwner) {
        base.OnEnter (procedureOwner);
    }
}