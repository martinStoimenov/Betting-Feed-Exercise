namespace Services.Interfaces
{
    public interface IMessagesService
    {
        Task<T> SaveMessage<T>(string content, string type);
        Task DeleteMessages();
    }
}