using Discord.Commands;
using Discord.Interactions;
using RunMode = Discord.Interactions.RunMode;

namespace DiscordBot_Core.modules;

public class TheFunnyOne : ModuleBase<SocketCommandContext>
{
    [SlashCommand("funny", "funne", false, RunMode.Async)]
    public async Task FunnyAsync()
        => await ReplyAsync("Why don’t scientists trust atoms? Because they make up everything!");
}