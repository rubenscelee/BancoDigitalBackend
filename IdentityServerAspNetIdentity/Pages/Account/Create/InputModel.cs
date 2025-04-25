// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace IdentityServerAspNetIdentity.Pages.Create;

public class InputModel
{
    public string Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }

    public string? ReturnUrl { get; set; }

    public string? Button { get; set; }
}