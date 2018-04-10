using ProtoBuf;

namespace StarForce
{
    [ProtoContract]
    public sealed class SCPacketHeader : PacketHeaderBase
    {
        public override PacketType PacketType
        {
            get
            {
                return PacketType.ServerToClient;
            }
        }
    }
}
