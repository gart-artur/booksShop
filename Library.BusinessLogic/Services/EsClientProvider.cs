using Library.BusinessLogic.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BusinessLogic.Services
{
    public class EsClientProvider : IEsClientProvider
    {
        private readonly IConfiguration _configuration;
        private ElasticClient _client;

        public EsClientProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ElasticClient GetClient()
        {
            if (_client != null)
            {
                return _client;
            }

            var settings = new ConnectionSettings(new Uri(_configuration["ESUri"])).DefaultIndex("item");
            _client = new ElasticClient(settings);

            return _client;
        }

    }
}

