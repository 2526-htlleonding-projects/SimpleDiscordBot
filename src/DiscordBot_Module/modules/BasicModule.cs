using Discord.Commands;

namespace DiscordBot_Module.modules;

public class BasicModule : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    public async Task PingAsync()
        => await ReplyAsync("Pong!");
}