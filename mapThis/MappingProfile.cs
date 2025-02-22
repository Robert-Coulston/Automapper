// filepath: /D:/Data/Code/mapThis/MappingProfile.cs
using AutoMapper;

namespace mapThis;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<string, DestinationValueClass>()
            .ForMember(dest => dest.CategoryValue, opt => opt.MapFrom(src => src));

        CreateMap<SourceClass, DestinationClass>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CategoryValues, opt => opt.MapFrom(src => src.GetCategoryValuesArray()))
            .AfterMap((src, dest) =>
            {
                dest.CategoryValues = src.GetCategoryValuesArray();
                dest.DestinationValueClasses = src.GetCategoryValuesArray()
                    .Select(value => new DestinationValueClass { CategoryValue = value, DestinationClassId = dest.Id })
                    .ToList();
            });
    }
}