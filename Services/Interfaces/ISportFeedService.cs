using UltraPlayBettingSystemExercise.ViewModels;

namespace Services.Interfaces
{
    public interface ISportFeedService
    {
        Task<bool> SaveIfNewFeedAvailable(SportViewModel viewModel);
        Task<T> GetFeed<T>();
    }
}