using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace PacketTracerSimulator.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        ///     Get Description attribute from an specific enum element.
        /// </summary>
        /// <param name="enumSource">The enum to extract the description.</param>
        /// <returns>String with enum description, can return empty string.</returns>
        public static string GetDescription(this Enum enumSource)
        {
            return
                enumSource
                    .GetType()
                    .GetMember(enumSource.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description;
        }
    }
}
