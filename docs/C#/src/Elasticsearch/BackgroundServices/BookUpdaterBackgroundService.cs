using System;
using System.Threading;
using System.Threading.Tasks;
using Elasticsearch.Models;
using Microsoft.Extensions.Hosting;
using Nest;

namespace Elasticsearch.BackgroundServices
{
    public class BookUpdaterBackgroundService : BackgroundService
    {
        private readonly IElasticClient _client;
        private int _pageCounter;

        public BookUpdaterBackgroundService(IElasticClient client)
        {
            _client = client;
            _pageCounter = 1;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            if (_client.Indices.Exists("books").Exists)
            {
                Console.WriteLine("Deleting books index");
                _client.Indices.Delete("books");
            }
            
            var createIndexResponse = _client.Indices.Create("books", c => c
                        .Map<Book>(b => b.AutoMap()));

            var bookId = Guid.NewGuid().ToString();

            while (true)
            {
                Book book = new()
                {
                    BookId = bookId,
                    Title = "My Popular Book",
                    PageCount = _pageCounter
                };
                
                Console.WriteLine($"Book: '{book.Title}' now has {book.PageCount} pages");

                _client.IndexDocument(book);
                _client.bu
                

                _pageCounter++;
                await Task.Delay(TimeSpan.FromSeconds(2));
            }
            
        }
    }
}