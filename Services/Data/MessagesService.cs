using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using UltraPlayBettingSystemExercise.Data.Repositoies;
using UltraPlayBettingSystemExercise.Services.Mapping;

namespace Services.Data
{
    public class MessagesService : IMessagesService
    {
        private readonly IDeletableEntityRepository<Message> messageRepository;

        public MessagesService(IDeletableEntityRepository<Message> messageRepository)
        {
            this.messageRepository = messageRepository;
        }

        public async Task<T> SaveMessage<T>(string content, string type)
        {
            if (content == null)
                throw new ArgumentNullException();

            var message = new Message()
            {
                Content = content,
                Type = type
            };

            await messageRepository.AddAsync(message);
            await messageRepository.SaveChangesAsync();

            return await messageRepository.All().OrderByDescending(x => x.CreatedOn).To<T>().FirstOrDefaultAsync();
        }

        public async Task DeleteMessages()
        {
            await messageRepository.All().ExecuteDeleteAsync();
            await messageRepository.SaveChangesAsync();
        }
    }
}
