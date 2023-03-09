using ApplicationLayer.Interfaces;
using DomainLayer.Entities;
using Infrastucture.Jwt;
using Infrastucture.Persistence.Context;
using Infrastucture.Repository.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Repository.Implementations
{
    public class LoginVmRepository : ILoginUser
    {
        private readonly ApplicationDbContext _context;
        private readonly Appsetting _jwt;

        public LoginVmRepository(ApplicationDbContext context,IOptions<Appsetting>jwt)
        {
            _context = context;
            _jwt = jwt.Value;
        }

        public RegisteredUsers Login(string email, string password)
        {
            var UserOfSameNameCheck = _context.registeredUsers.FirstOrDefault(u => u.email == email);
            if (UserOfSameNameCheck == null)
            {
                return null;
            }
            else if (UserOfSameNameCheck.approved == true)
            {
                var dPassoword = DomainLayer.Common.EncryptionDecryption.decrypt(UserOfSameNameCheck.password);
                var dConfirmPassword = DomainLayer.Common.EncryptionDecryption.decrypt(UserOfSameNameCheck.confirmPassword);
                UserOfSameNameCheck.password = dPassoword;
                UserOfSameNameCheck.confirmPassword = dConfirmPassword;

                //here generate token
                var TokenHandeler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwt.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name,UserOfSameNameCheck.Id.ToString()),
                    new Claim(ClaimTypes.Role,UserOfSameNameCheck.role)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                   , SecurityAlgorithms.HmacSha256Signature)

                };
                var token = TokenHandeler.CreateToken(tokenDescriptor);
                UserOfSameNameCheck.token = TokenHandeler.WriteToken(token);
                return UserOfSameNameCheck;
            }
            return null;
        }

   
    }
}
