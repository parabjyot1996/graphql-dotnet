using System;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Http;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using GraphQLServer.Contracts;
using GraphQLServer.Entities.Context;
using GraphQLServer.GraphQL.Mutations;
using GraphQLServer.GraphQL.Queries;
using GraphQLServer.GraphQL.Schemas;
using GraphQLServer.GraphQL.Subscriptions;
using GraphQLServer.GraphQL.Types;
using GraphQLServer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace GraphQLServer
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

            //Okta OAuth authentication
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
              options.Authority = "https://dev-478144.okta.com/oauth2/default";
              options.Audience = "api://default";
              options.RequireHttpsMetadata = false;
            });
            //End

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
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            
            services.AddSingleton<DataLoaderDocumentListener>();

            services.AddScoped<IDocumentExecuter, DocumentExecuter>();

            services.AddScoped<IDocumentWriter, DocumentWriter>();

            services.AddScoped<ISchema, AppSchema>();

            services.AddScoped<AppQuery>();

            services.AddScoped<OwnerType>();

            services.AddScoped<AccountType>();

            services.AddScoped<AccountTypeEnumType>();

            services.AddScoped<OwnerInputType>();

            services.AddScoped<AccountInputType>();

            services.AddScoped<AppMutation>();

            services.AddScoped<OwnerCreatedEvents>();

            services.AddScoped<AppSubscription>();

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddScoped(
                    s => new AppSchema(new FuncDependencyResolver(type => (IGraphType)s.GetRequiredService(type))));

                services.AddGraphQL()
                        .AddGraphTypes(ServiceLifetime.Scoped)
                        .AddWebSockets()
                        .AddDataLoader();
            //Ends

            services.AddControllers()
                    .AddNewtonsoftJson(
                        options => options
                                    .SerializerSettings
                                    .ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    );
                    

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder =>
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();
    
            //GraphQL middlewares
            app.UseWebSockets();

            app.UseGraphQLWebSockets<AppSchema>();

            //app.UseGraphQL<AppSchema>();

            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());
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