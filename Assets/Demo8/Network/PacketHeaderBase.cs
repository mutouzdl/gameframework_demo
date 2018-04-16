using GameFramework;
using GameFramework.Network;
using ProtoBuf;

namespace StarForce
{
    public abstract class PacketHeaderBase : IPacketHeader, IReference
    {
        public abstract PacketType PacketType
        {
            get;
        }

        /* 注意,abstract是木头加上的,为的是能在子类中重写并加上ProtoMember特性,以便可以使用protobuf序列化 */
        public abstract int Id
        {
            get;
            set;
        }

        /* 注意,abstract是木头加上的,为的是能在子类中重写并加上ProtoMember特性,以便可以使用protobuf序列化 */
        public abstract int PacketLength
        {
            get;
            set;
        }

        public bool IsValid
        {
            get
            {
                return PacketType != PacketType.Undefined && Id > 0 && PacketLength >= 0;
            }
        }

        public void Clear()
        {
            Id = 0;
            PacketLength = 0;
        }
    }
}
