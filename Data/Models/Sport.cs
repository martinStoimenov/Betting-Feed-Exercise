using UltraPlayBettingSystemExercise.Data.Common;

namespace UltraPlayBettingSystemExercise.Data.Models
{
    public class Sport : BaseDeletableModel<int>
    {
        public Sport()
        {
            this.Events = new HashSet<Event>();
        }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual IEnumerable<Event> Events { get; set; }
    }
}
