using Discord.Commands;
using Discord.Interactions;

namespace DiscordBot_Core.modules;

public class NiceBot : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("hibot", "a friendly greeting")]
    public async Task Greeting()
        => await RespondAsync("Hi dear messenger! Great to see you again!");
}