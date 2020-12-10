using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SampleApiCoreCqrs.Application.Common.Library
{
    public class Functions
    {
        public static string GenerateToken(object entity, DateTime dateExpiration, string key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        //new Claim(ClaimTypes.Name, entity.FirstName),
                        //new Claim(ClaimTypes.Role, entity.Type.ToString())
                    }),
                Expires = dateExpiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
