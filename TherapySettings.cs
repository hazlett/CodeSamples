using System.Xml.Serialization;

namespace GTMY.Xml.Serializable
{
    public class TherapySettings
    {
        [XmlElement("Trunk")]
        public BoolValueAttribute Trunk = new BoolValueAttribute(false);
        [XmlElement("Overhead")]
        public BoolValueAttribute Overhead = new BoolValueAttribute(false);
        [XmlElement("Wrist")]
        public BoolValueAttribute Wrist = new BoolValueAttribute(false);
        [XmlElement("Hand")]
        public BoolValueAttribute Hand = new BoolValueAttribute(false);
    }
}
