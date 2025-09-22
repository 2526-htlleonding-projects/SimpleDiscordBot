using System.Reflection;
using System.Text.Json;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;

namespace DiscordBot_Core;

using Discord;

class Program
{
    private DiscordSocketClient _client = null!;
    private InteractionService _interactions = null!;

    public static Task Main(string[] args) => new Program().MainAsync();

    private async Task MainAsync()
    {
        //set client
        _client = new DiscordSocketClient(new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.AllUnprivileged | (GatewayIntents)4096 |  GatewayIntents.GuildMembers | GatewayIntents.Guilds
        });
        
        //set interactions
        _interactions = new InteractionService(_client.Rest);
        await _interactions.AddModulesAsync(Assembly.GetEntryAssembly(), null);

        
        //event handlers
        _client.Log += LogAsync;
        _client.Ready += OnReady;
        _client.InteractionCreated += HandleInteractions;
        
        //get token
        var json = await File.ReadAllTextAsync("config.json");
        var cfg = JsonSerializer.Deserialize<BotConfig>(json);
        var token = cfg!.Token;
        
        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
        
        await Task.Delay(-1);
    }

    public class BotConfig
    {
        public string Token { get; init; } = null!;
    }
    
    private async Task HandleInteractions(SocketInteraction arg)
    {
        var ctx = new SocketInteractionContext(_client, arg);
        await _interactions.ExecuteCommandAsync(ctx, null);
    }

    private async Task<Task> OnReady()
    {
        Console.WriteLine("Bot is ready!");

        const ulong guildId = 1411698864783888618;
        await _interactions.RegisterCommandsToGuildAsync(guildId);
        
        return Task.CompletedTask;
    }

    private Task LogAsync(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}