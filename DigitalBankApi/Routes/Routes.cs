using DigitalBankApi.Models;
using DigitalBankApi.Repositories.BankTransferPixRepositories;
using DigitalBankApi.Repositories.UserRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DigitalBankApi.DTOs;

namespace DigitalBankApi.Routes
{
    public static class Routes
    {
        // Middleware Extension Method
        public static void CallRoutes(WebApplication app)
        {
            app.MapGet("identity", (ClaimsPrincipal user) => user.Claims.Select(c => new { c.Type, c.Value }))
                .RequireAuthorization("ApiScope");

            app.MapGet("userInfo", (ClaimsPrincipal user) =>
            {
                // Extract the 'scope' claim and check for 'verification'
                var userEmail = user.FindFirst(c => c.Type == "scope" || c.Type == "scp")?.Value;

                return Results.Ok(userEmail); // Return the user info as JSON
            })
            .RequireAuthorization("ApiScope"); // Ensure the user is authorized

            app.MapPost("/createUser", async ([FromBody] User user, [FromServices] IUserRepository userRepository, ILogger<Program> logger) => {
                logger.LogInformation("Creating user: {Email}", user.Email);

                var createdUser = await userRepository.CreateUser(user);

                if (createdUser is null || !createdUser.Success)
                    return Results.BadRequest(createdUser?.Message ?? "Erro ao criar um novo usuário.");

                return Results.Ok(createdUser);
            })
            .RequireAuthorization("ApiScope");

            app.MapPost("/transferenciaPix", async ([FromBody] BankTransferPixDto bankTransferPixRequest, 
                [FromServices] IBankTransferPixRepository bankTransferPixRepository, 
                ILogger<Program> logger) => {

                    logger.LogInformation("Iniciando transferência via Pix");

                    await bankTransferPixRepository.MakeTransferPix(bankTransferPixRequest);

                    return Results.Ok(/*createdUser*/);
            })
            .RequireAuthorization("ApiScope");

        }
    }
}
