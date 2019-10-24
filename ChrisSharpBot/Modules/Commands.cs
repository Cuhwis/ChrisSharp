using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Discord.Net;
using Discord.WebSocket;
using System.IO;
using System.Collections;
using System.Net;
using System.Linq;
using ChrisSharpBot.Modules;
using HtmlAgilityPack;

namespace ChrisSharpBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("test")]
        public async Task test()
        {
            await ReplyAsync("https://google.com/");
        }
        public string prefix = "/";
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
        public async Task getUserID(SocketUser user, string reason = null)
        {
            await ReplyAsync(null, false, EmbedColorMsg.ColoredMsg($" {Context.Message.Author.Username}, requested ID : {user.Id}"));
        }
        [Command("kick")]
        [Remarks("/kick [user] [reason]")]
        [Summary("This allows admins to kick users.")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        [RequireBotPermission(GuildPermission.KickMembers)]
        public async Task KickUser(IGuildUser user, string reason = "No reason provided.")
        {
            try
            {
                await user.KickAsync(reason);
                await ReplyAsync($"{Context.Message.Author.Username}, {user} has been kicked.");
            }
            catch
            {
                await ReplyAsync($"{Context.Message.Author.Mention} Please use format \"{prefix}kick @example#1337 \"");
            }
        }
        [Command("timeout")]
        [Remarks("/kick [user] [reason]")]
        [Summary("This allows admins to kick users.")]
        [RequireUserPermission(GuildPermission.ManageRoles)]
        [RequireBotPermission(GuildPermission.ManageRoles)]
        public async Task TimeOut(IGuildUser user, double sec = 0)
        {

        }
        [Command("whoami")]
        public async Task WhoAmI()
        {
            var user = Context.Message.Author;
            if (user.Id == 205209327487680513 || user.Id == 451585633714962432)
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
        [Command("meme")]
        [Summary("Get a meme from Gerards Website!")]
        public async Task meme()
        {
            string path = "https://litmemes.blob.core.windows.net/images/";
            WebClient temp = new WebClient();
            string response = temp.DownloadString(path);

            // Load the Html into the agility pack
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(response);

            // Now, using LINQ to get all Images
            List<HtmlNode> imageNodes = null;
            imageNodes = (from HtmlNode node in doc.DocumentNode.SelectNodes("//img")
                          where node.Name == "img" && node.Attributes["class"] != null && node.Attributes["class"].Value.StartsWith("https://litmemes.blob.core.windows.net/images/")
                          select node).ToList();
            foreach (var item in imageNodes)
            {
                await ReplyAsync(item.ToString());
            }
        }
        [Command("github")]
        public async Task GitHub()
        {
            await ReplyAsync("/github @user, will display user's github\n");
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
        [Command("griffsays")]
        public async Task Likur()
        {
            await ReplyAsync("LIKKUUUUUURRRRRRRRRRRR");
        }
        [Command("bot")]
        public async Task bot()
        {
            await ReplyAsync("<@205209327487680513> : https://ibb.co/qM21Tqy");
        }
        [Command("8ball")]
        [Alias("ask")]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task AskEightBall([Remainder]string args = null)
        {
            // I like using StringBuilder to build out the reply
            var sb = new StringBuilder();
            // let's use an embed for this one!
            var embed = new EmbedBuilder();
            // now to create a list of possible replies
            var replies = new List<string>();
            // add our possible replies
            replies.Add("yes");
            replies.Add("no");
            replies.Add("maybe");
            replies.Add("hazzzzy....");
            // time to add some options to the embed (like color and title)
            embed.WithColor(new Discord.Color(0, 255, 0));
            embed.Title = "Welcome to the 8-ball!";
            // we can get lots of information from the Context that is passed into the commands
            // here I'm setting up the preface with the user's name and a comma
            sb.AppendLine($"");
            sb.AppendLine();
            // let's make sure the supplied question isn't null 
            if (args == null)
            {
                // if no question is asked (args are null), reply with the below text
                sb.AppendLine("Sorry, can't answer a question you didn't ask!");
            }
            else
            {
                // if we have a question, let's give an answer!
                // get a random number to index our list with (arrays start at zero so we subtract 1 from the count)
                var answer = replies[new Random().Next(replies.Count - 1)];
                // build out our reply with the handy StringBuilder
                sb.AppendLine($"You asked: [{Context.Message}]...");
                sb.AppendLine();
                sb.AppendLine($"...your answer is [{answer}]");
                // bonus - let's switch out the reply and change the color based on it
                switch (answer)
                {
                    case "yes":
                        {
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            break;
                        }
                    case "no":
                        {
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            break;
                        }
                    case "maybe":
                        {
                            embed.WithColor(new Discord.Color(255, 255, 0));
                            break;
                        }
                    case "hazzzzy....":
                        {
                            embed.WithColor(new Discord.Color(255, 0, 255));
                            break;
                        }
                }
            }
            // now we can assign the description of the embed to the contents of the StringBuilder we created
            embed.Description = sb.ToString();
            // this will reply with the embed
            await ReplyAsync(null, false, embed.Build());
        }
        [Command("awwskeetskeet")]
        public async Task skeet()
        {
            await ReplyAsync("MOTHA FUCKAAAAAAAAA");
        }

    }
}
