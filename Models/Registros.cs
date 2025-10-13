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
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }
        public string Unidad { get; set; }
        public int Cantidad { get; set; }
        public string DiaHora { get; set; }
    }
}
