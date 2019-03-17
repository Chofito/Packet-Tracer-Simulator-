using System.Collections.Generic;
using PacketTracerSimulator.Enums;
using PacketTracerSimulator.Models.Common;

namespace PacketTracerSimulator.Models
{
    public class Device
    {
        public string Name { get; set; }
        public TypeOfDevice Type { get; set; }
        public Ipv4 Ipv4 { get; set; }
        public string MacAddress { get; set; }
        public List<string> Ports { get; set; }
    }
}
