using GameFramework;
using UnityGameFramework.Runtime;

/// <summary>
/// 英雄逻辑处理
/// </summary>
public class Demo6_HeroLogic : EntityLogic
{
    protected Demo6_HeroLogic()
    {
    }

    protected override void OnShow (object userData) {
        base.OnShow (userData);

        Log.Debug("显示英雄实体.");
    }
}