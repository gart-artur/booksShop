using Library.BusinessLogic.Helper;
using Library.BusinessLogic.Services.Interfaces;
using Library.DataAccess.Entities;
using Library.DataAccess.Interfaces;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogic.Services
{
    public class ElasticsearchService : IElasticsearchService
    {
        private readonly ElasticClient _elasticClient;
        private readonly IBookRepository _bookRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOptions<ElasticsearchOption> _options; 

        public ElasticsearchService(IEsClientProvider clientProvider,
            IBookRepository bookRepository,
            IOrderRepository orderRepository,
            IOptions<ElasticsearchOption> options)
        {
            _elasticClient = clientProvider.GetClient();
            _bookRepository = bookRepository;
            _orderRepository = orderRepository;
            _options = options;
        }

        public async Task Run()
        {
            await IndexBooksPage();
            await IndexOrdersPage();
        }

        private  async Task<BulkResponse> IndexBooksPage()
        {
            await _elasticClient.DeleteByQueryAsync<Book>(q => q.MatchAll());

            var allRecords = _bookRepository.GetAll();

            BulkResponse indexManyResponse = await _elasticClient.IndexManyAsync(allRecords);

            if (!indexManyResponse.IsValid)
            {
                var exeption = indexManyResponse.OriginalException;
                var debugExeption = indexManyResponse.DebugInformation;
            }

            return indexManyResponse;
        }

        private  async Task<BulkResponse> IndexOrdersPage()
        {
            await _elasticClient.DeleteByQueryAsync<Order>(q => q.MatchAll());

            var allRecords = _orderRepository.GetAll();

            BulkResponse indexManyResponse = await _elasticClient.IndexManyAsync(allRecords);

            if (!indexManyResponse.IsValid)
            {
                var exeption = indexManyResponse.OriginalException;
                var debugExeption = indexManyResponse.DebugInformation;
            }

            return indexManyResponse;
        }

    }
}
