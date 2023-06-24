using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Market.Core.Application.ViewModel.User
{
    public class SaveUserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es requerido")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El Apellido es requerido")]
        [DataType(DataType.Text)]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El Correo es requerido")]
        [DataType(DataType.Text)]
        public string Correo { get; set; }
        [Required(ErrorMessage = "El Telefono es requerido")]
        [DataType(DataType.Text)]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "La contraseña es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password),ErrorMessage ="Las contraseñas no coinciden")]
        [Required(ErrorMessage = "Confirmar la contraseña es requerido")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        [DataType(DataType.Text)]
        public string NombreUsuario { get; set;}
    }
}
