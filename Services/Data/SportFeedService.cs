using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using System.Reactive.Linq;
using UltraPlayBettingSystemExercise.Data.Models;
using UltraPlayBettingSystemExercise.Data.Repositoies;
using UltraPlayBettingSystemExercise.Services.Interfaces;
using UltraPlayBettingSystemExercise.Services.Mapping;
using UltraPlayBettingSystemExercise.ViewModels;
using ViewModels;

namespace Services.Data
{
    public class SportFeedService : ISportFeedService
    {
        private readonly IDeletableEntityRepository<Sport> sportRepository;
        private readonly IMessagesService messagesService;
        private readonly IXMLProcessor xmlProcessor;
        private readonly IMapper mapper;
        private const int seconds = 60;

        public SportFeedService(IDeletableEntityRepository<Sport> sportRepository,
            IMessagesService messagesService,
            IMapper mapper,
            IXMLProcessor xmlProcessor)
        {
            this.sportRepository = sportRepository;
            this.messagesService = messagesService;
            this.mapper = mapper;
            this.xmlProcessor = xmlProcessor;
        }

        public async Task<bool> SaveIfNewFeedAvailable(SportViewModel feed)
        {
            var dbFeedDate = await sportRepository.All().Select(x=>x.CreatedDate).FirstOrDefaultAsync();
            var dbDatePlusSeconds = dbFeedDate.AddSeconds(seconds);

            if(dbDatePlusSeconds <= feed.CreatedDate || dbFeedDate == DateTime.MinValue)
            {
                await this.DeleteSportFeed();
                var message = await this.SaveFeedToDB(feed);
                return true;
            }
            return false;
        }

        public async Task<T> GetFeed<T>() => await sportRepository.All().To<T>().FirstOrDefaultAsync();

        private async Task<MessageViewModel> SaveFeedToDB(SportViewModel viewModel)
        {
            var sport = mapper.Map<Sport>(viewModel);

            await sportRepository.AddAsync(sport);

            await sportRepository.SaveChangesAsync();

            return await messagesService.SaveMessage<MessageViewModel>("New Sport Feed was saved.", "Changes in Sport Feed");
        }

        private async Task DeleteSportFeed()
        {
            await messagesService.DeleteMessages();

            var feed = await sportRepository.AllAsNoTracking().FirstOrDefaultAsync();

            if (feed != null)
            {
                sportRepository.HardDelete(feed);
            }
            await sportRepository.SaveChangesAsync();
        }

        private async Task ConsumeFeedEvery60Seconds()
        {
            SportViewModel sportFeed = await xmlProcessor.GetSportDataFeedAsync();

            await this.SaveIfNewFeedAvailable(sportFeed);
        }

        
        public void GetXMLFeedEvery60Seconds()
        {
            var timer = Observable.Generate(
                new { now = DateTimeOffset.Now, count = 0 },
                t => true,
                t => new { t.now, count = t.count + 1 },
                t => t.count,
                t => t.now.AddMinutes(t.count))
            .SelectMany(x => Observable.FromAsync(async () => await this.ConsumeFeedEvery60Seconds()));

            timer.Subscribe();
        }
    }
}
