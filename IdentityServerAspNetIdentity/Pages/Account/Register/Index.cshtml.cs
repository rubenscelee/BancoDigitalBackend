using Duende.IdentityServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace IdentityServerAspNetIdentity.Pages.Register
{
    [AllowAnonymous]
    public class Index : PageModel
    {
        [BindProperty]
        public registerInputModel Input { get; set; } = default!;

        public IActionResult OnGet(string? returnUrl = null)
        {
            return Page();
        }
    }
}
