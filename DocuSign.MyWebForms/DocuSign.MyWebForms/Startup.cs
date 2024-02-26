using System.Threading.Tasks;
using DocuSign.MyWebForms.Infrustructure.Extensions;
using DocuSign.MyWebForms.Services.Auth;
using DocuSign.MyWebForms.Services.Auth.Implementation;
using DocuSign.MyWebForms.Services.Clients;
using DocuSign.MyWebForms.Services.Clients.Implementation;
using DocuSign.MyWebForms.Services.Configuration;
using DocuSign.MyWebForms.Services.Configuration.Implementation;
using DocuSign.MyWebForms.Services.Database;
using DocuSign.MyWebForms.Services.Database.Implementation;
using DocuSign.MyWebForms.Services.Documents;
using DocuSign.MyWebForms.Services.Documents.Implementation;
using DocuSign.MyWebForms.Services.Form;
using DocuSign.MyWebForms.Services.Form.Implementation;
using DocuSign.MyWebForms.Services.Statuses;
using DocuSign.MyWebForms.Services.Statuses.Implementation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DocuSign.MyWebForms
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddHttpContextAccessor();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IFormService, FormService>();
            services.AddScoped<IAccountRepository, ClaimsAccountRepository>();
            services.AddScoped<IDocuSignClientsFactory, DocuSignClientsFactory>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddSingleton<IAppConfiguration, AppSettingsConfiguration>();
            services.AddSingleton<IEnvelopeStatusModelConverter, EnvelopeStatusModelConverter>();
            services.AddSingleton<IEnvelopeStatusRepository, InMemoryEnvelopeStatusRepository>();
            services.AddSingleton<IDocumentModelConverter, DocumentModelConverter>();

            services.AddSpaStaticFiles(configuration =>
                {
                    configuration.RootPath = "ClientApp/dist";
                });

            services.AddAuthentication(options =>
            {
                options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
            {
                config.Cookie.Name = "UserLoginCookie";
                config.Cookie.HttpOnly = true;
                config.Cookie.SameSite = SameSiteMode.Lax;
                config.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.AddControllers().AddNewtonsoftJson();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.Headers["Location"] = context.RedirectUri;
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/myapp-{Date}.txt");
            app.UseSession();
            app.ConfigureDocuSignExceptionHandling(loggerFactory);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
                }
            });
        }
    }
}
