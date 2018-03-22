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

    private float moveSpeed = 10;
    protected override void OnInit (object userData) {
        base.OnInit (userData);
    }

    protected override void OnShow (object userData) {
        base.OnShow (userData);

        m_AircraftData = userData as DemoSF_AircraftData;
        if (m_AircraftData == null) {
            Log.Error ("Aircraft data is invalid.");
            //return;
        }
        CachedTransform.localPosition = transform.position;
        CachedTransform.localRotation = transform.rotation;
        CachedTransform.localScale = Vector3.one;

        DemoSF_ScrollableBackground sceneBackground = FindObjectOfType<DemoSF_ScrollableBackground> ();
        if (sceneBackground == null) {
            Log.Warning ("Can not find scene background.");
            //return;
        }

        m_PlayerMoveBoundary = new Rect (sceneBackground.PlayerMoveBoundary.bounds.min.x, sceneBackground.PlayerMoveBoundary.bounds.min.z,
            sceneBackground.PlayerMoveBoundary.bounds.size.x, sceneBackground.PlayerMoveBoundary.bounds.size.z);
    }

    protected override void OnUpdate (float elapseSeconds, float realElapseSeconds) {
        base.OnUpdate (elapseSeconds, realElapseSeconds);

        // 发射子弹
        if (Input.GetMouseButton (0)) {
            Log.Debug ("Fire");

            Vector3 point = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            m_TargetPosition = new Vector3 (point.x, 0f, point.z);
        }

        Vector3 direction = m_TargetPosition - CachedTransform.localPosition;
        if (direction.sqrMagnitude <= Vector3.kEpsilon) {
            return;
        }

        // 移动
        Vector3 speed = Vector3.ClampMagnitude (direction.normalized * moveSpeed * elapseSeconds, direction.magnitude);

        CachedTransform.localPosition = new Vector3 (
            Mathf.Clamp (CachedTransform.localPosition.x + speed.x, m_PlayerMoveBoundary.xMin, m_PlayerMoveBoundary.xMax),
            0f,
            Mathf.Clamp (CachedTransform.localPosition.z + speed.z, m_PlayerMoveBoundary.yMin, m_PlayerMoveBoundary.yMax)
        );
    }

}