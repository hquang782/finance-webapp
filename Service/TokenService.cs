using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
    public class TokenService : ITokenService
    {
        // IConfiguration interface dùng để truy cập các cài đặt cấu hình
        private readonly IConfiguration _configuration;

        // SymmetricSecurityKey dùng để ký token JWT
        private readonly SymmetricSecurityKey _key;

        // Constructor khởi tạo đối tượng configuration và symmetric security key

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            // Lấy giá trị key từ cài đặt cấu hình
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
        }

        // Phương thức tạo token cho người dùng
        public string CreateToken(AppUser user)
        {
            // Khai báo các claims cho token
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
            };

            // Khai báo credentials dùng để ký token
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            // Khai báo cấu hình cho token bao gồm các claims, thời hạn và thông tin ký
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
            };

            // Tạo token sử dụng JwtSecurityTokenHandler
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Trả về chuỗi token
            return tokenHandler.WriteToken(token);
        }
    }
}
