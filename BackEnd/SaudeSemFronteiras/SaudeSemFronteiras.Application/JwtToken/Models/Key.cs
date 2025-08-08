﻿namespace SaudeSemFronteiras.Application.JwtToken.Models;
public static class Key
{
    public static string? Secret { get; private set; }

    public static void SetSecret(string? secret)
    {
        Secret = secret;
    }
}
