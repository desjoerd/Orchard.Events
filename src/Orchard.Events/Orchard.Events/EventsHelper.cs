using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchard.Events
{
    internal static class EventsHelper
    {
        internal static string GetInterfaceKey(Type eventHandlerInterfaceType)
        {
            var keyBuilder = new StringBuilder(eventHandlerInterfaceType.Name);
            for (int i = 0; i < eventHandlerInterfaceType.GenericTypeArguments.Length; i++)
            {
                keyBuilder.Append("[");
                keyBuilder.Append(eventHandlerInterfaceType.GenericTypeArguments[i].Name);
                keyBuilder.Append("]");
            }
            return keyBuilder.ToString();
        }

        internal static bool IsGenericInterfaceKey(string interfaceKey)
        {
            return interfaceKey[interfaceKey.Length - 1] == ']';
        }

        internal static string[] GetGenericParamNames(string interfaceKey)
        {
            var paramNames = interfaceKey.Split('[');
            var result = new string[paramNames.Length -1];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = paramNames[i + 1].Substring(0, paramNames[i + 1].Length - 1);
            }
            return result;
        }

        internal static string GetInterfaceNameFromKey(string interfaceKey)
        {
            if (IsGenericInterfaceKey(interfaceKey))
            {
                return interfaceKey.Substring(0, interfaceKey.IndexOf('['));
            }
            return interfaceKey;
        }
    }
}
