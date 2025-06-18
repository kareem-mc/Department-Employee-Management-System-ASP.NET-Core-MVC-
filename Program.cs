using Data_Access_Layer.Persistance.Data;
using Data_Access_Layer.Persistance.Repositories.Departments;
using Data_Access_Layer.Persistance.Repositories.Employees;
using Logic_layer.Services.Departments_services;
using Logic_layer.Services.EmployeeServices;
using Microsoft.EntityFrameworkCore;

namespace Web_mvd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // object => Department => serices => Repository => context => options
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {

                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("Defualt connection"));
            });
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();  
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();  
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();  
            builder.Services.AddScoped<IEmployeeServices, EmpolyeeServices>();      

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
