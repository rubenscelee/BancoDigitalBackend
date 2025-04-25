using Microsoft.AspNetCore.Authentication;
using Duende.AccessTokenManagement.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = "https://localhost:5001";

    options.ClientId = "web";
    options.ClientSecret = "secret";
    options.ResponseType = "code";

    options.Scope.Clear();
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("digitalbankapi");
    options.Scope.Add("verification");
    //options.Scope.Add("offline_access");
    //options.ClaimActions.MapJsonKey("email_verified", "email_verified");
    //options.ClaimActions.MapJsonKey("address", "address");


    options.GetClaimsFromUserInfoEndpoint = true;

    options.MapInboundClaims = false; // Don't rename claim typesalice

    options.CallbackPath = "/signin-oidc"; // 👈 Ensure this is set

    options.SaveTokens = true;
});

builder.Services.AddOpenIdConnectAccessTokenManagement();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

app.Run();
