using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Levi9.NetSel.Elements;

namespace Levi9.NetSel.Internal
{
    public static class TypeExtensionMethods
    {
        public static bool IsCompositeType(this Type type)
        {
            if (type.BaseType == null)
                return false;

            if (type.BaseType != typeof(CompositeElement) && !type.GenericTypeArguments.Any())
                return false;

            if (type.BaseType == typeof(CompositeElement))
                return true;

            return type.GetGenericArguments()[0].BaseType == typeof(CompositeElement);
        }
    }
}
