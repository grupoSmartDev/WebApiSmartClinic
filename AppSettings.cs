﻿namespace WebApiSmartClinic;

public class AppSettings
{
    public string? UserProfileImageSizeMb { get; set; }
    public string? UserDocumentsPath { get; set; }
    public string? JwtSecretKey { get; set; }
    public int JwtExpiresHours { get; set; }
    public string? JwtIssuer { get; set; }
    public string? JwtAudience { get; set; }
}
