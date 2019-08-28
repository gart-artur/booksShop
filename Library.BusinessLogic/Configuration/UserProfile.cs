using AutoMapper;
using Library.BusinessLogic.Services.ViewModel.Account.Post;
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
            CreateMap<RegisterUserViewModel, User>()
                .ForMember(x => x.Token, opt => opt.Ignore())
                .ForMember(x => x.DateModified, opt => opt.Ignore())
                .ForMember(x => x.DateCreated, opt => opt.Ignore());

        }
            
    }
}
