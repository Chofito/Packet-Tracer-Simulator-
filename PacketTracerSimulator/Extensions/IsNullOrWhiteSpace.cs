namespace PacketTracerSimulator.Extensions
{
    public static partial class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string source)
            => string.IsNullOrWhiteSpace(source);
    }
}
