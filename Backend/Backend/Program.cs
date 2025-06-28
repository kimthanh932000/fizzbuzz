using Backend.Repositories;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register dbcontext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

            // Add repo to the container.
            builder.Services.AddScoped<IGameRepo, GameRepo>();
            builder.Services.AddScoped<IRuleRepo, RuleRepo>();
            builder.Services.AddScoped<IGameSessionRepo, GameSessionRepo>();
            builder.Services.AddScoped<IGameSessionNumberRepo, GameSessionNumberRepo>();

            // Add services to the container.
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<IRuleService, RuleService>();
            builder.Services.AddScoped<IGameSessionService, GameSessionService>();
            builder.Services.AddScoped<IGameSessionNumberService, GameSessionNumberService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
