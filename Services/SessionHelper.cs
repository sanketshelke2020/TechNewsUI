using Newtonsoft.Json;

namespace TechNewsUI.Services
{
    public static class SessionHelper
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.Set(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key)
;
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
