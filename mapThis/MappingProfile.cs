// filepath: /D:/Data/Code/mapThis/MappingProfile.cs
using AutoMapper;

namespace mapThis;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<string, AccountValue>()
            .ForMember(dest => dest.CategoryValue, opt => opt.MapFrom(src => src));

        CreateMap<SourceClass, Account>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CategoryValues, opt => opt.MapFrom(src => src.GetCategoryValuesArray()))
            .AfterMap((src, dest) =>
            {
                dest.CategoryValues = src.GetCategoryValuesArray();
                dest.AccountValues = src.GetCategoryValuesArray()?
                    .Select(value => new AccountValue { CategoryValue = value, AccountId = dest.Id, Account = dest })
                    .ToList();
            });
    }
}