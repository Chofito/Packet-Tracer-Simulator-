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
        public List<string> Connections { get; set; }

        public bool HasAvailablePort()
        {
            switch (Type)
            {
                case TypeOfDevice.Pc:
                    return Connections.Count < 1;
                case TypeOfDevice.Router:
                    return Connections.Count < 2;
                case TypeOfDevice.Switch:
                    return Connections.Count < 4;
                default:
                    return false;
            }
        }
    }
}
