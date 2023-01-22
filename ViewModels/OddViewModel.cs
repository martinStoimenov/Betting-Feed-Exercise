using System.Xml.Serialization;
using UltraPlayBettingSystemExercise.Data.Models;
using UltraPlayBettingSystemExercise.Services.Mapping;

namespace UltraPlayBettingSystemExercise.ViewModels
{
    [Serializable]
    [XmlRoot(ElementName = "Odd")]
    public class OddViewModel : IMapTo<Odd>, IMapFrom<Odd>
    {
        [XmlAttribute(AttributeName = "ID")]
        public int ID { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "Value")]
        public decimal Value { get; set; }

        [XmlAttribute(AttributeName = "SpecialBetValue")]
        public string SpecialBetValue { get; set; }
    }
}
