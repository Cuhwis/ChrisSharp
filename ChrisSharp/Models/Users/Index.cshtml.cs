using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ChrisSharp.Data;
using ChrisSharp.Models;

namespace ChrisSharp.Models.Users
{
    public class IndexModel : PageModel
    {
        private readonly ChrisSharp.Data.ChrisSharpContext _context;

        public IndexModel(ChrisSharp.Data.ChrisSharpContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; }

        public async Task OnGetAsync()
        {
            //User = await _context.User.ToListAsync();
        }
    }
}
