using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkBabyApp.Services
{
    public class ApiConnection : IApiConnection
    {
        private readonly IApiConnection _apiService;
        public ApiConnection() 
        {
            var url = "https//jsonplaceholder.typecode.com";
            _apiService = RestService.For<IApiConnection>(url);
        }
        public async Task<bool> Login()
        {
            return await _apiService.Login();
        }
    }
}
