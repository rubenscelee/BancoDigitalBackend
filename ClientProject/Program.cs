global using static System.Console;
using Duende.IdentityModel.Client;
using System.Text.Json;


#region Request de token do usuario para acessar API

string? name = String.Empty;
name ??= "Default Name";

Console.WriteLine(name); // Output: "Default Name"

#endregion

#region Request de token do Client para acessar api
// discover endpoints from metadata
var client = new HttpClient();

var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

if (disco.IsError)
{
    WriteLine(disco.Error);
    WriteLine(disco.Exception);
    return 1;
}

// request token
var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,
    ClientId = "client",
    ClientSecret = "secret",
    Scope = "digitalbankapi"
});


Console.WriteLine(tokenResponse.AccessToken);

// call api
var apiClient = new HttpClient();
apiClient.SetBearerToken(tokenResponse.AccessToken!); // AccessToken is always non-null when IsError is false

var response = await apiClient.GetAsync("https://localhost:6001/identity");

if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
    return 1;
}

var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));

return 0;

#endregion