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

        string[] insults =
            ["fucking monkey", "retard", "dumb ahh", "little bitch", "Beckenrandschwimmer", "Schwammerl"];

        var rnd = new Random();

        if (name is "integr_" or "schokoladenmuffin.")
        {
            await RespondAsync($"You are a {insults[rnd.Next(1, insults.Length + 1)]} {name}! (You've reached EvilBot)");
            Console.WriteLine("insulting Erik and Almir...");
        }
        else
        {
            await RespondAsync($"You are a lovely person {name}! (You've reached NiceBot)");
            Console.WriteLine("being a nice bot...");
        }
    }
}