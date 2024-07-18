using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Extensions
{
    public static class EnumExtension
    {
        public static string GetModuleNameWithType<TEnum>(this TEnum enumValue, string moduleName) where TEnum : Enum
        {
            if (enumValue.Equals(default(TEnum)))
            {
                return moduleName;
            }
            return $"{moduleName}\\{enumValue}";
        }
    }
}
