using Lemmo.WebApi.Persistance.TelegramBot.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace Lemmo.WebApi.Persistance.TelegramBot.Services
{
    public class TelegramBotHostedService(ITelegramBotClient botClient, ITelegramUpdateHandler updateHandler, ILogger<TelegramBotHostedService> logger) : IHostedService
    {
        private CancellationTokenSource _cts = new();

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = [],
            };

            _ = Task.Run(async () =>
            {
                try
                {
                    await botClient.ReceiveAsync(
                        updateHandler: updateHandler,
                        receiverOptions: receiverOptions,
                        cancellationToken: _cts.Token
                    );
                }
                catch (OperationCanceledException) when (_cts.IsCancellationRequested)
                {
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Ошибка в ReceiveAsync");
                }
            }, cancellationToken);

            logger.LogInformation("Telegram бот запущен");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cts.Cancel();
            logger.LogInformation("Telegram бот остановлен");
            return Task.CompletedTask;
        }
    }
}
