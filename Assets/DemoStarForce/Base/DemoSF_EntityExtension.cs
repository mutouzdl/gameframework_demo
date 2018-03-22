using System;

/// <summary>
/// 实体辅助类，官方Demo中用的是扩展函数的形式，为了避免大家混乱，这里仅用普通静态函数
/// </summary>
public static class DemoSF_EntityExtension {
    // 关于 EntityId 的约定：
    // 0 为无效
    // 正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
    // 负值用于本地生成的临时实体（如特效、FakeObject等）
    private static int s_SerialId = 0;

    public static int GenerateSerialId () {
        return --s_SerialId;
    }
}