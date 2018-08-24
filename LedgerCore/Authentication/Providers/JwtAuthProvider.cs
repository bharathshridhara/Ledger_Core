using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LedgerCore.Data.Entities;
using Microsoft.IdentityModel.Tokens;


namespace LedgerCore.Authentication.Providers
{
    public class JwtAuthProvider //: IAuthProvider
    {
        private string secret = "Mba287xd!";


        public string GenerateNewToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomClaimType.USER_EMAIL, user.Email),
                new Claim(CustomClaimType.USER_ID, user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
