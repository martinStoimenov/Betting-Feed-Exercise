using UltraPlayBettingSystemExercise.Data.Common;

namespace UltraPlayBettingSystemExercise.Data.Models
{
    public class Bet : BaseDeletableModel<int>
    {
        public Bet()
        {
            this.Odds = new HashSet<Odd>();
        }
        public string Name { get; set; }
        public bool IsLive { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public virtual IEnumerable<Odd> Odds { get; set; }
    }
}
