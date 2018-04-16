using GameFramework;
using GameFramework.Network;
using StarForce;

public class Demo8_HelloPacketHandler : PacketHandlerBase {
    public override int Id {
        get {
            return 10;
        }
    }

    public override void Handle (object sender, Packet packet) {
        SCHello packetImpl = (SCHello) packet;
        Log.Info ("Demo8_HelloPacketHandler 收到消息： '{0}'.", packetImpl.Name);
    }
}