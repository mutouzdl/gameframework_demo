using System;
using ProtoBuf;

namespace StarForce
{
    [Serializable, ProtoContract(Name = @"SCPacketHeader")]
    public sealed class SCPacketHeader : PacketHeaderBase
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
                return PacketType.ServerToClient;
            }
        }
    }
}
