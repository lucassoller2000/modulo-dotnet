﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Rewrite;
using Crescer.Spotify.Dominio.Contratos;
using Crescer.Spotify.Infra.Repository;
using Crescer.Spotify.Dominio.Servicos;
using Crescer.Spotify.Infra;
using Crescer.Spotify.Dominio.Entidades;

namespace Crescer.Spotify.WebApi
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
            services.AddMvc();
            
            var connectionString = Configuration.GetConnectionString("Spotify");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Spotify API", Version = "v1" });
            });
            
            services.AddScoped<MusicaService, MusicaService>();
            services.AddScoped<UsuarioService, UsuarioService>();
            services.AddScoped<AlbumService, AlbumService>();
            services.AddScoped<IMusicaRepository, MusicaRepository>();
            services.AddScoped<IAlbumRepository, AlbumRepository>();
             services.AddScoped<IUsuarioRepository, UsuarioRepository>();       
            services.AddScoped<Database>(d => new Database(connectionString));     
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Spotify API");
            });

            app.UseMvc();

            app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));
        }
    }
}
