using System.Xml.Serialization;
using UltraPlayBettingSystemExercise.Data.Models;
using UltraPlayBettingSystemExercise.Services.Mapping;

namespace UltraPlayBettingSystemExercise.ViewModels
{
    [Serializable]
    [XmlRoot(ElementName = "Match")]
    public class MatchViewModel : IMapTo<Match>, IMapFrom<Match>
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "ID")]
        public int ID { get; set; }

        [XmlAttribute(AttributeName = "StartDate")]
        public DateTime StartDate { get; set; }

        [XmlAttribute(AttributeName = "MatchType")]
        public string MatchType { get; set; }

        [XmlElement(ElementName = "Bet")]
        public List<BetViewModel> Bets { get; set; }
    }
}
