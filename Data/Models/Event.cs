using UltraPlayBettingSystemExercise.Data.Common;

namespace UltraPlayBettingSystemExercise.Data.Models
{
    public class Event : BaseDeletableModel<int>
    {
        public Event()
        {
            this.Matches = new HashSet<Match>();
        }
        public string Name { get; set; }
        public bool IsLive { get; set; }
        public int CategoryID { get; set; }
        public int SportId { get; set; }
        public Sport Sport { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}
