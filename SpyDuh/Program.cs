using SpyDuh.Repositories;

namespace SpyDuh
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<ISpyRepository, SpyRepository>();
            builder.Services.AddTransient<ISkillsRepository, SkillsRepository>();
            builder.Services.AddTransient<IAssignmentRepository, AssignmentRepository>();
            builder.Services.AddTransient<IHandlerRepository, HandlerRepository>();
            //if below is not present it will cause a 500 response from swagger
            builder.Services.AddTransient<IServicesRepository, ServicesRepository>();
            


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseCors(options =>
                {
                    options.AllowAnyOrigin();
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();

                });

            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}