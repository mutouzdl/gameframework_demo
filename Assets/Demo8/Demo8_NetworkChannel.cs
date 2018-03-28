using System.Net;
using GameFramework.Network;

public class Demo8_NetworkChannel : INetworkChannel {
    public string Name {
        get {
            throw new System.NotImplementedException ();
        }
    }

    public bool Connected {
        get {
            throw new System.NotImplementedException ();
        }
    }

    public NetworkType NetworkType {
        get {
            throw new System.NotImplementedException ();
        }
    }
    public IPAddress LocalIPAddress {
        get {
            throw new System.NotImplementedException ();
        }
    }

    public int LocalPort {
        get {
            throw new System.NotImplementedException ();
        }
    }

    public IPAddress RemoteIPAddress {
        get {
            throw new System.NotImplementedException ();
        }
    }

    public int RemotePort {
        get {
            throw new System.NotImplementedException ();
        }
    }

    public bool ResetHeartBeatElapseSecondsWhenReceivePacket {
        get {
            throw new System.NotImplementedException ();
        }
        set {
            throw new System.NotImplementedException ();
        }
    }
    public float HeartBeatInterval {
        get {
            throw new System.NotImplementedException ();
        }
        set {
            throw new System.NotImplementedException ();
        }
    }
    public int ReceiveBufferSize {
        get {
            throw new System.NotImplementedException ();
        }
        set {
            throw new System.NotImplementedException ();
        }
    }
    public int SendBufferSize {
        get {
            throw new System.NotImplementedException ();
        }
        set {
            throw new System.NotImplementedException ();
        }
    }

    public void Close () {
        throw new System.NotImplementedException ();
    }

    public void Connect (IPAddress ipAddress, int port) {
        throw new System.NotImplementedException ();
    }

    public void Connect (IPAddress ipAddress, int port, object userData) {
        throw new System.NotImplementedException ();
    }

    public void RegisterHandler (IPacketHandler handler) {
        throw new System.NotImplementedException ();
    }

    public void Send (byte[] buffer) {
        throw new System.NotImplementedException ();
    }

    public void Send (byte[] buffer, object userData) {
        throw new System.NotImplementedException ();
    }

    public void Send (byte[] buffer, int offset, int size) {
        throw new System.NotImplementedException ();
    }

    public void Send (byte[] buffer, int offset, int size, object userData) {
        throw new System.NotImplementedException ();
    }

    public void Send<T> (T packet) where T : Packet {
        throw new System.NotImplementedException ();
    }

    public void Send<T> (T packet, object userData) where T : Packet {
        throw new System.NotImplementedException ();
    }
}