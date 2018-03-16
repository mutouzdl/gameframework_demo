using System.Collections;
using System.Collections.Generic;
using Demo5;
using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

public class Demo5_ProcedureLaunch : ProcedureBase {
	protected override void OnEnter(ProcedureOwner procedureOwner)
	{
		base.OnEnter(procedureOwner);

		// 获取框架事件组件
		EventComponent Event
			= UnityGameFramework.Runtime.GameEntry.GetComponent<EventComponent>();

		// 获取框架数据表组件
		DataTableComponent DataTable
			= UnityGameFramework.Runtime.GameEntry.GetComponent<DataTableComponent>();

		// 订阅加载成功事件
		Event.Subscribe(UnityGameFramework.Runtime.LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);

		// 加载配置表
		DataTable.LoadDataTable<DRHero>("Hero", "Assets/Demo5/Hero.txt");
	}

	private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
	{
        UnityGameFramework.Runtime.LoadDataTableSuccessEventArgs eventArgs 
			= e as UnityGameFramework.Runtime.LoadDataTableSuccessEventArgs;

        Log.Debug("加载成功:" + eventArgs.DataRowType);

		// 获取框架数据表组件
		DataTableComponent DataTable
			= UnityGameFramework.Runtime.GameEntry.GetComponent<DataTableComponent>();

		// 获得数据表
		IDataTable<DRHero> dtScene = DataTable.GetDataTable<DRHero>();
		
		// 获得所有行
		DRHero[] drHeros = dtScene.GetAllDataRows();

		Log.Debug("drHeros:" + drHeros.Length);

		// 根据行号获得某一行
		DRHero drScene = dtScene.GetDataRow(1); // 或直接使用 dtScene[1]
		if (drScene != null)
		{
			// 此行存在，可以获取内容了
			string name = drScene.Name;
			int atk = drScene.Atk;

        	Log.Debug("name:" + name + ", atk:" + atk);
		}
		else
		{
			// 此行不存在
		}

		// 获得满足条件的所有行
		DRHero[] drScenesWithCondition = dtScene.GetAllDataRows(x => x.Id > 0);
		
		// 获得满足条件的第一行
		DRHero drSceneWithCondition = dtScene.GetDataRow(x => x.Name == "mutou");
	
	}
}

