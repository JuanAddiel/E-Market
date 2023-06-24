using E_Market.Core.Application.Interface.Repositories;
using E_Market.Core.Domain.Entites;
using E_Market.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Infrastructure.Persistence.Repositories
{
    public class UserRepository:GenericRepository<User>, IUserRepository
    {
        private readonly E_MarketContext _context;
        public UserRepository(E_MarketContext context): base(context)
        {
            _context = context;
        }
    }
}
