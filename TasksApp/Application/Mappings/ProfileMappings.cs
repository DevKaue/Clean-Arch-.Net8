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
                .ForMember(x => x.RefreshToken, x => x.MapFrom(x => GenerateGuid()))
                .ForMember(x => x.RefreshTokenExpirationTime, x => x.MapFrom(x => AddFiveDays()))
                .ForMember(x => x.PasswordHash, x => x.MapFrom(x => x.Password));

            CreateMap<User, UserInfoViewModel>()
                .ForMember(x => x.TokenJWT, x => x.AllowNull());

        }

        private string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
        private DateTime AddFiveDays()
        {  
            return DateTime.Now.AddDays(5); 
        }
    }
}
