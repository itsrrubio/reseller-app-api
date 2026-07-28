using Microsoft.Extensions.Options;
using ResellerApp.Api.Configuration;
using ResellerApp.Api.Interfaces;
using ResellerApp.Api.Models.Ebay;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ResellerApp.Api.Services
{
    public class EbayService : IEbayService
    {
        private readonly HttpClient _httpClient;
        private readonly EbayOptions _options;

        public EbayService(
            HttpClient httpClient,
            IOptions<EbayOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        public async Task<EbayTokenResponse> GetApplicationTokenAsync()
        {
            var credentials = $"{_options.ClientId}:{_options.ClientSecret}";

            var encodedCredentials = Convert.ToBase64String(
                Encoding.UTF8.GetBytes(credentials));

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", encodedCredentials);

            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>(
                    "grant_type",
                    "client_credentials"),

                new KeyValuePair<string, string>(
                    "scope",
                    "https://api.ebay.com/oauth/api_scope")
            });

            var response = await _httpClient.PostAsync(
                $"{_options.BaseUrl}/identity/v1/oauth2/token",
                content);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var token = JsonSerializer.Deserialize<EbayTokenResponse>(json);

            if (token == null)
                throw new Exception("Unable to deserialize eBay OAuth response.");

            return token;
        }
    }
}