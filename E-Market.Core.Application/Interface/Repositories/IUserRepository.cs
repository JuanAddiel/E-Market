using E_Market.Core.Application.ViewModel.User;
using E_Market.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Interface.Repositories
{
    public interface IUserRepository:IGenericRepository<User>
    {
        Task<User> LoginAsync(LoginViewModel vm);
    }
}
