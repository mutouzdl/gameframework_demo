

using GameFramework;
using GameFramework.Procedure;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class DemoSF_ProcedureGame : ProcedureBase {
    private DemoSF_SurvivalGame survivalGame = null;

    protected override void OnInit(ProcedureOwner procedureOwner) 
    {
        base.OnInit(procedureOwner);

        survivalGame = new DemoSF_SurvivalGame();
    }

    protected override void OnEnter (ProcedureOwner procedureOwner) {
        base.OnEnter (procedureOwner);

        Log.Debug("进入游戏");
        survivalGame.Initialize();
    }
}