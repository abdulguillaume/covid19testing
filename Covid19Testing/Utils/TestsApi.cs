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
        string _endpoint = "";
        public TestsApi(IConfiguration configuration)
        {
            //Initial();
            //_configuration = configuration;
            _endpoint = configuration.GetConnectionString("Covid19TestingServer");

        }

        public HttpClient Initial()
        {
            
            HttpClient client = new HttpClient();           
                                                                    //"Covid19TestingServer": "https://localhost:44353/api/results"
            client.BaseAddress = new Uri(_endpoint);
            return client;
        }

        public string getEnpoint()
        {
            return _endpoint;
        }

    }

    
}
