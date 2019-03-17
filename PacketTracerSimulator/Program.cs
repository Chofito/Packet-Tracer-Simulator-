using PacketTracerSimulator.Interfaces;

namespace PacketTracerSimulator
{
    public class Program
    {
        private static readonly IPacketTracer PacketTracer = new PacketTracer();

        public static void Main(string[] args)
        {
            PacketTracer.Start();
        }
    }
}
