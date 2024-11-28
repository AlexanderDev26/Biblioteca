using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Token
{
    public static class JwtTokenHelper
    {
        private const string SecretKey = "EstaEsUnaClaveSuperSecretaDePrueba12345"; // Cambiar por una clave más robusta
        private const int TokenExpirationMinutes = 60; // Duración del token en minutos

        private static readonly SymmetricSecurityKey _signingKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        // Método para generar un token JWT
        public static string GenerateToken(string email, string role, int userId)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // ID único del token
                new Claim("UserId", userId.ToString()) // ID del usuario (opcional)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(TokenExpirationMinutes),
                SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        // Método para validar un token JWT
        public static ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false, // Si tienes un emisor específico, configúralo
                ValidateAudience = false, // Si tienes un público específico, configúralo
                ValidateLifetime = true, // Verificar si el token ha expirado
                IssuerSigningKey = _signingKey,
                ValidateIssuerSigningKey = true
            };

            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                return principal; // Si el token es válido, se devuelve el usuario autenticado
            }
            catch
            {
                return null; // Si el token no es válido
            }
        }
    }
}
