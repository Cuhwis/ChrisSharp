using System;
using System.Reflection;
using System.Threading.Tasks;
using ChrisSharp;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace ChrisSharp
{
    class BotStart
    {
        public static void StartBot() => new BotStart().RunBotAsync().GetAwaiter().GetResult();
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;
        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient(new DiscordSocketConfig { MessageCacheSize = 100, LogLevel = LogSeverity.Critical });
            _commands = new CommandService();
            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            string token = "#######";
            //events
            _client.Log += _client_Log;
            _client.Ready += clientReady;

            await RegisterCommandsAsync();
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await Task.Delay(-1);
        }
        private async Task clientReady()
        {
            Console.WriteLine($"[{DateTime.Now}] Logged in as: {_client.CurrentUser.Username}");
            await _client.SetGameAsync("Despacito", null, ActivityType.Listening);
            Console.WriteLine($"[{DateTime.Now}] Client: Ready");
        }
        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            //Event recieving message of user and runs HandleCommandAsync method below
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            string prefix = "/";
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix(prefix, ref argPos))
            {
                var result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess)
                {
                    await context.Channel.SendMessageAsync($"{context.Message.Author.Mention} Error: {result.ErrorReason}");
                }
            }
        }
    }
}
