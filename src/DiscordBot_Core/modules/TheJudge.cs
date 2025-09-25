using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace DiscordBot_Core.modules;

public class Judge : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("judgeme", "you dare come before the judge?")]
    public async Task JudgeMe()
    {
        Console.WriteLine("-- Strating Trial --");
        
        var guild = Context.Guild as SocketGuild;
        await guild.DownloadUsersAsync(); // fetches all members from the API
        var user =  Context.User as SocketGuildUser;
        
        var allMembers = guild.Users
            .Where(u => !u.IsBot) // skip bots
            .ToList();
        
        var numberOfMember = 0;
        foreach (var member in allMembers)
        {
            numberOfMember++;
            Console.WriteLine($"#{numberOfMember}: {member.Mention}");
        }

        if (allMembers.Count < 2)
        {
            await RespondAsync("Need at least 2 members to judge.");
            return;
        }

        var shuffled = allMembers;
        shuffled.Shuffle();

        // Split
        var half = allMembers.Count / 2;
        var guiltyList = shuffled.Take(half + (allMembers.Count % 2)).Select(u => u.Username);
        var innocentList = shuffled.Skip(half + (allMembers.Count % 2)).Select(u => u.Username);

        // guilty verdict
        var isGuilty = guiltyList.Contains(user?.Username);
        var judgement = "not guilty";
        if(isGuilty) judgement = "guilty";
        
        // Build embed
        var embed = new EmbedBuilder()
            .WithTitle("The Judge Has Spoken")
            .WithDescription($"You, {user?.Username}, are **{judgement}**.")
            .WithColor(Color.DarkRed);

        await RespondAsync(embed: embed.Build());
    }
}