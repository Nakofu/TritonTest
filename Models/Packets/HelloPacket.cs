using System.Text;

namespace TritonTest.Models.Packets;

public class HelloPacket : IPacket
{
    public int Code { get; private set; }
    
    public IPacket Create(byte[] operands)
    {
        return new HelloPacket
        {
            Code = BitConverter.ToInt32(operands, 0)
        };
    }

    public string ConvertToString()
    {
        var strBuilder = new StringBuilder();

        strBuilder.Append("HELLO packet:\n");
        strBuilder.Append($"Code: { Code }\n");

        return strBuilder.ToString();
    }
}