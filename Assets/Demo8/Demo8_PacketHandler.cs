
using GameFramework;
using GameFramework.Network;

public class Demo8_PacketHandler : IPacketHandler
{
    private readonly int _id = 123;

    public int Id
    {
        get
        {
            return _id;
        }
    }

    public void Handle(object sender, Packet packet)
    {
        Log.Debug("packet:" + packet);
    }
}