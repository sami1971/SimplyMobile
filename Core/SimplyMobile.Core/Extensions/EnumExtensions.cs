using System;
using System.Reflection;
using System.ComponentModel;

namespace SimplyMobile
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {            
            FieldInfo field = value.GetType().GetField(value.ToString());

            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}

