using System.Xml.Serialization;
using UltraPlayBettingSystemExercise.Data.Models;
using UltraPlayBettingSystemExercise.Services.Mapping;

namespace UltraPlayBettingSystemExercise.ViewModels
{
    [Serializable]
    [XmlRoot(ElementName = "Event")]
    public class EventViewModel : IMapTo<Event>, IMapFrom<Event>
    {
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "ID")]
        public int Id { get; set; }

        [XmlAttribute(AttributeName = "IsLive")]
        public bool IsLive { get; set; }

        [XmlAttribute(AttributeName = "CategoryID")]
        public int CategoryID { get; set; }

        [XmlElement(ElementName = "Match")]
        public List<MatchViewModel> Matches { get; set; }
    }
}
