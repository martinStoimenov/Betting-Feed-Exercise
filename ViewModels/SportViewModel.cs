using AutoMapper;
using System.Xml.Serialization;
using UltraPlayBettingSystemExercise.Data.Models;
using UltraPlayBettingSystemExercise.Services.Mapping;

namespace UltraPlayBettingSystemExercise.ViewModels
{
    [Serializable]
    [XmlRoot(ElementName = "Sport")]
    public class SportViewModel : IMapTo<Sport>, IMapFrom<Sport>
    {
        [XmlAttribute(AttributeName = "ID")]
        public int Id { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

        [XmlElement(ElementName = "Event")]
        public List<EventViewModel> Events { get; set; }
    }
}
