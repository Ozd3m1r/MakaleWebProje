using AutoMapper;
using Entities.DTOs;
using Entities.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MakaleComment, MakaleCommentDto>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.MakaleName, opt => opt.MapFrom(src => src.Makale.MakaleName));

        CreateMap<CreateMakaleCommentDto, MakaleComment>();
        CreateMap<UpdateMakaleCommentDto, MakaleComment>();
    }
} 