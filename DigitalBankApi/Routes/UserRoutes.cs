using DigitalBankApi.DTOs;
using DigitalBankApi.Models;
using DigitalBankApi.Repositories.BankTransferPixRepositories;
using DigitalBankApi.Repositories.UserRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalBankApi.Routes
{
    public static class UserRoutes
    {
        public static RouteGroupBuilder GroupUserRoutes(this RouteGroupBuilder group)
        {
            
            group.MapGet("/identity", (ClaimsPrincipal user) => user.Claims.Select(c => new { c.Type, c.Value }))
                .RequireAuthorization("ApiScope");

            group.MapGet("/userInfo", (ClaimsPrincipal user) =>
            {
                // Extract the 'scope' claim and check for 'verification'
                //var userEmail = user.FindFirst(c => c.Type == "scope" || c.Type == "scp")?.Value;
                var userEmail = user.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

                return Results.Ok(userEmail); // Return the user info as JSON
            })
            .RequireAuthorization("ApiScope"); // Ensure the user is authorized

            group.MapPost("/", async ([FromBody] User user, [FromServices] IUserRepository userRepository, ILogger<Program> logger) => {
                logger.LogInformation("Creating user: {Email}", user.Email);

                var createdUser = await userRepository.CreateUser(user);

                if (createdUser is null || !createdUser.Success)
                    return Results.BadRequest(createdUser?.Message ?? "Erro ao criar um novo usuário.");

                return Results.Ok(createdUser);
            })
            .RequireAuthorization("ApiScope");

            return group;
        }
    }
}
