namespace DiscordBot_Core.modules;

public class TheFunnyOne : ModuleBase<SocketCommandContext>
{
    [Command("funny")]
    public async Task PingAsync()
        => await ReplyAsync("Why don’t scientists trust atoms? Because they make up everything!");
}