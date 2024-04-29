using Microsoft.EntityFrameworkCore;
using Data;
using Service;
using Microsoft.Extensions.Configuration;
using Repository;

public class Startup
{

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddHttpClient<IPokeAPIService, PokeAPIService>();
        services.AddHttpClient<IPokemonService, PokemonService>();
        services.AddHttpClient<IMoveService, MoveService>();
        services.AddHttpClient<ITypeService, TypeService>();
        services.AddHttpClient<IBattleService, BattleService>();
        services.AddScoped<IPokemonRepository, PokemonRepository>();
        services.AddScoped<IMoveRepository, MoveRepository>();
        services.AddScoped<ITypeRepository, TypeRepository>();

        services.AddCors(options => 
            options.AddPolicy("MyCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
    }

    

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors("MyCorsPolicy");
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

}