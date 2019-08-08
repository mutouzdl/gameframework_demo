using GameFramework;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

public class Demo10_HeroIdleState : FsmState<Demo10_HeroLogic>
{
    /// <summary>
    /// 有限状态机状态初始化时调用。
    /// </summary>
    /// <param name="fsm">有限状态机引用。</param>
    protected override void OnInit (IFsm<Demo10_HeroLogic> fsm) { }

    /// <summary>
    /// 有限状态机状态进入时调用。
    /// </summary>
    /// <param name="fsm">有限状态机引用。</param>
    protected override void OnEnter (IFsm<Demo10_HeroLogic> fsm)
    {
        Log.Info("进入站立状态");
    }

    /// <summary>
    /// 有限状态机状态轮询时调用。
    /// </summary>
    /// <param name="fsm">有限状态机引用。</param>
    /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
    /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
    protected override void OnUpdate (IFsm<Demo10_HeroLogic> fsm, float elapseSeconds, float realElapseSeconds)
    {
        /* 按W、S或者上下方向键移动 */
        float inputVertical = Input.GetAxis("Vertical");
        if (inputVertical != 0)
        {
            /* 移动 */
            ChangeState<Demo10_HeroWalkState>(fsm);
        }
    }

    /// <summary>
    /// 有限状态机状态离开时调用。
    /// </summary>
    /// <param name="fsm">有限状态机引用。</param>
    /// <param name="isShutdown">是否是关闭有限状态机时触发。</param>
    protected override void OnLeave (IFsm<Demo10_HeroLogic> fsm, bool isShutdown)
    {

    }

    /// <summary>
    /// 有限状态机状态销毁时调用。
    /// </summary>
    /// <param name="fsm">有限状态机引用。</param>
    protected override void OnDestroy (IFsm<Demo10_HeroLogic> fsm)
    {
        base.OnDestroy(fsm);
    }

}