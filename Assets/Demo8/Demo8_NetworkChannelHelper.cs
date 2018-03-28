using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GameFramework;
using GameFramework.Event;
using GameFramework.Network;
using ProtoBuf;
using ProtoBuf.Meta;

public class Demo8_NetworkChannelHelper : INetworkChannelHelper {
    public int PacketHeaderLength {
        get {
            return sizeof (int);
        }
    }
    /// <summary>
    /// 反序列化消息包。
    /// </summary>
    /// <param name="packetHeader">消息包头。</param>
    /// <param name="source">要反序列化的来源流。</param>
    /// <param name="customErrorData">用户自定义错误数据。</param>
    /// <returns>反序列化后的消息包。</returns>
    public Packet DeserializePacket (IPacketHeader packetHeader, Stream source, out object customErrorData) {
        // 注意：此函数并不在主线程调用！
        customErrorData = null;

        Demo8_PacketHeader scPacketHeader = packetHeader as Demo8_PacketHeader;
        if (scPacketHeader == null) {
            Log.Warning ("Packet header is invalid.");
            return null;
        }

        Packet packet = (Packet) RuntimeTypeModel.Default.DeserializeWithLengthPrefix (
            source, ReferencePool.Acquire (packetType), packetType, PrefixStyle.Fixed32, 0);

        ReferencePool.Release (scPacketHeader);
        return packet;
    }

    /// <summary>
    /// 反序列消息包头。
    /// </summary>
    /// <param name="source">要反序列化的来源流。</param>
    /// <param name="customErrorData">用户自定义错误数据。</param>
    /// <returns></returns>
    public IPacketHeader DeserializePacketHeader (Stream source, out object customErrorData) {
        // 注意：此函数并不在主线程调用！
        customErrorData = null;
        return (IPacketHeader) RuntimeTypeModel.Default.Deserialize (
            source, ReferencePool.Acquire<Demo8_PacketHeader> (), typeof (Demo8_PacketHeader));
    }

    public void Initialize (INetworkChannel networkChannel) {
        Log.Debug ("Initialize");
        // 注册消息监听类
        networkChannel.RegisterHandler (new Demo8_PacketHandler ());
    }

    public bool SendHeartBeat () {
        Log.Debug ("SendHeartBeat");
        return true;
    }

    public byte[] Serialize<T> (T packet) where T : Packet {
        Log.Debug ("Serialize");

        return new byte[1];
    }

    public void Shutdown () {
        Log.Debug ("Shutdown");
    }
}