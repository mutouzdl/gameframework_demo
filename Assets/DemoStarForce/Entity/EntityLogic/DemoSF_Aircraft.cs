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
    private DemoSF_Weapon m_Weapon = null;

    public bool IsDead { get; set; }

    private float moveSpeed = 10;
    protected override void OnInit (object userData) {
        base.OnInit (userData);
    }

    protected override void OnShow (object userData) {
        base.OnShow (userData);

        CachedTransform.localPosition = transform.position;
        CachedTransform.localRotation = transform.rotation;
        CachedTransform.localScale = Vector3.one;

        // 移动范围
        DemoSF_ScrollableBackground sceneBackground = FindObjectOfType<DemoSF_ScrollableBackground> ();
        m_PlayerMoveBoundary = new Rect (sceneBackground.PlayerMoveBoundary.bounds.min.x, sceneBackground.PlayerMoveBoundary.bounds.min.z,
            sceneBackground.PlayerMoveBoundary.bounds.size.x, sceneBackground.PlayerMoveBoundary.bounds.size.z);

        // 加载武器，最后一个参数为当前飞机的ID，武器将附加到飞机身上
        DemoSF_GameEntry.Entity.ShowEntity<DemoSF_Weapon>(
            DemoSF_EntityExtension.GenerateSerialId(),
            "Assets/DemoStarForce/Prefabs/DefaultWeapon.prefab",
            "WeaponGroup",
            this.Entity.Id.ToString());
    }

        protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
            base.OnAttached(childEntity, parentTransform, userData);

            if (childEntity is DemoSF_Weapon)
            {
                m_Weapon = (DemoSF_Weapon)childEntity;
            }
        }

    protected override void OnUpdate (float elapseSeconds, float realElapseSeconds) {
        base.OnUpdate (elapseSeconds, realElapseSeconds);

        // 发射子弹
        if (Input.GetMouseButton (0)) {
            Vector3 point = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            m_TargetPosition = new Vector3 (point.x, 0f, point.z);

            if (m_Weapon != null) {
                m_Weapon.TryAttack();
            }
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