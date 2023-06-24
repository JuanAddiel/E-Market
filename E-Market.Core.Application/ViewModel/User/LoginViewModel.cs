using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModel.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Debe ingresar el usuario")]
        [DataType(DataType.Text)]
        public string NombreUsuario { get; set; }
        [Required(ErrorMessage = "Debe ingresar la contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
