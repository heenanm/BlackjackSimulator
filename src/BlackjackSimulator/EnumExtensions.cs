using System;
using System.ComponentModel;

namespace BlackjackSimulator
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            var field = type.GetField(name);

            if (name == null)
            {
                return null;
            }

            if (field == null)
            {
                return null;
            }

            return Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attr ? attr.Description : null;
        }
    }
}