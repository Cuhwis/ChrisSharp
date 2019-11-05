using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChrisSharp.Models
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
    }
}
