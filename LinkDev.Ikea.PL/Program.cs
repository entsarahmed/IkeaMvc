using LinkDev.Ikea.BLL.Common.Services.Attachments;
using LinkDev.Ikea.BLL.Services.Departments;
using LinkDev.Ikea.BLL.Services.Employees;
using LinkDev.Ikea.DAL.Entities.Identity;
using LinkDev.Ikea.DAL.Persistance.Data;
using LinkDev.Ikea.DAL.Persistance.Repositories.Departments;
using LinkDev.Ikea.DAL.Persistance.Repositories.Employees;
using LinkDev.Ikea.DAL.Persistance.UnitOfWork;
using LinkDev.Ikea.PL.Controllers.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace LinkDev.Ikea.PL
{
    public class Program
    {
        //Entry Point
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Configure Services

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>((OptionsBuilder) =>
            { 
            OptionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
           
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();


            builder.Services.AddTransient<IAttachmentService, AttachmentService>();



           // builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)))
           // builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddAutoMapper(M => M.AddProfile( new MappingProfile()));



            //Allow Dependence Injection in AccountController

            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>();//Add the default Identity Configuration for the specified user and Role Type
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = true; //#%$
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars=1;

                options.User.RequireUniqueEmail = true;
                //options.User.AllowedUserNameCharacters = "asdmfhhf;ajshs;kjjfh";

                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts=5;
                options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromDays(5);




            })//Add the default Identity Configuration for the specified user and Role Type
              .AddEntityFrameworkStores<ApplicationDbContext>(); //Register Identity Scope from Dependence Injection Container


			//Replace All
			//         builder.Services.AddScoped<UserManager<ApplicationUser>>();
			//builder.Services.AddScoped<SignInManager<ApplicationUser>>();
			//builder.Services.AddScoped<RoleManager<IdentityRole>>();







			#endregion

			var app = builder.Build();

            #region Configure Kestrel Middlewares

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion

            app.Run();
        }
    }
}
