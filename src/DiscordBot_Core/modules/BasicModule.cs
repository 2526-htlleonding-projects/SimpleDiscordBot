using Discord;
using Discord.Commands;
using Discord.Interactions;

namespace DiscordBot_Core.modules;

public class NiceBot : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("hibot", "a friendly greeting (if you are a nice person)")]
    public async Task Greeting()
    {
        var name = Context.User.Username;
        /*var embed = new EmbedBuilder();   
        embed.Description = "Nigga";
        embed.Title = "Greetings";*/
        if(name is "integr_" or "schokoladenmuffin.") await RespondAsync($"You are a fucking monkey {name}! (You've reached EvilBot)");
        else await RespondAsync($"You are a lovely person {name}! (You've reached NiceBot)");
    }
}