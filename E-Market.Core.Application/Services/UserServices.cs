using E_Market.Core.Application.Interface.Repositories;
using E_Market.Core.Application.Interface.Services;
using E_Market.Core.Application.ViewModel.User;
using E_Market.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Add(SaveUserViewModel vm)
        {
            User user = new();
            user.Id = vm.Id;
            user.Nombre = vm.Nombre;
            user.NombreUsuario = vm.NombreUsuario;
            user.Correo = vm.Correo;
            user.Telefono = vm.Telefono;

            await _userRepository.Add(user);
        }

        public async Task Delete(int id)
        {
            var usuario = await _userRepository.GetByIdAsync(id);
            await _userRepository.Delete(usuario);
        }

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var userList = await _userRepository.GetAllWithInclude(new List<string> { "Anuncio" });
            return userList.Select(a => new UserViewModel
            {
                Id = a.Id,
                Nombre = a.Nombre,
                NombreUsuario = a.NombreUsuario,
                Correo = a.Correo,
                Telefono = a.Telefono
        }).ToList();
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            var usuario = await _userRepository.GetByIdAsync(id);
            SaveUserViewModel vm = new();
            vm.Id = usuario.Id;
            vm.Nombre = usuario.Nombre;
            vm.NombreUsuario = usuario.NombreUsuario;
            vm.Correo = usuario.Correo;
            vm.Telefono = usuario.Telefono;

            return vm;

        }

        public async Task<UserViewModel> Login(LoginViewModel vm)
        {
            User users = await _userRepository.LoginAsync(vm);

            if (users == null)
                return null;

            UserViewModel uservm = new();
            uservm.Id = users.Id;
            uservm.Nombre = users.Nombre;
            uservm.NombreUsuario = users.NombreUsuario;
            uservm.Password = users.Password;
            uservm.Correo = users.Correo;
            uservm.Telefono = users.Telefono;

            return uservm;
        }

        public async Task Update(SaveUserViewModel vm)
        {
            User user = new();
            user.Id = vm.Id;
            user.Nombre = vm.Nombre;
            user.NombreUsuario = vm.NombreUsuario;
            user.Correo = vm.Correo;
            user.Telefono = vm.Telefono;

            await _userRepository.Update(user);
        }
    }
}
