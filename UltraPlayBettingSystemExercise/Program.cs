using System.Reflection;
using UltraPlayBettingSystemExercise.Services;
using UltraPlayBettingSystemExercise.ViewModels;
using UltraPlayBettingSystemExercise.Services.Mapping;
using UltraPlayBettingSystemExercise.Services.Interfaces;
using UltraPlayBettingSystemExercise.Data.Repositoies;
using UltraPlayBettingSystemExercise.Data;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using Services.Data;

namespace UltraPlayBettingSystemExercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly, typeof(Program).GetTypeInfo().Assembly);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();

            builder.Services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            builder.Services.AddTransient<IMatchesService, MatchesService>();
            builder.Services.AddTransient<ISportFeedService, SportFeedService>();
            builder.Services.AddTransient<IMessagesService, MessagesService>();
            builder.Services.AddTransient<IXMLProcessor, XMLProcessor>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //app.UseExceptionHandler("/error");

            app.MapControllers();

            app.Run();
        }
    }
}