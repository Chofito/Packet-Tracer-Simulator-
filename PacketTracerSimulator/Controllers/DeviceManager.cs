using System;
using System.Collections.Generic;
using System.Linq;
using PacketTracerSimulator.Enums;
using PacketTracerSimulator.Extensions;
using PacketTracerSimulator.Interfaces;
using PacketTracerSimulator.Models;

namespace PacketTracerSimulator.Controllers
{
    public class DeviceManager : IDeviceManager
    {
        public Device SelectedDevice { get; set; }
        public List<Device> Devices { get; } = new List<Device>();

        public bool AddDevice(Device item)
        {
            var temp = Devices.FirstOrDefault(x => x.Name == item.Name);
            if (temp != null) return false;

            Devices.Add(item);
            return true;
        }

        public bool RemoveDevice(string name)
        {
            var temp = Devices.FirstOrDefault(x => x.Name == name);
            if (temp == null) return false;

            Devices.RemoveAll(x => x.Name == name);
            return true;
        }

        public void ListAllDevices()
        {
            Devices.ForEach(x =>{Console.WriteLine("Device Name: " + x.Name + " ||" + " Type: " + x.Type.GetDescription() +  " ||" + " IP: " + x.Ipv4);});
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

        public bool SelectDevice(string name)
        {
            var temp = Devices.FirstOrDefault(x => x.Name == name);
            if (temp == null) return false;


            SelectedDevice = temp;
            return true;
        }

        public void Ping(string to)
        {
            throw new System.NotImplementedException();
        }

        public bool ConnectTo(string to)
        {
            throw new System.NotImplementedException();
        }

        public bool EditDevice(Device data)
        {
            var temp = Devices.FirstOrDefault(x => x.Name == data.Name || x.Ipv4.Ip == data.Ipv4.Ip);
            if (temp != null) return false;

            var index = Devices.FindIndex(x => x.Name == SelectedDevice.Name);
            Devices[index] = data;
            return true;
        }

        public bool SaveSession(string fileName)
        {
            throw new System.NotImplementedException();
        }
    }
}
