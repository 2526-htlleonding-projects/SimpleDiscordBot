using Discord.Commands;
using Discord.Interactions;

namespace DiscordBot_Core.modules;

public class BasicModule : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("hibot", "a friendly greeting")]
    public async Task Greeting()
        => await ReplyAsync("Hi dear messenger! Great to see you again!");
}