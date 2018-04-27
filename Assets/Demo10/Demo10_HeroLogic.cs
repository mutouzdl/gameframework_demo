using GameFramework;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// 英雄逻辑处理
/// </summary>
public class Demo10_HeroLogic : EntityLogic {
    private GameFramework.Fsm.IFsm<Demo10_HeroLogic> m_HeroFsm;
    private FsmComponent Fsm = null;

    protected override void OnInit (object userData) {
        base.OnInit (userData);

        Fsm = UnityGameFramework.Runtime.GameEntry.GetComponent<FsmComponent>();

        /* 英雄的所有状态类 */
        FsmState<Demo10_HeroLogic>[] heroStates = new FsmState<Demo10_HeroLogic>[] {
            new Demo10_HeroIdleState (),
            new Demo10_HeroWalkState (),
        };

        /* 创建状态机 */
        m_HeroFsm = Fsm.CreateFsm<Demo10_HeroLogic> (this, heroStates);

        /* 启动站立状态 */
        m_HeroFsm.Start<Demo10_HeroIdleState> ();
    }

    protected override void OnShow (object userData) {
        base.OnShow (userData);
    }

    protected override void OnUpdate (float elapseSeconds, float realElapseSeconds) {
        base.OnUpdate (elapseSeconds, realElapseSeconds);

        /* 旋转镜头，这个可以不用管，和状态机Demo没有太大关系 */
        float inputHorizontal = Input.GetAxis ("Horizontal");
        if (inputHorizontal != 0) {
            transform.Rotate (new Vector3 (0, inputHorizontal * 3, 0));
        }
    }

    protected override void OnHide (object userData) {
        base.OnHide (userData);

        Fsm.DestroyFsm<Demo10_HeroLogic> ();
    }

    /// <summary>
    /// 向前移动
    /// </summary>
    /// <param name="distance"></param>
    public void Forward (float distance) {
        transform.position += transform.forward * distance * 5;
    }
}