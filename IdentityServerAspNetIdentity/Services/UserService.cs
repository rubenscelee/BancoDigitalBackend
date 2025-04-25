using IdentityModel.Client;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace IdentityServerAspNetIdentity.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserService> _logger;

        public UserService(HttpClient httpClient, ILogger<UserService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task CriarUserApi(User user)
        {
            var disco = await _httpClient.GetDiscoveryDocumentAsync("https://localhost:5001");

            if (disco.IsError)
            {
                _logger.LogError($"Token request error: {disco.Error}");
                return;
            }

            // request token
            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "digitalbankapi"
            });

            if (tokenResponse.IsError)
            {
                _logger.LogError($"Token request error: {tokenResponse.Error}");
                return;
            }

            string userJson = JsonSerializer.Serialize(user);
            var content = new StringContent(userJson, Encoding.UTF8, "application/json");

            // call api
            _httpClient.SetBearerToken(tokenResponse.AccessToken!);

            var response = await _httpClient.PostAsync("https://localhost:6001/createUser", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"API request failed: {response.StatusCode}");
                return;
            }

            _logger.LogInformation("User created successfully.");
        }
    }
}
