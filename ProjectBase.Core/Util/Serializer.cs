using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ProjectBase.Core.Util
{
    /// <summary>
    /// 任意对象的XML序列化
    /// </summary>
    public class Serializer
    {
        /// <summary>
        /// 直接输出的属性（这里面包括 bool值）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static bool IsNumber(object obj)
        {
            if (obj is int
                || obj is long
                || obj is float
                || obj is double
                || obj is byte
                || obj is short
                || obj is decimal
                || obj is uint
                || obj is ulong
                || obj is ushort
                || obj is sbyte
                || obj is bool
                )
                return true;
            return false;
        }
        private static bool IsDateTime(object obj)
        {
            return obj is DateTime;
        }
        public static bool IsSimpleDataTypes(object obj)
        {
            return IsNumber(obj) || IsString(obj);
        }
        private static bool IsString(object obj)
        {
            return (obj is string
                || obj is char
                || obj is Guid
                || obj is DateTime
                || obj is Enum
                || obj is StringBuilder
                || obj is Uri
                );
        }
        public static string GetXML(object obj)
        {
            if (obj == null) return string.Empty;
            XmlSerializer formmafter = new XmlSerializer(obj.GetType());
            using (TextWriter writer = new StringWriter())
            {
                formmafter.Serialize(writer, obj);
                return writer.ToString();
            }
        }
    }
}
