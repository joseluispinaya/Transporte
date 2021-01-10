using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporte.Web.Data.Entities;

namespace Transporte.Web.Models
{
    public class VehiculoViewModel : Vehiculo
    {
        public int AfiliadoId { get; set; }
    }
}
