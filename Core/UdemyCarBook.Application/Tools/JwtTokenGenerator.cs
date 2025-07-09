using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Dtos;
using UdemyCarBook.Application.Features.Mediator.Results.AppUserResults;

namespace UdemyCarBook.Application.Tools
{
    public class JwtTokenGenerator
    {
        public static TokenResponseDto GenerateToken(GetCheckAppUserQueryResult result)
        {
            // Token içerisine eklenecek talepler (claims) listesi oluşturuluyor
            var claims = new List<Claim>();

            // Kullanıcının rolü varsa, role claim olarak ekleniyor
            if (!string.IsNullOrWhiteSpace(result.Role))
                claims.Add(new Claim(ClaimTypes.Role, result.Role));

            // Kullanıcının ID'si claim olarak ekleniyor
            claims.Add(new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()));

            // Kullanıcının kullanıcı adı varsa, ekleniyor
            if (!string.IsNullOrWhiteSpace(result.Username))
                claims.Add(new Claim("Username", result.Username));

            // JWT için şifreleme anahtarı oluşturuluyor
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));

            // Anahtar ve algoritma kullanılarak imzalama bilgisi oluşturuluyor
            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Token'ın geçerlilik süresi ayarlanıyor (örneğin: 1 gün)
            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);

            // JWT token'ı oluşturuluyor
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: JwtTokenDefaults.ValidIssuer,       // Token'ı oluşturan
                audience: JwtTokenDefaults.ValidAudience,   // Token'ın hedef kitlesi
                claims: claims,                              // Eklenen talepler (claims)
                notBefore: DateTime.UtcNow,                 // Bu zamandan önce geçerli değil
                expires: expireDate,                        // Geçerlilik süresi
                signingCredentials: signinCredentials       // İmzalama bilgisi
            );

            // Token'ı yazmak (string haline getirmek) için handler oluşturuluyor
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // Token string olarak oluşturuluyor ve döndürülüyor
            return new TokenResponseDto(tokenHandler.WriteToken(token), expireDate);
        }

    }
}
