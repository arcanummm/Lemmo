namespace Lemmo.WebApi.Persistance.TelegramBot.Interfaces
{
    public interface ITelegramBotService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
