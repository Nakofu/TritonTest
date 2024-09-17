using System.Text;

namespace TritonTest.Models.Packets;

public class DataPacket : IPacket
{
    private Dictionary<uint, string> flagConverter = new()
    {
        { 1, "A-Flag" },
        { 2, "B-Flag" },
        { 4, "C-Flag" },
        { 8, "D-Flag" }
    };
    
    public uint Flags { get; private set; }
    public byte[] Data { get; private set; }
    
    public IPacket Create(byte[] operands)
    {
        var flagsBytes = operands[..4];
        var dataBytes = operands[4..];

        return new DataPacket
        {
            Flags = BitConverter.ToUInt32(flagsBytes, 0), 
            Data = dataBytes
        };
    }

    public string ConvertToString()
    {
        var strBuilder = new StringBuilder();

        strBuilder.Append("DATA packet:\n");
        strBuilder.Append($"Flags: { flagConverter[Flags] }\n");
        strBuilder.Append($"Data: { string.Join("; ", Data) }\n");

        return strBuilder.ToString();
    }
}