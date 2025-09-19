using Discord;
using Discord.Interactions;

namespace DiscordBot_Core.modules;

public class Judge : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("judgeme", "you dare come before the judge?")]
    public async Task Greeting()
    {
        var allMembers = Context.Guild.Users
            .Where(u => !u.IsBot) // skip bots
            .ToList();

        if (allMembers.Count < 2)
        {
            await RespondAsync("Need at least 2 members to judge.");
            return;
        }

        var random = new Random();

        // Pick random number just for flavor
        var randomNumber = random.Next(1, allMembers.Count + 1);

        // Shuffle
        var shuffled = allMembers.OrderBy(_ => random.Next()).ToArray();

        // Split
        var half = allMembers.Count / 2;
        var guiltyList = shuffled.Take(half + (allMembers.Count % 2)).Select(u => u.Username);
        var innocentList = shuffled.Skip(half + (allMembers.Count % 2)).Select(u => u.Username);

        // Build embed
        var embed = new EmbedBuilder()
            .WithTitle("⚖️ The Judge Has Spoken ⚖️")
            .WithDescription($"Random Number: **{randomNumber}**\n\n" +
                             $"**Guilty:** {string.Join(", ", guiltyList)}\n\n" +
                             $"**Innocent:** {string.Join(", ", innocentList)}")
            .WithColor(Color.DarkRed);

        await RespondAsync(embed: embed.Build());
    }
}