using AutoMapper;
using ChatApp.DTOs;
using ChatApp.Entities;

namespace ChatApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserReadByAdminDTO, User>().ReverseMap();
            CreateMap<UserCreateByAdminDTO, User>().ReverseMap();
            CreateMap<UserUpdateByAdminDTO, User>().ReverseMap();
            CreateMap<UserUpdateDTO, User>().ReverseMap();
            CreateMap<UserReadDTO, User>().ReverseMap();

            CreateMap<Conversation, ConversationReadDTO>();
            CreateMap<ConversationCreateDTO, Conversation>();
            CreateMap<ConversationReadDTO, ConversationCreateDTO>();
        }
    }
}
