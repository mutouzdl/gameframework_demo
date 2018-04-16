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

        public abstract int Id
        {
            get;
            set;
        }

        public abstract int PacketLength
        {
            get;
            set;
        }

        public bool IsValid
        {
            get
            {
                Log.Warning("PacketType != PacketType.Id:" + Id);
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
