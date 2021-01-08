using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transporte.Web.Data.Entities
{
    public class Vehiculo
    {
        public int Id { get; set; }

        [MaxLength(8, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres.")]
        [Required]
        [Display(Name = "Num Placa")]
        public string Nroplaca { get; set; }

        [MaxLength(20, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres.")]
        [Required]
        [Display(Name = "Num Chasis")]
        public string Nrochasis { get; set; }

        [MaxLength(20, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres.")]
        [Required]
        [Display(Name = "Num Motor")]
        public string Nromotor { get; set; }

        [MaxLength(20, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres.")]
        [Required]
        [Display(Name = "Marca")]
        public string Marca { get; set; }

        public Afiliado Afiliado { get; set; }
    }
}
