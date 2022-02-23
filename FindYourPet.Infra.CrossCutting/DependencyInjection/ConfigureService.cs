using AutoMapper;
using FindYourPet.Domain.Interfaces.Services;
using FindYourPet.Infra.CrossCutting.AutoMapper;
using FindYourPet.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FindYourPet.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection services)
        {
            // Services      
            services.AddTransient<ISightingService, SightingService>();

            // AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ModelToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
                cfg.AddProfile(new EntityToModelProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
