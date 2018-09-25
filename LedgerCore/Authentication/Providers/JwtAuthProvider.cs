using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
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
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Email, "TokenAuth"),
                new[] {
                    new Claim("User_Id", user.Id.ToString())
                }
            );

            var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = identity,
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddMilliseconds(30),
                Issuer = "Issuer",
                Audience = "Audience"
            });
            
            return handler.WriteToken(token);
        }

    }
}
