using AutoMapper;
using EdApp.AutoFill.BL.Model;
using EdApp.AutoFill.DAL.Model;

namespace EdApp.AutoFill.BL.Mapper
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<CalculationType, CalculationTypeDto>().ReverseMap();
            CreateMap<ModelType, ModelTypeDto>().ReverseMap();
            CreateMap<Parameter, ParameterDto>().ReverseMap();
        }
    }
}