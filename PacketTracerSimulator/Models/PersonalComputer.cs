using System.Collections;
using PacketTracerSimulator.Enums;

namespace PacketTracerSimulator.Models
{
    public class PersonalComputer : Device
    {
        public PersonalComputer()
        {
            Type = TypeOfDevice.Pc;
        }
    }
}
