using System;
using ProtoBuf;
using StarForce;

[Serializable, ProtoContract (Name = @"CSHello")]
public class CSHello : CSPacketBase {
    public override int Id {
        get {
            return 5;
        }
    }

    [ProtoMember (1)]
    public string Name { get; set; }

    public override void Clear () {

    }
}