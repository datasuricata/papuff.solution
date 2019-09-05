using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace papuff.domain.Helpers {
    public static class Helper {

        public static string EnumDisplay(this Enum value) {
            return !(value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false)
                .SingleOrDefault() is DisplayAttribute attribute) ? value.ToString() : attribute.Description;
        }
    }
}
