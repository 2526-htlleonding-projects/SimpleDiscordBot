using System.Reflection;
using System.Text.Json;
using Discord.Commands;
using Discord.WebSocket;

namespace DiscordBot_Core;

using Discord;

class Program
{
    private DiscordSocketClient _client = null!;
    private CommandService _commands = null!;

    public static Task Main(string[] args) => new Program().MainAsync();

    private async Task MainAsync()
    {
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | (GatewayIntents)4096
        });
        _commands = new CommandService();

        _client.Log += Log;
        _client.Ready += OnReady;

        var json = await File.ReadAllTextAsync("config.json");
        var cfg = JsonSerializer.Deserialize<BotConfig>(json);
        var token = cfg!.Token;
        
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
        
        await InitCommands();
        await Task.Delay(-1);
    }

    public class BotConfig
    {
        public string Token { get; set; } = null!;
    }
    
    private async Task InitCommands()
    {
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);
        _client.MessageReceived += HandleCommandAsync;
    }

    private async Task HandleCommandAsync(SocketMessage msg)
    {
        var message = msg as SocketUserMessage;
        if (message == null || message.Author.IsBot) return;

        var argPos = 0;
        if (message.HasCharPrefix('!', ref argPos))
        {
            var context = new SocketCommandContext(_client, message);
            await _commands.ExecuteAsync(context, argPos, null);
        }
    }

    private Task OnReady()
    {
        Console.WriteLine("Bot is ready!");
        return Task.CompletedTask;
    }

    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}