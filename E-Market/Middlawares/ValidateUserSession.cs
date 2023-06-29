using E_Market.Core.Application.ViewModel.User;
using E_Market.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace E_Market.Middlawares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public bool hasUser()
        {
            UserViewModel usersViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            if (usersViewModel == null)
            {
                return false;
            }
                return true;

        }
    }
}
