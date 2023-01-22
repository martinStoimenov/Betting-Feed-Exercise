using UltraPlayBettingSystemExercise.Data.Common;

namespace Data.Models
{
    public class Message : BaseDeletableModel<int>
    {
        public string Content { get; set; }
        public string Type { get; set; }
    }
}
