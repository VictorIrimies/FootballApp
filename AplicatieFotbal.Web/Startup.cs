using AplicatieFotbal.DataAccess.Data;
using AplicatieFotbal.DataAccess.Data.Repository;
using AplicatieFotbal.DataAccess.Data.Repository.IRepository;
using AplicatieFotbal.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AplicatieFotbal.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public object MyIdentityDataInitializer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders().AddDefaultTokenProviders();


            //services.AddIdentityCore<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI();
            //services.AddMvc(config =>
           // {
           //     var policy = new AuthorizationPolicyBuilder()
           //                     .RequireAuthenticatedUser()
           //                     .Build();
           //     config.Filters.Add(new AuthorizeFilter(policy));
           // });

            //services.AddAuthorization();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddControllersWithViews().AddNewtonsoftJson().AddRazorRuntimeCompilation();
            //services.AddControllersWithViews().AddFluentValidation();
            services.AddRazorPages();
            services.ConfigureApplicationCookie(options =>
            {

                options.LoginPath = $"/Dashboard/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Dashboard/Administrator/AccessDenied";

                options.Cookie.Name = "Edafic";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            //services.AddAuthentication().AddFacebook(options => {
            //    options.AppId = Configuration["FacebookAppId"];
            //    options.AppSecret = Configuration["FacebookAppSecret"];
            //}).AddGoogle(options => {
            //    options.ClientId = Configuration["GoogleClientId"];
            //    options.ClientSecret = Configuration["GoogleClientSecret"];
            //});


            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Default SignIn settings.

                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.Configure<PasswordHasherOptions>(option =>
            {
                option.IterationCount = 12000;
            });

            services.AddRazorPages().AddMvcOptions(options =>
            {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(_ => "The field is required.");
            });

            //services.Configure<SmtpOption>(Configuration.GetSection("Smtp"));

            //services.AddSingleton<DataAccess.Service.IService.IEmailSender, SmtpEmailSender>();

            //services.AddTransient<IValidator<Models.View.SignUp>, Models.View.SignupValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<ApplicationUser> userManager,
RoleManager<IdentityRole> roleManager)
        {

            //MyIdentityDataInitializer.SeedData(userManager, roleManager);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=Dashboard}/{controller=Home}/{action=Index}/{id?}");
                // endpoints.MapRazorPages();
            });
        }
    }
}

            //TODO: De redirectionat in functie de rol
        
    
