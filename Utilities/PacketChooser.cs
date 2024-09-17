using TritonTest.Models.Packets;

namespace TritonTest.Utilities;

public static class PacketChooser
{
    public static IPacket ChoosePacket(string header)
    {
        return header switch
        {
            "HI" => new HelloPacket(),
            "DT" => new DataPacket(),
            "VL" => new ValuePacket(),
            _ => throw new Exception("The header does not match any of the supported packet types.")
        };
    }
}