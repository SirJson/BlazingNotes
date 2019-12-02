using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingNotes.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            if (services is null)
                throw new System.ArgumentNullException(nameof(services));
        }

        public void Configure(IComponentsApplicationBuilder app) => app.AddComponent<App>("app");
    }
}
