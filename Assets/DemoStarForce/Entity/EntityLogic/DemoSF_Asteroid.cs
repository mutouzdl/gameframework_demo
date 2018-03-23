using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 小行星类。
/// </summary>
public class DemoSF_Asteroid : EntityLogic {
    private Vector3 m_RotateSphere = Vector3.zero;

    private float m_Speed = 5f;
    private float m_AngularSpeed = 2f;

    protected override void OnInit (object userData) {
        base.OnInit (userData);
    }

    protected override void OnShow (object userData) {
        base.OnShow (userData);

        m_RotateSphere = Random.insideUnitSphere;
    }

    protected override void OnUpdate (float elapseSeconds, float realElapseSeconds) {
        base.OnUpdate (elapseSeconds, realElapseSeconds);

        CachedTransform.Translate (Vector3.back * m_Speed * elapseSeconds, Space.World);
        CachedTransform.Rotate (m_RotateSphere * m_AngularSpeed * elapseSeconds, Space.Self);
    }
}