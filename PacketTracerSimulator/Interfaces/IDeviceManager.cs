using PacketTracerSimulator.Models;

namespace PacketTracerSimulator.Interfaces
{
    public interface IDeviceManager
    {
        /// <summary>
        /// Creates a new device and adds it to the list. 
        /// </summary>
        /// <param name="item"></param>
        void AddDevice(Device item);
        void RemoveDevice(string name);
        void ListAllDevices();
        void ListRouters();
        void ListSwitch();
        void ListPersonalComputers();
        void SelectDevice(string name);
        void Ping(string to);
        void ConnectTo(string to);
        void EditDevice(Device data);
        void SaveSession(string fileName);
        void ConfigureSave(bool crypt);
    }
}
