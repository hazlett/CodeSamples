using System.Xml.Serialization;
using GTMY.IO;
using GTMY.Xml;
using GTMY.Xml.Serializable;

namespace Example
{
	public class Example
	{
		public Example()
		{
			IReader reader = new SerializedXmlReader("path.xml");
			TherapySettings settings = reader.Read<TherapySettings>();	
			IWriter writer = new SerializedEncryptedXmlWriter("encryptedPath.xml");
			writer.Write(settings);	
		}
	}
}
