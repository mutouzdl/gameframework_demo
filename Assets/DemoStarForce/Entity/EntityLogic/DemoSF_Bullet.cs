using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 子弹类。
/// </summary>
public class DemoSF_Bullet : EntityLogic {
    private float m_Speed = 30f;
    protected override void OnInit (object userData) {
        base.OnInit (userData);
    }

    protected override void OnShow (object userData) {
        base.OnShow (userData);

        Vector3 pos = (Vector3)userData;
        CachedTransform.localPosition = pos;
        CachedTransform.localScale = Vector3.one;
    }

    protected override void OnUpdate (float elapseSeconds, float realElapseSeconds) {
        base.OnUpdate (elapseSeconds, realElapseSeconds);

        CachedTransform.Translate (
            Vector3.forward * m_Speed * elapseSeconds, Space.World);
    }
    
    private void OnTriggerEnter (Collider other) {
        EntityLogic entityLogic = other.gameObject.GetComponent<EntityLogic> ();

        if (entityLogic == null) {
            return;
        }

        // 这只是示例
        if (entityLogic is DemoSF_Asteroid) {
            Log.Warning("攻击敌人！");

            // 消灭敌人
            DemoSF_GameEntry.Entity.HideEntity(entityLogic.Entity.Id);
        }
    }
}