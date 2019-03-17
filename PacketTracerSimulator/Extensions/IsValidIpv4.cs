using System.Text.RegularExpressions;

namespace PacketTracerSimulator.Extensions
{
    public static partial class StringExtensions
    {
        public static bool IsValidIpv4(this string str)
            => Regex.IsMatch(str,
                @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
    }
}
