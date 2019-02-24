using PacketTracerSimulator.Enums;
using PacketTracerSimulator.Models.Common;

namespace PacketTracerSimulator.Models
{
    public abstract class Device
    {
        public string Name { get; set; }
        public TypeOfDevice Type { get; set; }
        public Ipv4 Ipv4 { get; set; }
        public Ipv6 Ipv6 { get; set; }
        public string MacAddress { get; set; }
    }
}
