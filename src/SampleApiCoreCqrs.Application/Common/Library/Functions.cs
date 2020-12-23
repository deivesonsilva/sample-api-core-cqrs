using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SampleApiCoreCqrs.Infrastructure.Entities;

namespace SampleApiCoreCqrs.Application.Common.Library
{
    public static class Functions
    {
        public static string GenerateCode()
        {
            int length = 6;
            string keys = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz";
            string result = string.Empty;
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(0, 62);
                string code = keys.Substring(index, 1);

                if (!result.Contains(code))
                    result += code;
                else
                    i--;
            }
            return result;
        }

        public static string GenerateMd5(this string input)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));

            return sBuilder.ToString();
        }

        public static string GenerateToken(Account entity, DateTime dateExpiration, string key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Hash, entity.Id.ToString()),
                        new Claim(ClaimTypes.Name, entity.FirstName),
                        new Claim(ClaimTypes.Role, entity.Type.ToString())
                    }),
                Expires = dateExpiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }
    }
}
