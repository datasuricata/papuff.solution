using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace papuff.domain.Helpers {
    public static class Helpers {
        public static string EnumDisplay(this Enum value) {
            DisplayAttribute attribute = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .SingleOrDefault() as DisplayAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
