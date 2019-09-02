using AutoMapper;
using Library.BusinessLogic.Services.ViewModel.Users.GET;
using Library.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BusinessLogic.Configuration
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserViewModel, User>();
        }
    }
}
