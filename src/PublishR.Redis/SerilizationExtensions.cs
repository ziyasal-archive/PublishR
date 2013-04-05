using System.IO;
using ProtoBuf;

namespace PublishR.Redis
{
    public static class SerilizationExtensions
    {
        public static byte[] ToBArray<T>(this T o)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize(ms, o);
                return ms.ToArray();
            }
        }
        public static T ToTypedEntity<T>(this byte[] array)
        {
            using (MemoryStream ms = new MemoryStream(array))
            {
                return Serializer.Deserialize<T>(ms);
            }
        }
    }
}