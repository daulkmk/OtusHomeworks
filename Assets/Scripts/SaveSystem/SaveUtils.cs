using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;

namespace SaveLoad
{
    public static class SaveUtils
    {
        public static byte[] ObjectToByteArray(byte[] obj) => obj;
        public static byte[] ObjectToByteArray<T>(T obj)
        {
            if (IsByteArray<T>())
                return (byte[])Convert.ChangeType(obj, typeof(byte[]));

            string json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            var bf = new BinaryFormatter();
            using var ms = new MemoryStream();
        
            bf.Serialize(ms, json);
            return ms.ToArray();
        }

        public static byte[] ByteArrayToObject(byte[] obj) => obj;
        public static T ByteArrayToObject<T>(byte[] arrBytes)
        {
            if (IsByteArray<T>())
                return (T)Convert.ChangeType(arrBytes, typeof(T));

            var binForm = new BinaryFormatter();
            using var memStream = new MemoryStream();

            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);

            var json = (string)binForm.Deserialize(memStream);
            return JsonConvert.DeserializeObject<T>(json);
        }

        private static bool IsByteArray<T>() 
        {
            var type = typeof(T);
            return type.IsArray && type.GetElementType() == typeof(byte);
        }
    }
}