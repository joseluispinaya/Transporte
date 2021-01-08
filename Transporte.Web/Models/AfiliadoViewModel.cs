using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Transporte.Web.Data.Entities;

namespace Transporte.Web.Models
{
    public class AfiliadoViewModel : Afiliado
    {
        public int SindicatoId { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
