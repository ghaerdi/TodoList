using todolist.DTOs;
using AutoMapper;

namespace todolist.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserEntity, UserRegisterDTO>().ReverseMap();
        CreateMap<UserEntity, UserLoginDTO>().ReverseMap();

        CreateMap<TaskEntity, TaskCreateDTO>().ReverseMap();
        CreateMap<TaskEntity, TaskUpdateDTO>().ReverseMap();
        CreateMap<TaskEntity, TaskInfoDTO>().ReverseMap();
    }
}
