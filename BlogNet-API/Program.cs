
using BlogNet_DAL;

namespace BlogNet_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<PostRepository>();
            builder.Services.AddScoped<CommentRepository>();
            builder.Services.AddScoped<ConnectionManager>(provider => new ConnectionManager(builder.Configuration.GetConnectionString("connectionStringDB")));

            var app = builder.Build();

//            if (app.Environment.IsDevelopment())
//            {
                app.UseSwagger();
                app.UseSwaggerUI();
            //            }

            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();

//            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
