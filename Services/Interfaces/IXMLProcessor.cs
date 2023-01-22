using UltraPlayBettingSystemExercise.ViewModels;

namespace Services.Interfaces
{
    public interface IXMLProcessor
    {
        Task<SportViewModel> GetSportDataFeedAsync();
    }
}