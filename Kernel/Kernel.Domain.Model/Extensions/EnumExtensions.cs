using Kernel.Domain.Model.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Kernel.Domain.Model.Extensions
{
    public static class EnumExtensions
    {
        public static List<EnumItem> ListEnum(this Enum source) => source.GetType().ListEnum();

        public static List<EnumItem> ListEnum(this Type source)
        {
            var data = Enum
                .GetNames(source)
                .Select(name => new EnumItem
                {
                    Value = (int)Enum.Parse(source, name),
                    Text = name,
                    Description = source.GetDescription(name)
                }).ToList();

            return data;
        }

        private static string GetDescription(this Type param, string source)
        {
            try
            {
                var displayAttribute = ((DisplayAttribute[])param.GetField(source)
                                            .GetCustomAttributes(typeof(DisplayAttribute), true))
                                            .FirstOrDefault();
                return displayAttribute != null ? displayAttribute.Description : source.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string GetDescription(this Enum source)
        {
            if (source == null) return string.Empty;

            var displayAttribute = source.GetType()
                .GetField(source.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

            return displayAttribute != null ? displayAttribute.Description : source.ToString();
        }

        public static string ToHexaString<TEnum>(this TEnum value, bool withHashPrefix = true) where TEnum : Enum
        {
            int int32 = Convert.ToInt32(value);
            return $"{(withHashPrefix ? "#" : "")}{Convert.ToString(int32, 16).PadLeft(6, '0')}";
        }
    }
}