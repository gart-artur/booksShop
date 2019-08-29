using Library.BusinessLogic.Helper;
using Library.BusinessLogic.Services.Interfaces;
using Library.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using  Microsoft.Extensions.Options;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Library.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings; 

        private readonly List<User> _users = new List<User>
        {
            new User {Id = 1, Login ="admin" , Password = "string", FirstName = "Alex" , SecondName = "Alex"}
        };
        public UserService(IOptions<AppSettings> appsettings)
        {
            _appSettings = appsettings.Value;

        }
        public User Authenticate(string login, string password)
        {
            var user = _users.SingleOrDefault(x=>x.Login == login &&  x.Password == password);
            if (user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;


        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
