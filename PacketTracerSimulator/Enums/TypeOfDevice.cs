using System.ComponentModel;

namespace PacketTracerSimulator.Enums
{
    public enum TypeOfDevice
    {
        [Description("Router")]
        Router,
        [Description("Switch")]
        Switch,
        [Description("PC")]
        Pc
    }
}
