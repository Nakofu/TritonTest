using System.Text;

namespace TritonTest.Models.Packets;

public class ValuePacket : IPacket
{
    public uint Number { get; private set; }
    public int Value { get; private set; }

    public IPacket Create(byte[] operands)
    {
        var numberBytes = operands[..4];
        var valueBytes = operands[4..];

        return new ValuePacket
        {
            Number = BitConverter.ToUInt32(numberBytes, 0), 
            Value = BitConverter.ToInt32(valueBytes, 0)
        };
    }

    public string ConvertToString()
    {
        var strBuilder = new StringBuilder();

        strBuilder.Append("VALUE packet:\n");
        strBuilder.Append($"Number: {Number}\n");
        strBuilder.Append($"Value: {Value}\n");

        return strBuilder.ToString();
    }
}