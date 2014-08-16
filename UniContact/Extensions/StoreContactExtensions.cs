using System.Collections.Generic;

namespace UniContact.Extensions
{
    public static class StoreContactExtensions
    {
        public static void AddIfNotNull(this IDictionary<string, object> contact, string key, object value)
        {
            if (value is string && !string.IsNullOrEmpty((string)value))
            {
                contact[key] = value;
            }
        }
    }
}
