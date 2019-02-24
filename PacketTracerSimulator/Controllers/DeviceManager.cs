using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks.Sources;
using PacketTracerSimulator.Enums;
using PacketTracerSimulator.Interfaces;
using PacketTracerSimulator.Models;

namespace PacketTracerSimulator.Controllers
{
    public class DeviceManager : IDeviceManager
    {
        private bool _encrypt = false;
        private Device SelectedDevice { get; set; }
        private List<Device> Devices { get; set; } = new List<Device>();


        public void AddDevice(Device item)
        {
            Devices.Add(item);
        }

        public void RemoveDevice(string name)
        {
            Devices.RemoveAll(x => x.Name == name);
        }

        public void ListAllDevices()
        {
            Devices.ForEach(x =>
            {
                Console.WriteLine("Device Name: " + x.Name + " IP: " + x.Ipv4);
            });
        }

        public void ListRouters()
        {
            Devices.Where(x => x.Type == TypeOfDevice.Router).ToList().ForEach(x =>
            {
                Console.WriteLine("Device Name: " + x.Name + " IP: " + x.Ipv4);
            });
        }

        public void ListSwitch()
        {
            Devices.Where(x => x.Type == TypeOfDevice.Switch).ToList().ForEach(x =>
            {
                Console.WriteLine("Device Name: " + x.Name + " IP: " + x.Ipv4);
            });
        }

        public void ListPersonalComputers()
        {
            Devices.Where(x => x.Type == TypeOfDevice.Pc).ToList().ForEach(x =>
            {
                Console.WriteLine("Device Name: " + x.Name + " IP: " + x.Ipv4);
            });
        }

        public void SelectDevice(string name)
        {
            SelectedDevice = Devices.Find(x => x.Name == name);
        }

        public void Ping(string to)
        {
            throw new System.NotImplementedException();
        }

        public void ConnectTo(string to)
        {
            throw new System.NotImplementedException();
        }

        public void EditDevice(Device data)
        {
            var device = Devices.Find(x => x.Name == data.Name);
            if (device != null) device = data;
        }

        public void SaveSession(string fileName)
        {
            throw new System.NotImplementedException();
        }

        public void ConfigureSave(bool crypt)
        {
            _encrypt = crypt;
        }
    }
}
