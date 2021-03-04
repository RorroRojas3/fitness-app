using System.ComponentModel.DataAnnotations;
using System;

namespace Rodrigo.Tech.Model.Attribute.V1
{
    public class RequiredEnumAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            var type = value.GetType();
            return type.IsEnum && Enum.IsDefined(type, value);
        }
    }
}
