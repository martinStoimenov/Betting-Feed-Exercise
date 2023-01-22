using UltraPlayBettingSystemExercise.ViewModels;
using UltraPlayBettingSystemExercise.Services.Interfaces;
using Services.Interfaces;
using UltraPlayBettingSystemExercise.Data.Repositoies;
using UltraPlayBettingSystemExercise.Data.Models;
using UltraPlayBettingSystemExercise.Services.Mapping;
using Microsoft.EntityFrameworkCore;

namespace UltraPlayBettingSystemExercise.Services
{
    public partial class MatchesService : IMatchesService
    {
        private readonly string[] previewMarkets = new string[] { "Match Winner", "Map Advantage", "Total Maps Played" };
        private readonly DateTime targetDateTime = DateTime.UtcNow.AddHours(24);
        private readonly IXMLProcessor xmlProcessor;
        private readonly ISportFeedService sportFeedService;
        private readonly IRepository<Match> matchRepository;
        private readonly IRepository<Odd> oddRepository;
        private readonly IRepository<Bet> betRepository;

        public MatchesService(IXMLProcessor xmlProcessor, ISportFeedService sportFeedService, IRepository<Match> matchRepository, IRepository<Odd> oddRepository, IRepository<Bet> betRepository)
        {
            this.xmlProcessor = xmlProcessor;
            this.sportFeedService = sportFeedService;
            this.matchRepository = matchRepository;
            this.oddRepository = oddRepository;
            this.betRepository = betRepository;
        }

        public async Task<IEnumerable<MatchViewModel>> GetAllMatchesInLast24Hours<MatchViewModel>()
        {
            SportViewModel sportFeed = await xmlProcessor.GetSportDataFeedAsync();

            await sportFeedService.SaveIfNewFeedAvailable(sportFeed);

            //IEnumerable<MatchViewModel> res = await matchRepository.All()
            //    .Where(m => m.StartDate >= DateTime.UtcNow && m.StartDate < targetDateTime)
            //    .Select(m => new Match()
            //    {
            //        Bets = (ICollection<Bet>)betRepository.All()
            //            .Where(x => x.MatchId == m.Id).ToList(),
            //    })
            //    .To<MatchViewModel>()
            //    .ToListAsync();


            IEnumerable<MatchViewModel> res = await matchRepository.All()
                .Where(m => m.StartDate >= DateTime.UtcNow && m.StartDate < targetDateTime)
                .To<MatchViewModel>()
                .ToListAsync();


            //IEnumerable<MatchViewModel> res = await betRepository.All()
            //    .Select(b=>b.Odds.GroupBy(o=>o.SpecialBetValue)).Where(x=>x.Count() >1)
            //    .Select(b=>b.Match)
            //    .Where(m => m.StartDate >= DateTime.UtcNow && m.StartDate < targetDateTime)
            //    .To<MatchViewModel>()
            //    .ToListAsync();



            //var res = await oddRepository.All()
            //    .Where(o=>o.SpecialBetValue == null)
            //    .Select(o=>o.Bet.Match)
            //    .Where(m=>m.StartDate >= DateTime.UtcNow && m.StartDate < targetDateTime)
            //    .To<T>()
            //    .ToListAsync();


            return res;
        }

        public async Task<T> GetSingleMatchByID<T>(int Id) => await matchRepository.All().Where(m=> m.Id == Id).To<T>().FirstOrDefaultAsync();
    }
}
