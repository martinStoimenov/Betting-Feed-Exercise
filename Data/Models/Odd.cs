using UltraPlayBettingSystemExercise.Data.Common;

namespace UltraPlayBettingSystemExercise.Data.Models
{
    public class Odd : BaseDeletableModel<int>
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int BetId { get; set; }
        public Bet Bet { get; set; }
        public string? SpecialBetValue { get; set; }
    }
}
