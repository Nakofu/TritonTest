using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TritonTest.Models;
using TritonTest.Utilities;

namespace TritonTest.Controllers;

[ApiController]
[Route("receiver")]
public class ReceiverController
{
    [HttpGet("connect-to-emulator")]
    public string ConnectToEmulator(int receivePort = 60002, int sendPort = 60001)
    {
        BroadcastClient.ChangePorts(receivePort, sendPort);
        var receivedPacket = BroadcastClient.ReceivePacket();
        var header = Encoding.UTF8.GetString(receivedPacket[0..2]);

        if (header != "HI")
            throw new Exception("The emulator has already been connected to");

        var packet = PacketChooser.ChoosePacket(header).Create(receivedPacket[2..]);
        var clientIp = IPAddress.Parse(LocalIpAddressFinder.GetLocalIpAddress());
        BroadcastClient.SendPacket(receivedPacket.Concat(clientIp.GetAddressBytes()).ToArray());
        
        return packet.ConvertToString();
    }
    
    [HttpGet("receive-packets")]
    public string ReceivePackets(int receivePort = 60002, int sendPort = 60001, int packetAmount = 2)
    {
        BroadcastClient.ChangePorts(receivePort, sendPort);
        var strBuilder = new StringBuilder();
        
        for (var i = 0; i < packetAmount; i++)
        {
            var receivedPacket = BroadcastClient.ReceivePacket();
            var header = Encoding.UTF8.GetString(receivedPacket[0..2]);
        
            if (header == "HI")
                throw new Exception("The emulator needs to be connected to before trying to receive packets");

            var packet = PacketChooser.ChoosePacket(header).Create(receivedPacket[2..]);

            strBuilder.Append(packet.ConvertToString());
            strBuilder.Append('\n');
        }
        
        return strBuilder.ToString();
    }
}