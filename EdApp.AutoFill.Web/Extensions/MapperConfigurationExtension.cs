using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using EdApp.AutoFill.BL.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace EdApp.AutoFill.Web.Extensions
{
    public static class MapperConfigurationExtension
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                cfg.AddMaps(typeof(DefaultProfile).Assembly);
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}