using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChrisSharp.Models;

namespace ChrisSharp.Data
{
    public class ChrisSharpContext : DbContext
    {
        public ChrisSharpContext (DbContextOptions<ChrisSharpContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
