using System.Collections.Generic;
using PacketTracerSimulator.Models;

namespace PacketTracerSimulator.Interfaces
{
    public interface IDeviceManager
    {
        /// <summary>
        /// A List of devices that contain all the devices that the user create. 
        /// </summary>
        List<Device> Devices { get; }
        /// <summary>
        /// The current working device. 
        /// </summary>
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
        bool EditName(string newName);
        bool EditIp(string newIp);
        bool EditSubnetMask(string newSM);
    }
}
