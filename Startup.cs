using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using weather_api.Application;

namespace weather_api
{
    public class Startup
    {
        private readonly string corsPolicy = "development-allowAll";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {   
            //CORS
            //Aqui a ordem dos tratores alteram o viaduto, então atenção para ordem ao informar a política de cors, pois não pode ser adicionada abaixo dos controllers.
            services.AddCors(options => {
                options.AddPolicy(corsPolicy, 
                policy=> policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

                //Se produção
                options.AddPolicy("production-allowFew",
                policy => policy.WithOrigins("www.myserver.com.br"));
            });

            services.AddControllers();
            // Níveis de injeção de dependências Transient adiciona uma nova instância para a classe WeatherSummary não compartilha estado.
            services.AddTransient<IWeatherSummary, WeatherSumary>();
            
            //services.AddScoped<IWeatherSummary, WeatherSumary>();
            //Scoped = compartilha estado dentro da mesma requisição


            //services.AddSingleton<IWeatherSummary, WeatherSumary>();
            //Singleton = compartilha estado entre todas as requisições

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Aqui o cors precisa ficar antes do Redirection e após o Routing
            app.UseRouting();

            app.UseCors(corsPolicy);

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
