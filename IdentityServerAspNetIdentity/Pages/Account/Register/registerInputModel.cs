﻿namespace IdentityServerAspNetIdentity.Pages.Register
{
    public class registerInputModel
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
