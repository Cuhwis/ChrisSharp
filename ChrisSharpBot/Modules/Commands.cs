using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Net;
using Discord.WebSocket;
using System.IO;
using System.Collections;
using System.Net;
using Discord;
using System.Linq;

namespace ChrisSharpBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        string prefix = "/";
        [Command("ping")]
        public async Task Ping()
        {
            await ReplyAsync("Pong");
        }
        [Command("fntrack")]
        public async Task FnTrack(string user)
        {
            await ReplyAsync($"https://fortnitetracker.com/profile/all/{user}");
        }
        [Command("coolpersontest")]
        public async Task Chris()
        {
            await ReplyAsync("you mean <@354510274411233281>");
        }
        [Command("getuserid")]
        [Summary("This allows users to get UserID without dev mode.")]
        public async Task getUserID(SocketUser user)
        {
            await ReplyAsync($"{Context.Message.Author.Username}, requested ID : {user.Id}");
        }
        [Command("kick")]
        [Remarks("!kick [user] [reason]")]
        [Summary("This allows admins to kick users.")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        //TODO Wont Work. Need Fixin'
        public async Task KickUser(SocketUser user, string reason = null)
        {
            try
            {
                await (user as IGuildUser).KickAsync(reason);
                await ReplyAsync($"{Context.Message.Author.Username}, {user} has been kicked.");
            }
            catch
            {
                await ReplyAsync($"{Context.Message.Author.Mention} Please use format \"{prefix}kick @example#1337 \"");
            }
        }
        [Command("timeout")]
        //TODO Get working
        public async Task TimeOut(SocketUser userName, double seconds = 0)
        {
            var user = Context.User as SocketGuildUser;
            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Role");

            //if (Context.Guild.Roles)
            //var timeout = DateTime.Now + DateTime.Now.AddMinutes(seconds);
            //if (DateTime.Now > timeout)
            //{

            //}
        }
        [Command("whoami")]
        public async Task WhoAmI()
        {
            var user = Context.Message.Author;
            if (user.Id == 205209327487680513)
                await (user as IGuildUser).SendMessageAsync("You are Mrs. Ruaboro");
            if (user.Id == 354510274411233281)
                await (user as IGuildUser).SendMessageAsync("You are Mr. Ruaboro");
        }
        [Command("delete")]
        [Summary("Clear an amount of messages from user in the channel")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task DeleteMsgs(SocketUser user, int numMsgs = 0)
        {
            int cap = 20;
            try
            {
                if (numMsgs < 1 || numMsgs > cap)
                    await ReplyAsync($"{Context.Message.Author.Mention} Max amount is {cap}.\"");
                else
                {
                    var messages = await Context.Channel.GetMessagesAsync(100).FlattenAsync();
                    int count = 0;
                    foreach (IMessage msg in messages)
                    {
                        if (count < numMsgs)
                        {
                            if (msg.Author.Id == user.Id)
                            {
                                await Context.Channel.DeleteMessageAsync(msg);
                                count++;
                            }
                        }
                        else break;
                    }
                    await ReplyAsync($"{Context.Message.Author.Mention} Deleted {count}/{numMsgs} from {user.Mention}.\"");
                }
            }
            catch
            {
                await ReplyAsync($"{Context.Message.Author.Mention} Please use format \"{prefix}delete @example#1337 5\"");
            }
        }
        [Command("delete")]
        [Summary("Clear an amount of messages in the channel")]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task DeleteMsgs(int numMsgs = 0)
        {
            int cap = 100;
            try
            {
                if (numMsgs < 1 || numMsgs > cap)
                    await ReplyAsync($"{Context.Message.Author.Mention} Max amount is {cap}.\"");
                else
                {
                    var messages = await Context.Channel.GetMessagesAsync(numMsgs + 1).FlattenAsync();
                    var channel = Context.Channel as ITextChannel;
                    await channel.DeleteMessagesAsync(messages);
                    await ReplyAsync($"{Context.Message.Author.Mention} Deleted {numMsgs} messages from {Context.Channel.Name}.");
                }
            }
            catch
            {
                await ReplyAsync($"{Context.Message.Author.Mention} Please use format \"{prefix}delete 5\"");
            }
        }
        [Command("github")]
        [Summary("Get the link to others Github!")]
        public async Task GitHub(SocketUser user)
        {
            try
            {
                ulong userId = Context.Message.Author.Id;
                ulong targetId = user.Id;
                string path = $"UserData\\{targetId}.txt";
                if (File.Exists(path))
                {
                    string gitHub = File.ReadAllText(path);
                    await ReplyAsync($"{user.Mention}'s GitHub:\n{gitHub}");
                }
                else
                {
                    await ReplyAsync($"{user.Mention} doesn't have a GitHub linked.");
                }

            }
            catch
            {
                await ReplyAsync($"{Context.Message.Author.Mention} Please use format \"{prefix}github @example#1337\"");
            }
        }
        [Command("linkgithub")]
        [Summary("Link your github so other people can see your hardwork")]
        public async Task LinkGitHub(string gitHub = null)
        {
            //https://github.com/discordapp
            ulong userId = Context.Message.Author.Id;
            var user = Context.Message.Author;
            if (gitHub == null || !gitHub.ToLower().Contains("github.com/") || gitHub.Contains("?"))
            {
                await ReplyAsync($"{user.Mention} Please use format \"{prefix}linkgithub https://github.com/example\"");
            }
            else
            {
                bool validGitHub = false;
                try
                {
                    WebClient temp = new WebClient();
                    string response = temp.DownloadString(gitHub);
                    if (response.Contains("Block or report user"))
                        validGitHub = true;
                }
                catch
                {
                    validGitHub = false;
                }
                if (validGitHub)
                {
                    string path = $"UserData\\{userId}.txt";
                    File.WriteAllText(path, gitHub);
                    await ReplyAsync($"{user.Mention} GitHub[{gitHub.Split(".com/", StringSplitOptions.RemoveEmptyEntries)[1]}]: Linked."); //
                }
                else
                    await ReplyAsync($"{user.Mention} Please use format \"{prefix}linkgithub https://github.com/example\"");
            }
        }
        //[Command("griffsays")]
        //public async Task Likur()
        //{
        //    await ReplyAsync("LIKKUUUUUURRRRRRRRRRRR");
        //}
        //[Command("choco")]
        //public async Task Kwasi()
        //{
        //    await ReplyAsync("<@354510274411233281> ");
        //}
        //[Command("bot")]
        //public async Task bot()
        //{
        //    await ReplyAsync("<@205209327487680513> : ");
        //}
    }
}
