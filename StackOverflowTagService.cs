using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Refit;
using Stack.Blazor.SignalR.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Stack.Blazor.SignalR
{
    public interface IStackOverflowTagsApi
    {
        [Get("/tags?pagesize={pageSize}&order=desc&sort=popular&site=stackoverflow")]
        Task<TagsResult> GetTags(int pageSize);
    }

    public class TagsResult
    {
        public List<Tag> Items { get; set; }
    }

    public class StackOverflowTagService : BackgroundService
    {
        private readonly IHubContext<StackOverflowTagsHub, IStackOverflowTagsClient> _hubContext;
        private readonly ILogger<StackOverflowTagService> _logger;
        private readonly IStackOverflowTagsApi _stackOverflowTagsApi;

        public StackOverflowTagService(IHubContext<StackOverflowTagsHub, IStackOverflowTagsClient> hubContext, 
            ILogger<StackOverflowTagService> logger)
        {
            _hubContext = hubContext;
            _logger = logger;

            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip
            };
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://api.stackexchange.com/2.2")
            };
            _stackOverflowTagsApi = RestService.For<IStackOverflowTagsApi>(client);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("StackOverflowTagService is starting.");

            stoppingToken.Register(() => _logger.LogInformation($"StackOverflowTagService is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = await _stackOverflowTagsApi.GetTags(5);
                    await _hubContext.Clients.All.NewTags(result.Items);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error retreiving tags.");
                }
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _logger.LogInformation("StackOverflowTagService is stopping.");
        }
    }
}
