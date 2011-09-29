using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeCamp.DataServerEF4
{
    public static class ExtensionMethods
    {
        public static bool IsTransient(this Type aType)
        {
            if (aType.IsAbstract)
                return true;
            object[] _Attributes = aType.GetCustomAttributes(true);
            foreach (Attribute _Attribute in _Attributes)
            {
                if (_Attribute.GetType().Name.ToLower().IndexOf("transient") != -1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
