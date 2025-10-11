using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkBabyApp.Services
{
    public interface IApiConnection
    {
        [Get("/login/")]
        Task<bool> Login();
    }
}
