using AutoMapper;
using ClothingStoreFranchise.NetCore.Common.Extensible;

namespace ClothingStoreFranchise.NetCore.Common.Mapper
{
    public class ExtensibleEntityProfile : Profile
    {
        public ExtensibleEntityProfile()
        {
            CreateMap<IExtensibleEntity, BaseExtensibleEntityDto>()
                .ForMember(dto => dto.ExtendedData, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(src => src.SerializedExtendedData, opt => opt.Ignore())
                .ForMember(src => src.ExtendedData, opt => opt.Ignore());
        }
    }
}
