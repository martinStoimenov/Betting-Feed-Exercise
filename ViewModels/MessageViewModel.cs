using Data.Models;
using UltraPlayBettingSystemExercise.Services.Mapping;

namespace ViewModels
{
    public class MessageViewModel : IMapFrom<Message>
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
    }
}
