using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transporte.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [MaxLength(10, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres.")]
        [Required]
        [Display(Name = "Nro C.I.")]
        public string NroDocumento { get; set; }


        [MaxLength(50, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres.")]
        [Required]
        [Display(Name = "Nombres")]
        public string Nombres { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres.")]
        [Required]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }
        //public string Apematerno { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres.")]
        [Required]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        

        //[MaxLength(10, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres.")]
        //[Required]
        //[Display(Name = "Nro Celular")]
        //public string Celular { get; set; }

        public string FullName => $"{Nombres} {Apellidos}";
    }
}
