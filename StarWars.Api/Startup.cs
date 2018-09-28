using AutoMapper;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StarWars.Api.Models;
using StarWars.Core.Data;
using StarWars.Core.Logic;
using StarWars.Data.EntityFramework;
using StarWars.Data.EntityFramework.Repositories;
using StarWars.Data.EntityFramework.Seed;

namespace StarWars.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<StarWarsQuery>();            
            services.AddTransient<ICharacterRepository, CharacterRepository>();
            services.AddTransient<IDroidRepository, DroidRepository>();
            services.AddTransient<IHumanRepository, HumanRepository>();
            services.AddTransient<IEpisodeRepository, EpisodeRepository>();
            
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<ITrilogyHeroes, TrilogyHeroes>();
            services.AddTransient<DroidType>();
            services.AddTransient<HumanType>();
            services.AddTransient<CharacterInterface>();
            services.AddTransient<EpisodeEnum>();
            var sp = services.BuildServiceProvider();
            services.AddScoped<ISchema>(_ => new StarWarsSchema(type => (GraphType) sp.GetService(type)) {Query = sp.GetService<StarWarsQuery>()});
            
            // Configuration database
            services.AddDbContext<StarWarsContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("StarWarsDatabaseConnection"), b => b.MigrationsAssembly("StarWars.Api")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, StarWarsContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            app.UseHttpsRedirection();
            app.UseMvc();
            
            db.EnsureSeedData();
        }
    }
}