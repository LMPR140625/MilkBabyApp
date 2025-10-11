using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkBabyApp.Models
{
    public class REGISTROALIMENTO
    {
        public REGISTROALIMENTO(int cantidad, string unidad, string fechaHora)
        {
            Cantidad = cantidad;
            Unidad = unidad;
            FechaHora = fechaHora;
        }

        public int IdAlimento { get; set; }
        public string Unidad { get; set; }
        public int Cantidad { get; set; }
        public string FechaHora { get; set; }
    }
}
