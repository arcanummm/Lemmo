using Lemmo.WebApi.Application;
using Lemmo.WebApi.Infrastructure.Data;
using Lemmo.WebApi.Persistance.Auth;
using Lemmo.WebApi.Persistance.TelegramBot;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAppDB(builder.Configuration);

        builder.Services.AddAuth(builder.Configuration);

        // Регистрация сервисов Telegram бота
        builder.Services.AddTelegramBot(builder.Configuration);
        builder.Services.AddMediator();
        var app = builder.Build();

        app.ApplyMigrations();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}

