// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace IdentityServerAspNetIdentity.Pages.Login;

public class InputModel
{
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    public bool RememberLogin { get; set; }
    public string? Button { get; set; }
}

