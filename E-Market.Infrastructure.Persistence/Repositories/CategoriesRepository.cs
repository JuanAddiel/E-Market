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
    public class CategoriesRepository:GenericRepository<Category>, ICategoryRepository
    {
        private readonly E_MarketContext _context;
        public CategoriesRepository(E_MarketContext context):base(context)
        {
            _context = context;
        }
    }
}
