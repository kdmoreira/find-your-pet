using FindYourPet.Domain.Interfaces.Repositories;
using FindYourPet.Infra.Data;
using FindYourPet.Infra.Data.Interfaces;
using FindYourPet.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FindYourPet.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection services)
        {
            // Repositories
            services.AddScoped<ISightingRepository, SightingRepository>();

            // Context
            services.AddScoped<IFindYourPetContext, FindYourPetContext>();
        }
    }
}
