using YulcomAssesment.API.Services.Implementation;
using YulcomAssesment.API.Services.Interface;

namespace YulcomAssesment.API.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddSingleton<SemSlimService>();
            return services;
        }
    }
}
