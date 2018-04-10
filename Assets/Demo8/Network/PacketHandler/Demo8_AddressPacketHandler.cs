using GameFramework;
using GameFramework.Network;
using protobuf_net;
using StarForce;

public class Demo8_AddressPacketHandler : PacketHandlerBase {
    public override int Id {
        get {
            return 10;
        }
    }

    public override void Handle (object sender, Packet packet) {
        Address packetImpl = (Address) packet;
        Log.Info ("Receive packet '{0}'.", packetImpl.Line1);
    }
}