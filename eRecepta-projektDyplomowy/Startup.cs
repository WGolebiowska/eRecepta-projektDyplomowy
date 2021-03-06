using AutoMapper;
using eRecepta_projektDyplomowy.Configuration;
using eRecepta_projektDyplomowy.Configuration.Profiles;
using eRecepta_projektDyplomowy.Controllers.Services;
using eRecepta_projektDyplomowy.Controllers.Services.Interfaces;
using eRecepta_projektDyplomowy.Data;
using eRecepta_projektDyplomowy.Models;
using eRecepta_projektDyplomowy.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

namespace eRecepta_projektDyplomowy
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>()
                .AddProfileService<ProfileService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin",
                    policy =>
                    {
                        policy.RequireClaim(ClaimTypes.Role, "administrator");
                    });
                options.AddPolicy("IsDoctor",
                    policy =>
                    {
                        policy.RequireClaim(ClaimTypes.Role, "doctor");
                    });
                options.AddPolicy("GetAppointmentsPolicy", policy =>
                    policy.Requirements.Add(new SameUserRequirement()));
                options.AddPolicy("GetPrescriptionsPolicy", policy =>
                    policy.Requirements.Add(new PrescriptionRequirement()));
                options.AddPolicy("isAdminOrDoctor", policy =>
                    policy.Requirements.Add(new DoctorOrAdminRequirement()));
            });

            services.AddControllersWithViews();
            services.AddControllers().AddNewtonsoftJson(x =>
 x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddRazorPages();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            // Mail configuration
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddTransient<IEmailSender, MailService>();
            services.AddTransient(typeof(ILogger), typeof(Logger<Startup>));
            services.AddAutoMapper(typeof(MainProfile));
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<IHelperService, HelperService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IPrescriptionService, PrescriptionService>();
            services.AddTransient<IPrescriptionFormService, PrescriptionFormService>();
            services.AddTransient<IIllnessService, IllnessService>();
            services.AddTransient<IMedicineService, MedicineService>();
            services.AddTransient<ClaimsPrincipal>(
                s => s.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddTransient<IAuthorizationHandler, AppointmentAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, DoctorOrAdminAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, PrescriptionAuthorizationHandler>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "eRecepta API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
            
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "eRecepta API");
            });
        }
    }
}