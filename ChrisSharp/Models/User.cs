using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChrisSharp.Models
{
    public class User
    {
        public ulong UserID { get; set; }
        public ulong ChannelID { get; set; }
        public string GitHub { get; set; }
        public string LinkedIn { get; set; }
        public string EpicID { get; set; }
    }
}
