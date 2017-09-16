using System.IO;
using System.Xml.Serialization;
using Twitter.Crawler.Interfaces;

namespace Twitter.Crawler.Access
{
    public class AccessCredentials:IAccessCredentials
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenSecret { get; set; }

        public AccessCredentials()
        {
        }

        public AccessCredentials(string consumerKey, string consumerSecret, string accessToken, string accessTokenSecret)
        {
            ConsumerKey = consumerKey;
            ConsumerSecret = consumerSecret;
            AccessToken = accessToken;
            AccessTokenSecret = accessTokenSecret;
        }

        public AccessCredentials(string fileName)
        {
            var credentials = Load(fileName);
            this.AccessTokenSecret = credentials.AccessTokenSecret;
            this.AccessToken = credentials.AccessToken;
            this.ConsumerKey = credentials.ConsumerKey;
            this.ConsumerSecret = credentials.ConsumerSecret;
        }

        /// <summary>
        /// Saves to an xml file
        /// </summary>
        /// <param name="fileName">File path of the new xml file</param>
        public void Save(string fileName)
        {
            using (var writer = new System.IO.StreamWriter(fileName))
            {
                var serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(writer, this);
                writer.Flush();
            }
        }

        /// <summary>
        /// Load an object from an xml file
        /// </summary>
        /// <param name="fileName">Xml file name</param>
        /// <returns>The object created from the xml file</returns>
        public IAccessCredentials Load(string fileName)
        {
            using (var stream = System.IO.File.OpenRead(fileName))
            {
                var serializer = new XmlSerializer(typeof(AccessCredentials));
                return serializer.Deserialize(stream) as AccessCredentials;
            }
        }
    }
}