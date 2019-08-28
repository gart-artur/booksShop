using Library.BusinessLogic.Helper;
using Library.BusinessLogic.Services.Interfaces;
using Library.DataAccess.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Library.DataAccess.Repository;
using Library.DataAccess.Interfaces;
using AutoMapper;

namespace Library.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }

        //public void Delete(User user)
        //{
        //    _userRepository.Delete(user);
        //}


        private readonly AppSettings _appSettings;
        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }


        public User Authenticate(User user)
        {
            var userModel = _userRepository.Authenticate(user.Login, user.Password);


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return (userModel);
        }


        //public IEnumerable<User> GetAll()
        //{

        //    IEnumerable<User> user = _userRepository.GetAll();
        //    return (user);

        //}

        public User Create(User user)
        {
                 
            _userRepository.Create(user);
            return null;

        }
    }
}
