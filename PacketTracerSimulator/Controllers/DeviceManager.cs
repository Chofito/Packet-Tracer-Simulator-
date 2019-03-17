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
            SelectedDevice = null;
            return true;
        }

        public void ListAllDevices()
        {
            Devices.ForEach(x =>{Console.WriteLine("Device Name: " + x.Name + " ||" + " Type: " + x.Type.GetDescription() +  " ||" + " IP: " + x.Ipv4.Ip);});
        }

        public void ListRouters()
        {
            Devices.Where(x => x.Type == TypeOfDevice.Router).ToList().ForEach(x =>
            {
                Console.WriteLine("Device Name: " + x.Name + " IP: " + x.Ipv4.Ip);
            });
        }

        public void ListSwitch()
        {
            Devices.Where(x => x.Type == TypeOfDevice.Switch).ToList().ForEach(x =>
            {
                Console.WriteLine("Device Name: " + x.Name + " IP: " + x.Ipv4.Ip);
            });
        }

        public void ListPersonalComputers()
        {
            Devices.Where(x => x.Type == TypeOfDevice.Pc).ToList().ForEach(x =>
            {
                Console.WriteLine("Device Name: " + x.Name + " IP: " + x.Ipv4.Ip);
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

        public bool EditName(string newName)
        {
            var exist = Devices.FirstOrDefault(x => x.Name == newName);
            if (exist != null) return false;

            Devices.Where(x => x.Name == SelectedDevice.Name).ToList().ForEach(x => x.Name = newName);
            SelectedDevice.Name = newName;
            return true;
        }

        public bool EditIp(string newIp)
        {
            var exist = Devices.FirstOrDefault(x => x.Ipv4.Ip == newIp);
            if (exist != null) return false;

            Devices.Where(x => x.Name == SelectedDevice.Name).ToList().ForEach(x => x.Ipv4.Ip = newIp);
            SelectedDevice.Ipv4.Ip = newIp;
            return true;
        }

        public bool EditSubnetMask(string newSM)
        {
            Devices.Where(x => x.Name == SelectedDevice.Name).ToList().ForEach(x => x.Ipv4.SubnetMask = newSM);
            SelectedDevice.Ipv4.SubnetMask = newSM;
            return true;
        }
    }
}
