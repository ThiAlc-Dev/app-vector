using Api.Domain.Models;
using Domain.Interfaces;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;

namespace Service.Services
{
    public class EmailService : IEmailService
    {
        private static IUserService _service;
        private readonly IHttpClientFactory _clientFactory;
        public IEnumerable<Email> _email { get; private set; }
        public EmailService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<string>> Get()
        {
            const string restUrl = "https://6064ac2bf09197001778660d.mockapi.io/api/test-api";
            var request = new HttpRequestMessage(HttpMethod.Get, restUrl);

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                _email = await JsonSerializer.DeserializeAsync<List<Email>>(responseStream);

                var listEmails = new List<string>();

                foreach (var item in _email)
                {
                    listEmails.Add(item.mail);
                }

                return listEmails;
            }
            else
            {
                return null;
            }
        }
    }
}
