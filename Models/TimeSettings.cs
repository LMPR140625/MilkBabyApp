using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkBabyApp.Models
{
    public class TimeSettings
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }
        public int TimeInterval { get; set; }
    }
}
