using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Bul.System.Common
{
    public class JwtBearer
    {
        private readonly IConfiguration _configuration;
        public JwtBearer(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string LoginToken<T>(T user, int expiresHour)
        {
            var iss = this._configuration.GetSection("Authentication:Issuer").Value;
            var adu = this._configuration.GetSection("Authentication:Audience").Value;

            var claims = new List<Claim> {
                //new Claim(JwtRegisteredClaimNames.Iss,iss),
                //new Claim(JwtRegisteredClaimNames.Aud,adu),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString("yyyyMMddHHmmss")),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.Sub, JsonConvert.SerializeObject(user))
            };

            var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration.GetSection("Authentication:Security").Value));
            var signing = new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(iss, adu, claims, DateTime.Now, DateTime.Now.AddHours(expiresHour), signing);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return token;
        }
    }
}
