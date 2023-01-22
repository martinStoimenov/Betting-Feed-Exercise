using System.Xml;
using System.Xml.Serialization;

namespace UltraPlayBettingSystemExercise.ViewModels
{
    [Serializable, XmlRoot("XmlSports")]
    public class RootViewModel
    {
        [XmlElement("Sport")]
        public SportViewModel Sport { get; set; }
        [XmlAttribute(AttributeName = "CreateDate")]
        public DateTime CreateDate { get; set; }
    }
}
