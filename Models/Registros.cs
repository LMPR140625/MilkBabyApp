
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkBabyApp.Models
{
    public class Registros
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public int IdRegistro { get; set; }
        public string Unidad { get; set; }
        public int Cantidad { get; set; }
        public string HoraMinutos { get; set; }
        public int Dia { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public string DateRecord { get; set; }
    }
}
