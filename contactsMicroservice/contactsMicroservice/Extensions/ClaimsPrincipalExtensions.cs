﻿using ContactsMicroservice.Objects;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text.Json;

namespace ContactsMicroservice.Extensions
{
    public static class ClaimsPrincipalExtensions
    {   
        //metoda zwracajaca role klienta api
        public static List<string>? GetApiClientRoles(this ClaimsPrincipal claimsPrincipal)
        {
            var resourceAccessClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "resource_access")?.Value;
            if (resourceAccessClaim is null) return null;
            var deserializedJson = JsonSerializer.Deserialize<ResourceAccessClaimValue>(resourceAccessClaim);
            return deserializedJson?.ApiClient?.Roles;
        }
    }
}
