using UltraPlayBettingSystemExercise.ViewModels;

namespace UltraPlayBettingSystemExercise.Services.Interfaces
{
    public interface IMatchesService
    {
        Task<IEnumerable<MatchViewModel>> GetAllMatchesInLast24Hours<MatchViewModel>();
        Task<T> GetSingleMatchByID<T>(int Id);
    }
}