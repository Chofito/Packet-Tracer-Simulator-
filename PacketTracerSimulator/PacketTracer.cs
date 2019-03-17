using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using static Colorful.Console;
using Newtonsoft.Json;
using PacketTracerSimulator.Controllers;
using PacketTracerSimulator.Enums;
using PacketTracerSimulator.Extensions;
using PacketTracerSimulator.Interfaces;
using PacketTracerSimulator.Models;
using PacketTracerSimulator.Models.Common;
using Pastel;

namespace PacketTracerSimulator
{
    public class PacketTracer : IPacketTracer
    {
        private IDeviceManager _deviceManager = new DeviceManager();
        private string _entrada = "";
        private Device _temporalDevice;
        private static readonly Regex QuitarEspaciosExtras = new Regex(@"\s\s+");
        private static readonly Random Random = new Random();

        public void Start()
        {
            WriteAscii("PACKET TRACER :v", Color.PaleGreen);
            Console.WriteLine(
                "Open Source example project, made by Darksun, Cristian and Joshua.\n".PastelWithGradient("#f44242",
                    "#4141f4"));
            while (true)
            {
                Console.Write("> ".Pastel(Color.OrangeRed));
                _entrada = Console.ReadLine();
                if (!_entrada.IsNullOrWhiteSpace())
                {
                    _entrada = QuitarEspaciosExtras.Replace(_entrada, " ");
                    Analizador(_entrada.ToLower().Split(" "));
                }
            }
        }

        public void Analizador(string[] comandos)
        {
            switch (comandos[0])
            {
                case "create":
                    if (comandos.Length == 3)
                    {
                        switch (comandos[1])
                        {
                            case "router":
                                _temporalDevice = new Router();
                                break;
                            case "switch":
                                _temporalDevice = new Switch();
                                break;
                            case "pc":
                                _temporalDevice = new PersonalComputer();
                                break;
                            default:
                                Console.WriteLine("Dispositivo desconocido: " + comandos[1]);
                                break;
                        }

                        _temporalDevice.Name = comandos[2];
                        _temporalDevice.MacAddress = RandomString(48);
                        _temporalDevice.Ipv4 = new Ipv4(){Ip = "", SubnetMask = ""};
                        _deviceManager.AddDevice(_temporalDevice);
                    }
                    else
                    {
                        Console.WriteLine("Bad command sequence, please see " +
                                          "help".PastelUnderLine(Color.DodgerBlue) + " or " +
                                          "--h".PastelUnderLine(Color.Purple));
                    }

                    break;
                case "delete":
                    if (comandos.Length == 2)
                    {
                        _deviceManager.RemoveDevice(comandos[1]);
                    }

                    break;
                case "edit":
                    if (comandos.Length == 3)
                    {
                        if (_deviceManager.SelectedDevice != null)
                        {
                            switch (comandos[1])
                            {
                                case "name":
                                    var resultName = _deviceManager.EditName(comandos[2]);
                                    Console.WriteLine(resultName ? "Name updated correctly".Pastel(Color.GreenYellow) : "Error, the name already exist".Pastel(Color.Red));
                                    break;
                                case "ip":
                                    if (!comandos[2].IsValidIpv4()) Console.WriteLine("Invalid IP".Pastel(Color.Red));
                                    else
                                    {
                                        var resultIp = _deviceManager.EditIp(comandos[2]);
                                        Console.WriteLine(resultIp ? "IP updated correctly".Pastel(Color.GreenYellow) : "Error, the IP already exist".Pastel(Color.Red));
                                    }
                                    break;
                                case "subnetmask":
                                    if (!int.TryParse(comandos[2], out var mask))
                                    {
                                        Console.WriteLine("The subnet mask format is a number in 1..24");
                                        break;
                                    }
                                    if (mask > 0 && mask <= 24)
                                    {
                                        _deviceManager.EditSubnetMask(comandos[2]);
                                        Console.WriteLine("SubnetMask updated correctly".Pastel(Color.GreenYellow));
                                    }
                                    break;
                                default:
                                    WriteLine("Unknown command: " + comandos[2].PastelUnderLine(Color.Red));
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Bad command sequence, please see " +
                                          "help".PastelUnderLine(Color.DodgerBlue) + " or " +
                                          "--h".PastelUnderLine(Color.Purple));
                    }

                    break;
                case "select":
                    if (comandos.Length == 2)
                    {
                        if (!comandos[1].IsNullOrWhiteSpace()) _deviceManager.SelectDevice(comandos[1]);
                    }

                    break;
                case "ping":
                    break;
                case "save":
                    if (comandos.Length == 2)
                    {
                        if (_deviceManager.Devices.Count > 0)
                        {
                            var onlyPc = _deviceManager.Devices.Where(x => x.Type == TypeOfDevice.Pc);
                            var onlyRouters = _deviceManager.Devices.Where(x => x.Type == TypeOfDevice.Router);
                            var onlySwitches = _deviceManager.Devices.Where(x => x.Type == TypeOfDevice.Switch);
                            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\saves\\" + comandos[1] + ".json",
                                JsonConvert.SerializeObject(new {onlyPc, onlySwitches, onlyRouters}));
                        }
                        else
                        {
                            Console.WriteLine("The current sesion is " + "empty".PastelUnderLine(Color.DodgerBlue));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Bad command sequence, please see " +
                                          "help".PastelUnderLine(Color.DodgerBlue) + " or " +
                                          "--h".PastelUnderLine(Color.Purple));
                    }

                    break;
                case "open":
                    if (comandos.Length == 2)
                    {
                        if (!_deviceManager.Devices.Any())
                        {
                            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\saves\\" + comandos[1] + ".json"))
                            {
                                var file = File.ReadAllText(
                                    AppDomain.CurrentDomain.BaseDirectory + "\\saves\\" + comandos[1] + ".json");
                                var tempType = new
                                {
                                    OnlyPc = new List<PersonalComputer>(), OnlyRouters = new List<Router>(),
                                    OnlySwitches = new List<PersonalComputer>()
                                };
                                var devices = JsonConvert.DeserializeAnonymousType(file, tempType);
                                devices.OnlySwitches.ForEach(x => _deviceManager.Devices.Add(x));
                                devices.OnlyRouters.ForEach(x => _deviceManager.Devices.Add(x));
                                devices.OnlyPc.ForEach(x => _deviceManager.Devices.Add(x));
                            }
                            else
                            {
                                Console.WriteLine("The file " +
                                                  (comandos[1] + ".json").PastelUnderLine(Color.YellowGreen) +
                                                  " doesn't exists.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please, "
                                              + "save".PastelUnderLine(Color.DodgerBlue)
                                              + " your current session and "
                                              + "clear".PastelUnderLine(Color.Purple)
                                              + " it");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Bad command sequence, please see " +
                                          "help".PastelUnderLine(Color.DodgerBlue) + " or " +
                                          "--h".PastelUnderLine(Color.Purple));
                    }

                    break;
                case "clear":
                    _deviceManager.Devices.Clear();
                    _deviceManager.SelectedDevice = null;
                    break;
                case "list-saves":
                    Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\saves\\", "*.json")
                        .ToList().ForEach(x => Console.WriteLine(Path.GetFileNameWithoutExtension(x)));
                    break;
                case "help":
                    break;
                case "--h":
                    break;
                case "list":
                    switch (comandos[1])
                    {
                        case "all":
                            _deviceManager.ListAllDevices();
                            break;
                        case "router":
                            _deviceManager.ListRouters();
                            break;
                        case "switch":
                            _deviceManager.ListSwitch();
                            break;
                        case "pc":
                            _deviceManager.ListPersonalComputers();
                            break;
                        default:
                            Console.WriteLine("Unknown command: " + comandos[1].PastelInverse(Color.Red));
                            break;
                    }
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Unknown command: " + comandos[0].PastelInverse(Color.Red));
                    break;
            }
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEF0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}