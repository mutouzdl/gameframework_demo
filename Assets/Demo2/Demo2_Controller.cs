using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

public class Demo2_Controller : MonoBehaviour {
	public void EnterGame() {
		// 获取框架场景组件
		SceneComponent Scene
			= UnityGameFramework.Runtime.GameEntry.GetComponent<SceneComponent>();

		// 卸载所有场景
		string[] loadedSceneAssetNames = Scene.GetLoadedSceneAssetNames();
		for (int i = 0; i < loadedSceneAssetNames.Length; i++)
		{
			Scene.UnloadScene(loadedSceneAssetNames[i]);
		}

		// 加载游戏场景
		Scene.LoadScene("Demo2_Game", this);
	}
}
