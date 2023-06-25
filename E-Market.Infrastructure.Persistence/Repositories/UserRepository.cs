using E_Market.Core.Application.Helpers;
using E_Market.Core.Application.Interface.Repositories;
using E_Market.Core.Application.ViewModel.User;
using E_Market.Core.Domain.Entites;
using E_Market.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
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
        public UserRepository(E_MarketContext context) : base(context)
        {
            _context = context;
        }
        public override async Task Add(User entity)
        {
            //Estoy cumpliendo con el pincipio de liskov, ya que estoy sobreescribiendo el metodo AddAsync de la clase GenericRepository
            entity.Password = PasswordEncryption.ComputeSha256Hash(entity.Password);
            await base.Add(entity);
        }


        public async Task<User> LoginAsync(LoginViewModel vm)
        {
            string passwordEncrypt = PasswordEncryption.ComputeSha256Hash(vm.Password);
            User users = await _context.Set<User>().FirstOrDefaultAsync(x => x.NombreUsuario == vm.NombreUsuario && x.Password == passwordEncrypt);
            return users;
        }
    }
}
