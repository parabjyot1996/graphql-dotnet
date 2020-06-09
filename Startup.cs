using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL_POC.Contracts;
using GraphQL_POC.Entities.Context;
using GraphQL_POC.GraphQL.Mutations;
using GraphQL_POC.GraphQL.Queries;
using GraphQL_POC.GraphQL.Schemas;
using GraphQL_POC.GraphQL.Subscriptions;
using GraphQL_POC.GraphQL.Types;
using GraphQL_POC.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphQL_POC
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
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            //Register DbContext and database provider with connectionstring
            services.AddDbContextPool<ApplicationContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            //End

            //Register dependency injection
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            //End

            //Registered GraphQL dependencies
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddScoped<AppSchema>();

            services.AddScoped<AppQuery>();

            services.AddScoped<OwnerType>();

            services.AddScoped<AccountType>();

            services.AddScoped<AccountTypeEnumType>();

            services.AddScoped<OwnerInputType>();

            services.AddScoped<AccountInputType>();

            services.AddScoped<AppMutation>();

            services.AddScoped<OwnerCreatedEvents>();

            services.AddScoped<AppSubscription>();

            services.AddGraphQL()
                    .AddGraphTypes(ServiceLifetime.Scoped)
                    .AddWebSockets();
            //Ends

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            //GraphQL middlewares
            app.UseWebSockets();

            app.UseGraphQLWebSockets<AppSchema>("/graphql");

            app.UseGraphQL<AppSchema>("/graphql");

            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions
            {
                Path = "/ui/playground"
            });
            //End

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}