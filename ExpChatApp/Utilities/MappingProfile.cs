using AutoMapper;
using ExpChatApp.Entity;
using ExpChatApp.Models;

namespace ExpChatApp.Utilities
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            
        CreateMap< RegisterDto, User>();
        }
    }
}
