using EdApp.AutoFill.BL.Contract.Services;
using EdApp.AutoFill.BL.Service;
using EdApp.AutoFill.DAL;
using EdApp.AutoFill.DAL.Contract.Repository;
using EdApp.AutoFill.DAL.Model;
using EdApp.AutoFill.DAL.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EdApp.AutoFill.Web.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            // Repositories.
            services.AddScoped<AutoFillContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBaseRepository<CalculationType>, BaseRepository<CalculationType>>();
            services.AddScoped<IBaseRepository<ModelType>, BaseRepository<ModelType>>();
            services.AddScoped<IBaseRepository<Parameter>, BaseRepository<Parameter>>();

            // Services
            services.AddScoped<ICalculationTypeService, CalculationTypeService>();
            services.AddScoped<IModelTypeService, ModelTypeService>();
            services.AddScoped<IParameterService, ParameterService>();
            services.AddScoped<ILoadAllDataService, LoadAllDataService>();
        }
    }
}