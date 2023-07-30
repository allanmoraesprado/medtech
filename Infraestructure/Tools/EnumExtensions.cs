using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Infraestructure.Tools
{
    public static class EnumExtensions
    {
        public static string ToDescription(this Enum value)
        {
            try
            {
                var attributes = (DescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
                string descricao = attributes.Length > 0 ? attributes[0].Description : value.ToString();
                return descricao;
            }
            catch
            {
                return "";
            }
        }
    }
}
