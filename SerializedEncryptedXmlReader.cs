using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace GTMY.IO
{
    public class SerializedEncryptedXmlReader : SerializedXmlReader
    {
        public SerializedEncryptedXmlReader(string path) : base(path)
        { }
        public SerializedEncryptedXmlReader(string path, string defaultPath) : base(path, defaultPath)
        { }

        protected override T ReadData<T>()
        {
            XmlDocument doc = Encryptor.LoadXmlDoc(path, true);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(new StringReader(doc.InnerXml));
        }

    }
}
