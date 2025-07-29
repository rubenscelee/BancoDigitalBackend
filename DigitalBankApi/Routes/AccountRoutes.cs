using DigitalBankApi.DTOs;
using DigitalBankApi.Repositories.BankTransferPixRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DigitalBankApi.Routes
{
    public static class AccountRoutes
    {
        public static RouteGroupBuilder GroupAccountRoutes(this RouteGroupBuilder group)
        {
            group.MapPost("/transferenciaPix", async ([FromBody] BankTransferPixDto bankTransferPixRequest,
                [FromServices] IBankTransferPixRepository bankTransferPixRepository,
                ILogger<Program> logger) => {

                    logger.LogInformation("Iniciando transferência via Pix");

                    await bankTransferPixRepository.MakeTransferPix(bankTransferPixRequest);

                    return Results.Ok(/*createdUser*/);
                })    
            .RequireAuthorization("ApiScope");

            group.MapGet("/transferenciaPix", async (ClaimsPrincipal user,
                [FromServices] IBankTransferPixRepository bankTransferPixRepository,
                ILogger<Program> logger) => {

                    logger.LogInformation("Iniciando lista de transferências via Pix");

                    var userEmail = user.FindFirst(c => c.Type == "scope" || c.Type == "scp")?.Value!;

                    var transferencias = await bankTransferPixRepository.ListTransferPix(userEmail);

                    return Results.Ok(transferencias);
                })
            .RequireAuthorization("ApiScope");

            return group;
        }
    }
}
