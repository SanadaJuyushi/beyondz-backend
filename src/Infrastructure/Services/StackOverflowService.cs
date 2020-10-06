using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class StackOverflowService : IStackOverflowService
    {

        private readonly HttpClient _httpClient;

        public StackOverflowService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ApiTagItem>> GetStackOverflowTagsAsync()
        {
            var items = new List<ApiTagItem>();

            for (var i = 0; i < 10; i++)
            {
                var reqPram = $"tags?page={i + 1}&pagesize=100&order=desc&sort=popular&site=stackoverflow";

                // Create HTTP Request
                var request = new HttpRequestMessage(HttpMethod.Get, reqPram);

                // Get API Data
                var response = await _httpClient.SendAsync(request);

                // Convert Data format
                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    var options = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    var apiRoot = await JsonSerializer.DeserializeAsync<ApiTagRoot>(responseStream, options);
                    apiRoot.Items.ForEach(async x =>
                    {
                        x.Excerpt = await GetTagExcerpt(x.Name);
                        items.Add(x);
                    });
                }
            }

            return items;
        }

        private async Task<string> GetTagExcerpt(string name)
        {
            var reqPram = $"tags/{name}/wikis?site=stackoverflow";
            var request = new HttpRequestMessage(HttpMethod.Get, reqPram);
            var response = await _httpClient.SendAsync(request);
            var excerpt = "";
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var apiRoot = await JsonSerializer.DeserializeAsync<ApiTagRoot>(responseStream, options);
                excerpt = apiRoot.Items.FirstOrDefault().Excerpt;
            }

            return excerpt;

        }
    }
}
