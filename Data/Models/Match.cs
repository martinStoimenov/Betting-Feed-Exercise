using UltraPlayBettingSystemExercise.Data.Common;

namespace UltraPlayBettingSystemExercise.Data.Models
{
    public class Match : BaseDeletableModel<int>
    {
        public Match()
        {
            this.Bets = new HashSet<Bet>();
        }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string MatchType { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
    }
}
