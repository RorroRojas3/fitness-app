using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace fitness_web_api.Helpers
{
    public static class SerializeHelper
    {
        /// <summary>
        ///     Serializes any object to a byte array
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] SerializeObject(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        ///     Deserialize byte array to T object
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(byte[] data)
        {
            if (data == null)
            {
                return default(T);
            }

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
    }
}