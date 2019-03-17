using System.Collections.Generic;
using PacketTracerSimulator.Models;

namespace PacketTracerSimulator.Interfaces
{
    public interface IDeviceManager
    {
        List<Device> Devices { get; }
        Device SelectedDevice { get; set; }
        /// <summary>
        /// Creates a new device and adds it to the list. 
        /// </summary>
        /// <param name="item"></param>
        bool AddDevice(Device item);
        bool RemoveDevice(string name);
        void ListAllDevices();
        void ListRouters();
        void ListSwitch();
        void ListPersonalComputers();
        bool SelectDevice(string name);
        void Ping(string to);
        bool ConnectTo(string to);
        bool EditDevice(Device data);
        bool SaveSession(string fileName);
    }
}
