using System.Net;
using System.Net.Sockets;

namespace TritonTest.Models;

public static class BroadcastClient
{
    private static IPEndPoint broadcastAddress;
    private static UdpClient udpClient;

    private static int sendPort = 60001;

    static BroadcastClient()
    {
        broadcastAddress = new IPEndPoint(IPAddress.Any, 60002);
        udpClient = new UdpClient();
        udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        udpClient.Client.Bind(broadcastAddress);
    }

    public static byte[] ReceivePacket()
    {
        return udpClient.Receive(ref broadcastAddress);
    }

    public static void SendPacket(byte[] sendData)
    {
        udpClient.Send(sendData, sendData.Length, "255.255.255.255", sendPort);
    }

    public static void ChangePorts(int receivePort, int sendPort)
    {
        if (receivePort == broadcastAddress.Port && sendPort == BroadcastClient.sendPort)
            return;
        
        BroadcastClient.sendPort = sendPort;
        broadcastAddress = new IPEndPoint(IPAddress.Any, receivePort);
    }
}