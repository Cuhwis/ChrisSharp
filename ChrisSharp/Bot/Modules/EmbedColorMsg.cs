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
namespace ChrisSharp.Modules
{
    class EmbedColorMsg
    {
        public static Embed ColoredMsg(string input, Discord.Color color)
        {
            var embed = new EmbedBuilder();
            if (color == null)
            {
                color = Discord.Color.Magenta;
            }
            embed.WithColor(color);
            embed.Description = input;            
            return embed.Build();
        }
        public static Embed ColoredMsg(string input)
        {
            var embed = new EmbedBuilder();
            Discord.Color color = Discord.Color.Purple;
            embed.WithColor(color);
            embed.Description = input;
            return embed.Build();
        }
    }
}
