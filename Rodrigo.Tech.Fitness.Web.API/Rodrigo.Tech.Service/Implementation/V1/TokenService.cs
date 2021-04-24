using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Rodrigo.Tech.Model.Constants;
using Rodrigo.Tech.Repository.Tables.Context;
using Rodrigo.Tech.Service.Interface.V1;

namespace Rodrigo.Tech.Service.Implementation.V1
{
    public class TokenService : ITokenService
    {
        private readonly ILogger _logger;

        public TokenService(ILogger<TokenService> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public string CreateJWTToken(User user)
        {
            _logger.LogInformation($"{nameof(TokenService)} - {nameof(CreateJWTToken)} - Started, " +
                $"{nameof(user)}: {JsonConvert.SerializeObject(user)}");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable(EnvironmentConstants.JWT_SECRET));

            Claim[] claims = new Claim[]
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = Environment.GetEnvironmentVariable(EnvironmentConstants.JWT_CLIENTID),
                Issuer = Environment.GetEnvironmentVariable(EnvironmentConstants.JWT_CLIENTID),
                IssuedAt = DateTime.UtcNow,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            _logger.LogInformation($"{nameof(TokenService)} - {nameof(CreateJWTToken)} - Finished, " +
                $"{nameof(user)}: {JsonConvert.SerializeObject(user)}");
            return tokenHandler.WriteToken(jwtToken);
        }

        /// <inheritdoc/>
        public bool ValidateJWTToken(string jwt)
        {
            throw new System.NotImplementedException();
        }
    }
}