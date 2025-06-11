using Application.UserCQ.Commands;
using Application.UserCQ.ViewModels;
using AutoMapper;
using Domain.Entity;

namespace Application.Mappings
{
    public  class ProfileMappings : Profile
    {
        public ProfileMappings()
        {
            CreateMap<CreateUserCommand, User>()
                .ForMember(x => x.RefreshToken, x => x.AllowNull())
                .ForMember(x => x.RefreshTokenExpirationTime, x => x.MapFrom(x => AddTwoDays()))
                .ForMember(x => x.PasswordHash, x => x.AllowNull());

        }

        private DateTime AddTwoDays()
        {  
            return DateTime.Now.AddDays(2); 
        }
    }
}
