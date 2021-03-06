﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Transporte.Web.Data.Entities
{
    public class Sindicato
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres.")]
        [Required]
        [Display(Name = "Nom Sindicato")]
        public string Nomsindica { get; set; }

        [MaxLength(50, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres.")]
        [Required]
        [Display(Name = "Responsable")]
        public string Responsable { get; set; }

        [MaxLength(100, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres.")]
        [Required]
        [Display(Name = "Ubicacion")]
        public string Ubicacion { get; set; }

        [Display(Name = "Fundacion")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Fechafundacion { get; set; }

        [MaxLength(10, ErrorMessage = "El campo {0} debe contener menos de {1} caracteres.")]
        [Required]
        [Display(Name = "Nro Celular")]
        public string Celular { get; set; }

        public ICollection<Afiliado> Afiliados { get; set; }

        [DisplayName("Numero de Afiliados")]
        public int NumAfiliados => Afiliados == null ? 0 : Afiliados.Count;
    }
}
