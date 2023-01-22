using System.Xml.Serialization;
using UltraPlayBettingSystemExercise.Data.Models;
using UltraPlayBettingSystemExercise.Services.Mapping;

namespace UltraPlayBettingSystemExercise.ViewModels
{
    [Serializable]
    [XmlRoot(ElementName = "Bet")]
    public class BetViewModel : IMapTo<Bet>, IMapFrom<Bet>
    {
        [XmlElement(ElementName = "Odd")]
        public List<OddViewModel> Odds { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "ID")]
        public int ID { get; set; }

        [XmlAttribute(AttributeName = "IsLive")]
        public bool IsLive { get; set; }
    }
}
