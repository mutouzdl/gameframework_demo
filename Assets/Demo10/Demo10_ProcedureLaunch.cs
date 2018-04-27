using GameFramework;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class Demo10_ProcedureLaunch : ProcedureBase {
	protected override void OnEnter(ProcedureOwner procedureOwner)
	{
		base.OnEnter(procedureOwner);

		// 获取框架实体组件
		EntityComponent entityComponent
			= UnityGameFramework.Runtime.GameEntry.GetComponent<EntityComponent>();

		// 创建实体
		entityComponent.ShowEntity<Demo10_HeroLogic>(1, "Assets/Demo10/CubeEntity.prefab", "EntityGroup");
	}
}

