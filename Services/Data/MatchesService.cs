using UltraPlayBettingSystemExercise.ViewModels;
using UltraPlayBettingSystemExercise.Services.Interfaces;
using Services.Interfaces;
using UltraPlayBettingSystemExercise.Data.Repositoies;
using UltraPlayBettingSystemExercise.Data.Models;
using UltraPlayBettingSystemExercise.Services.Mapping;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using AutoMapper.Configuration.Annotations;
using Services.Data;

namespace UltraPlayBettingSystemExercise.Services
{
    public partial class MatchesService : IMatchesService
    {
        private readonly string[] previewMarkets = new string[] { "Map Advantage", "Match Winner", "Total Maps Played" };
        private readonly DateTime targetDateTime = DateTime.UtcNow.AddHours(24);
        private readonly ISportFeedService sportFeedService;
        private readonly IXMLProcessor xmlProcessor;
        private readonly IRepository<Match> matchRepository;
        private readonly IRepository<Odd> oddRepository;
        private readonly IRepository<Bet> betRepository;

        public MatchesService(IRepository<Match> matchRepository,
            IRepository<Odd> oddRepository,
            IRepository<Bet> betRepository,
            ISportFeedService sportFeedService,
            IXMLProcessor xmlProcessor)
        {
            this.matchRepository = matchRepository;
            this.oddRepository = oddRepository;
            this.betRepository = betRepository;
            this.sportFeedService = sportFeedService;
            this.xmlProcessor = xmlProcessor;
        }

        public async Task<IEnumerable<MatchViewModel>> GetAllMatchesInLast24Hours<MatchViewModel>()
        {
            SportViewModel sportFeed = await xmlProcessor.GetSportDataFeedAsync();

            await sportFeedService.SaveIfNewFeedAvailable(sportFeed);
            //sportFeedService.GetXMLFeedEvery60Seconds();

            // if the bet odds doesn't have SBT return all odds from this bet
            // if the bet has more than one group of sbt return only the first group

            //   <Bet Name="Winner Map 2" ID="44736939" IsLive="true">
            //      < Odd Name = "1" ID = "305535598" Value = "1.61" />     -return all odds
            //      < Odd Name = "2" ID = "305535603" Value = "2.20" />     -return all odds
            //   </ Bet >
            //   < Bet Name = "Round Advantage (Map 2)" ID = "44736946" IsLive = "true" >
            //      < Odd Name = "1" ID = "305535615" Value = "1.84" SpecialBetValue = "-2.5" />    -return only this group
            //      < Odd Name = "2" ID = "305535614" Value = "1.84" SpecialBetValue = "-2.5" />    -return only this group

            //      < Odd Name = "1" ID = "305540643" Value = "1.60" SpecialBetValue = "2.5" />     -do not return the second group
            //      < Odd Name = "2" ID = "305540642" Value = "2.18" SpecialBetValue = "2.5" />     -do not return the second group
            //      ...
            //   </ Bet >


            //IEnumerable<MatchViewModel> res = await matchRepository.All()

            //    .Where(m => m.StartDate >= DateTime.UtcNow && m.StartDate < targetDateTime
            //        //&& m.Bets.Any(b => previewMarkets.Contains(b.Name)
            //        //&& b.Odds.All(o=>o.SpecialBetValue != null)
            //        //)
            //        )

            //    .To<MatchViewModel>()
            //    .ToListAsync();

            //var groupedCustomerList = oddRepository.All()
            //    .Where(o => previewMarkets
            //        .Contains(o.Bet.Name) && o.Bet.Match.StartDate >= DateTime.UtcNow && o.Bet.Match.StartDate < targetDateTime)
            //    .GroupBy(u => u.SpecialBetValue ?? null)
            //    .Select(grp => grp.ToList())
            //    .ToList();


            IEnumerable<MatchViewModel> res = await matchRepository.All()
                .Where(m => m.StartDate >= DateTime.UtcNow && m.StartDate < targetDateTime)
                .Select(m => new Match()
                {
                    Bets = m.Bets.Where(b => previewMarkets.Contains(b.Name))
                    .Select(b => new Bet()
                    {
                        Odds = b.Odds//.ToList().ForEach(x =>
                        //{
                        //    if (x.SpecialBetValue != null)
                        //        x.Bet.Odds.ToList().RemoveRange(2, x.Bet.Odds.Count());
                        //})
                        ,
                        Id = b.Id,
                        Name = b.Name,
                        IsLive = b.IsLive,
                        Match = b.Match,
                        MatchId = b.MatchId
                    }).ToList(),
                    Name = m.Name,
                    Id = m.Id,
                    Event = m.Event,
                    EventId = m.EventId,
                    MatchType = m.MatchType,
                    StartDate = m.StartDate
                })
                .Where(m=>m.Bets.Count() > 0)
                .To<MatchViewModel>()
                .ToListAsync();


            var a = 0;

            return res;
        }

        public async Task<T> GetSingleMatchByID<T>(int Id) => await matchRepository.All().Where(m => m.Id == Id).To<T>().FirstOrDefaultAsync();
    }
}
