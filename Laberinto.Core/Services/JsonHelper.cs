using System.Text.Json;

namespace Laberinto.Core.Services
{
    public static class JsonHelper
    {
        public static object ToObject(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    var dict = new Dictionary<string, object>();
                    foreach (var prop in element.EnumerateObject())
                        dict[prop.Name] = ToObject(prop.Value);
                    return dict;
                case JsonValueKind.Array:
                    var list = new List<object>();
                    foreach (var item in element.EnumerateArray())
                        list.Add(ToObject(item));
                    return list;
                case JsonValueKind.String:
                    return element.GetString();
                case JsonValueKind.Number:
                    if (element.TryGetInt64(out long l))
                        return l;
                    if (element.TryGetDouble(out double d))
                        return d;
                    return element.GetDecimal();
                case JsonValueKind.True:
                    return true;
                case JsonValueKind.False:
                    return false;
                case JsonValueKind.Null:
                    return null;
                default:
                    return null;
            }
        }

        public static Dictionary<string, object> ToObjectDictionary(Dictionary<string, JsonElement> dict)
        {
            var result = new Dictionary<string, object>();
            foreach (var kvp in dict)
                result[kvp.Key] = ToObject(kvp.Value);
            return result;
        }
    }
}