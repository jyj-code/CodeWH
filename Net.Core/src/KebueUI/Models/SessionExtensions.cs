using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KebueUI.Models
{
    public static class SessionExtensions
    {
        public static T Get<T>(this Microsoft.AspNetCore.Http.ISession session, string key) where T : class
        {
            byte[] byteArray = null;
            if (session.TryGetValue(key, out byteArray))
            {
                using (var memoryStream = new System.IO.MemoryStream(byteArray))
                {
                    var obj = ProtoBuf.Serializer.Deserialize<T>(memoryStream);
                    return obj;
                }
            }
            return null;
        }

        public static void Set<T>(this Microsoft.AspNetCore.Http.ISession session, string key, T value) where T : class
        {
            try
            {
                using (var memoryStream = new System.IO.MemoryStream())
                {
                    ProtoBuf.Serializer.Serialize(memoryStream, value);
                    byte[] byteArray = memoryStream.ToArray();
                    session.Set(key, byteArray);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
