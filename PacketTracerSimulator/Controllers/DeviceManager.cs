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
        public IList<Device> Devices { get; } = new List<Device>();
        public IList<List<string>> Network { get; set; } = new List<List<string>>();
        public Device TempDevice { get; set; }

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

            Devices.ToList().RemoveAll(x => x.Name == name);
            SelectedDevice = null;
            return true;
        }

        public void ListAllDevices()
        {
            Devices.ToList().ForEach(x =>{Console.WriteLine("Device Name: " + x.Name + " ||" + " Type: " + x.Type.GetDescription() +  " ||" + " IP: " + x.Ipv4.Ip);});
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

        public bool Ping(string to)
        {
            if (!SelectedDevice.Connections.Any()) return false;
            if (Devices.FirstOrDefault(x => x.Name == to) == null) return false;

            return Network.Select(x => x.Contains(to) && x.Contains(SelectedDevice.Name)).FirstOrDefault();
        }

        public bool ConnectTo(string to)
        {
            var exist = Devices.FirstOrDefault(x => x.Name == to);
            if (exist == null) return false;

            switch (SelectedDevice.Type)
            {
                case TypeOfDevice.Pc:
                    if (!SelectedDevice.HasAvailablePort()) return false;
                    if (exist.HasAvailablePort())
                    {
                        Devices.Where(x => x.Name == SelectedDevice.Name).ToList().ForEach(x => x.Connections.Add(to));
                        Devices.Where(x => x.Name == exist.Name).ToList().ForEach(x => x.Connections.Add(SelectedDevice.Name));
                    }
                    else { return false; }
                    break;
                case TypeOfDevice.Router:
                    if (!SelectedDevice.HasAvailablePort()) return false;
                    if (exist.HasAvailablePort())
                    {
                        Devices.Where(x => x.Name == SelectedDevice.Name).ToList().ForEach(x => x.Connections.Add(to));
                        Devices.Where(x => x.Name == exist.Name).ToList().ForEach(x => x.Connections.Add(SelectedDevice.Name));
                    }
                    else { return false; }
                    break;
                case TypeOfDevice.Switch:
                    if (!SelectedDevice.HasAvailablePort()) return false;
                    if (exist.HasAvailablePort())
                    {
                        Devices.Where(x => x.Name == SelectedDevice.Name).ToList().ForEach(x => x.Connections.Add(to));
                        Devices.Where(x => x.Name == exist.Name).ToList().ForEach(x => x.Connections.Add(SelectedDevice.Name));
                    }
                    else { return false; }
                    break;
                default:
                    return false;
            }

            if (Network.Count == 0)
            {
                Network.Add(new List<string>(){to, SelectedDevice.Name});
            }
            else
            {
                var cont = 0;
                Network.ToList().ForEach(x =>
                {
                    if (x.Contains(to))
                    {
                        x.Add(SelectedDevice.Name);
                        cont++;
                    }
                    else if (x.Contains(SelectedDevice.Name))
                    {
                        x.Add(to);
                        cont++;
                    }
                });
                if (cont == 0)
                {
                    Network.Add(new List<string>() { to, SelectedDevice.Name });
                }
            }
            return true;
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

    //public static partial class EnumerableExtensions
    //{
    //    public static bool GetConnections(this List<string> source)
    //    {
            
    //    }
    //}
}
