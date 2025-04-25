// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Test;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models.UserModels;
using System.ComponentModel.DataAnnotations;
using IdentityServerAspNetIdentity.Models;
using IdentityServerAspNetIdentity.Services;

namespace IdentityServerAspNetIdentity.Pages.Create;

[SecurityHeaders]
[AllowAnonymous]
public class RegisterModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IUserStore<IdentityUser> _userStore;
    private readonly ILogger<RegisterModel> _logger;
    private readonly UserService _userService;

    public RegisterModel(
            UserManager<IdentityUser> userManager,
            IUserStore<IdentityUser> userStore,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            UserService userService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userStore = userStore;
        _logger = logger;
        _userService = userService;
    }

    [BindProperty]
    public InputModel? Input { get; set; }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var user = new IdentityUser { UserName = Input!.Email, Email = Input!.Email };
        var result = await _userManager.CreateAsync(user, Input.Password);

        if (result.Succeeded)
        {
            User userModel = new User
            {
                Email = Input!.Email,
                IsEnabled = true
            };

            await _userService.CriarUserApi(userModel);

            return RedirectToPage("/Account/Login/Index");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return Page();
    }
}
