using AutoMapper;
using UltraPlayBettingSystemExercise.Data.Models;
using UltraPlayBettingSystemExercise.ViewModels;

namespace ViewModels
{
    public class UltraplayMapProfile : Profile
    {
        public UltraplayMapProfile()
        {
                CreateMap<SportViewModel, Sport>();
                CreateMap<EventViewModel, Event>();
                CreateMap<MatchViewModel, Match>();
                CreateMap<BetViewModel, Bet>();
                CreateMap<OddViewModel, Odd>();
                //CreateMap<Sport, SportViewModel>();
                //CreateMap<Event, EventViewModel>();
                //CreateMap<Match, MatchViewModel>();
                //CreateMap<Bet, BetViewModel>();
                //CreateMap<Odd, OddViewModel>();
        }
    }
}
