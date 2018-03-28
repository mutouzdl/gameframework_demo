using GameFramework;
using GameFramework.Network;

public class Demo8_PacketHeader : IPacketHeader, IReference
{
    public int Id
    {
        get;
        set;
    }

    public int PacketLength
    {
        get;
        set;
    }

    public void Clear()
    {
        Id = 0;
        PacketLength = 0;
    }
}