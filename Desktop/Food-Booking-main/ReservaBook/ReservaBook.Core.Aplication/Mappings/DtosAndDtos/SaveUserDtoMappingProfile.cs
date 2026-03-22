

using AutoMapper;
using ReservaBook.Core.Aplication.Dtos.User;

namespace ReservaBook.Core.Aplication.Mappings.DtosAndDtos
{
    public class SaveUserDtoMappingProfile: Profile
    {

        public SaveUserDtoMappingProfile()
        {


            CreateMap<SaveUserDto, CreateUserDto>()
               .ReverseMap()
               .ForMember(dest => dest.ProfileImage, opt => opt.Ignore())
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForMember(dest => dest.Role,opt => opt.MapFrom(src => src.Role.ToString()));
        
        
        
        
        }


    }
}
