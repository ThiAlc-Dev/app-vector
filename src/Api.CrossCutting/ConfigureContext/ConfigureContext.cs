using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Api.Data.Context;
using Data.Repository;
using Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CrosCutting.ConfigureContext
{
    public class ConfigureContext
    {
        public static void ConfigureDbContext(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            //serviceCollection.AddScoped<IUserRepository, UserRepository>();

            var MYSQL_CONNECTION = Environment.GetEnvironmentVariable("MYSQL_CONNECTION");
            var MYSQL_DATABASE = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
            var dbVersion = "5.7";

            var connection = MYSQL_CONNECTION + $";Database={MYSQL_DATABASE};";

            serviceCollection.AddDbContext<MyContext>(o =>
            o.UseMySql(
                connection,
                new MySqlServerVersion(new Version(dbVersion))
                )
            );
        }

        public static void ConfigureMgrationDatabase(IServiceScope service)
        {
            using (var context = service.ServiceProvider.GetService<MyContext>())
            {
                context.Database.Migrate();
            }
        }
    }
}
