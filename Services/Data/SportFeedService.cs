using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using UltraPlayBettingSystemExercise.Data.Models;
using UltraPlayBettingSystemExercise.Data.Repositoies;
using UltraPlayBettingSystemExercise.Services.Mapping;
using UltraPlayBettingSystemExercise.ViewModels;
using ViewModels;

namespace Services.Data
{
    public class SportFeedService : ISportFeedService
    {
        private readonly IDeletableEntityRepository<Sport> sportRepository;
        private readonly IMessagesService messagesService;
        private readonly IMapper mapper;
        private const int seconds = 60;

        public SportFeedService(IDeletableEntityRepository<Sport> sportRepository, 
            IMessagesService messagesService, 
            IMapper mapper)
        {
            this.sportRepository = sportRepository;
            this.messagesService = messagesService;
            this.mapper = mapper;
        }

        public async Task<bool> SaveIfNewFeedAvailable(SportViewModel viewModel)
        {
            var dbFeedDate = await sportRepository.All().Select(x=>x.CreatedDate).FirstOrDefaultAsync();
            var dbDatePlusSeconds = dbFeedDate.AddSeconds(seconds);

            if(dbDatePlusSeconds <= viewModel.CreatedDate || dbFeedDate == DateTime.MinValue)
            {
                await this.DeleteSportFeed();
                var message = await this.SaveFeedToDB(viewModel);
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
    }
}
