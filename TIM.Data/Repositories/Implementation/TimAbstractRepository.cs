using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace TIM.Data.Repositories.Implementation
{
    public class TimAbstractRepository
    {
        protected  HttpClient _client;
        private const string baseUri = "http://localhost:1602/";

        public TimAbstractRepository()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseUri);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}