using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 战机类。
/// </summary>
public class DemoSF_Aircraft : EntityLogic {
    [SerializeField]
    private DemoSF_AircraftData m_AircraftData = null;

    private Rect m_PlayerMoveBoundary = default (Rect);
    private Vector3 m_TargetPosition = Vector3.zero;

    public bool IsDead { get; set; }
    protected override void OnInit (object userData)
    {
        base.OnInit (userData);
    }

    protected override void OnShow (object userData)
    {
        base.OnShow (userData);

        m_AircraftData = userData as DemoSF_AircraftData;
        if (m_AircraftData == null) {
            Log.Error ("Aircraft data is invalid.");
            return;
        }

        CachedTransform.localPosition = Vector3.zero;
        CachedTransform.localScale = Vector3.one;
    }

    protected override void OnUpdate (float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate (elapseSeconds, realElapseSeconds);

        // 发射子弹
        if (Input.GetMouseButton (0)) {
            Log.Debug("Fire");
        }
    }

}