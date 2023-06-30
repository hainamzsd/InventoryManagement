using System.Text.Json;

namespace project.SessionExtensions
{
    public static class SessionExtensions
    {
        public static void SetSession<T>(this ISession session, string key, T value)
        {   
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetSession<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }

    }
}
