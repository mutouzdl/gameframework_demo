using GameFramework;
using GameFramework.DataTable;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

public class DemoSF_SurvivalGame {
    private DemoSF_ScrollableBackground m_SceneBackground = null;

    public bool GameOver {
        get;
        protected set;
    }

    private float m_ElapseSeconds = 0f;

    private DemoSF_Aircraft m_MyAircraft = null;

    public void Initialize () {
        DemoSF_GameEntry.Event.Subscribe (ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        DemoSF_GameEntry.Event.Subscribe (ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);

        m_SceneBackground = Object.FindObjectOfType<DemoSF_ScrollableBackground> ();

        // 创建实体
        DemoSF_GameEntry.Entity.ShowEntity<DemoSF_Aircraft> (
            DemoSF_EntityExtension.GenerateSerialId (),
            "Assets/DemoStarForce/Prefabs/PlayerShip.prefab",
            "PlayerGroup");

        GameOver = false;
        m_MyAircraft = null;
    }

    public void Shutdown () {
        DemoSF_GameEntry.Event.Unsubscribe (ShowEntitySuccessEventArgs.EventId, OnShowEntitySuccess);
        DemoSF_GameEntry.Event.Unsubscribe (ShowEntityFailureEventArgs.EventId, OnShowEntityFailure);
    }

    public void Update (float elapseSeconds, float realElapseSeconds) {
        if (m_MyAircraft != null && m_MyAircraft.IsDead) {
            GameOver = true;
            return;
        }

        m_ElapseSeconds += elapseSeconds;
        if (m_ElapseSeconds >= 1f) {
            m_ElapseSeconds = 0f;

            float randomPositionX = m_SceneBackground.EnemySpawnBoundary.bounds.min.x + m_SceneBackground.EnemySpawnBoundary.bounds.size.x * (float) Utility.Random.GetRandomDouble ();
            float randomPositionZ = m_SceneBackground.EnemySpawnBoundary.bounds.min.z + m_SceneBackground.EnemySpawnBoundary.bounds.size.z * (float) Utility.Random.GetRandomDouble ();

            DemoSF_GameEntry.Entity.ShowEntity<DemoSF_Asteroid> (
                DemoSF_EntityExtension.GenerateSerialId (),
                "Assets/DemoStarForce/Prefabs/Asteroid01.prefab",
                "AsteroidGroup",
                new Vector3 (randomPositionX, 0f, randomPositionZ));
        }
    }

    protected void OnShowEntitySuccess (object sender, GameEventArgs e) {
        ShowEntitySuccessEventArgs ne = (ShowEntitySuccessEventArgs) e;
        if (ne.EntityLogicType == typeof (DemoSF_Aircraft)) {
            m_MyAircraft = (DemoSF_Aircraft) ne.Entity.Logic;
        }
    }

    protected void OnShowEntityFailure (object sender, GameEventArgs e) {
        ShowEntityFailureEventArgs ne = (ShowEntityFailureEventArgs) e;
        Log.Warning ("Show entity failure with error message '{0}'.", ne.ErrorMessage);
    }
}