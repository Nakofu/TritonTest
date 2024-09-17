namespace TritonTest.Models.Packets;

public interface IPacket
{
    IPacket Create(byte[] operands);
    string ConvertToString();
}