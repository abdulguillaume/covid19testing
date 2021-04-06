using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Covid19Testing.Utils
{
    public class TestsApi
    {
        IConfiguration _configuration;
        public TestsApi(IConfiguration configuration)
        {
            //Initial();
            _configuration = configuration;
        }

        public HttpClient Initial()
        {
            
            HttpClient client = new HttpClient();
            string endpoint = //"https://localhost:44353/api/results"; 
                _configuration.GetConnectionString("Covid19TestingServer");
                                                                    //"Covid19TestingServer": "https://localhost:44353/api/results"
            client.BaseAddress = new Uri(endpoint);
            return client;
        }

    }

    
}
