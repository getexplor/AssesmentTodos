﻿using AssesmentTodo.Application;
using AssesmentTodo.Application.Features;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.Infrastructure.Authentications
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOption;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _jwtOption = options.Value;
        }

        public async Task<string> GenerateToken(UserModel user)
        {
            var claims = new Claim[]
            {
                new(ClaimConstant.IdUser, user.IdUser.ToString()),
                new(ClaimConstant.UserName, user.UserName!),
                new(ClaimConstant.FullName, user.FullName!),
            };

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOption.SecretKey!)),
                SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                _jwtOption.Issuer,
                _jwtOption.Audience,
                claims,
                null,
                DateTime.UtcNow.AddDays(7),
                credentials
                );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
