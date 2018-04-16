using ProtoBuf;

namespace StarForce
{
    [ProtoContract]
    public sealed class CSPacketHeader : PacketHeaderBase
    {
        [ProtoMember (1)]
        public override int Id
        {
            get;
            set;
        }

        [ProtoMember (2)]
        public override int PacketLength
        {
            get;
            set;
        }

        public override PacketType PacketType
        {
            get
            {
                return PacketType.ClientToServer;
            }
        }
    }
}
