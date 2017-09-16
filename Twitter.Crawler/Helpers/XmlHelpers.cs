using System.Collections.Generic;
using System.Xml.Serialization;
using LinqToTwitter;

namespace Twitter.Crawler.Helpers
{
    public static class XmlHelpers
    {
        /// <summary>
        /// Saves to an xml file
        /// </summary>
        /// <param name="fileName">File path of the new xml file</param>
        public static void SaveToFile<T>(this T Value, string fileName)
        {
            using (var writer = new System.IO.StreamWriter(fileName))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, Value);
                writer.Flush();
            }
        }
        /// <summary>
        /// Saves to an xml file
        /// </summary>
        /// <param name="fileName">File path of the new xml file</param>
        public static void SaveToFile<T>(this List<T> Value, string fileName)
        {
            using (var writer = new System.IO.StreamWriter(fileName))
            {
                var serializer = new XmlSerializer(typeof(T));
                foreach (var value in Value)
                {
                    serializer.Serialize(writer, Value);
                }
                
                writer.Flush();
            }
        }
    }
}