using Complaints.Business;
using Complaints.IBusiness;

using Complaints.IData;
using Complaints.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }
    public IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services)
    {
        // Load the connection string from your appsettings.json
        services
    .AddControllersWithViews()
    .AddRazorRuntimeCompilation();
        string connectionString = Configuration.GetConnectionString("ComplaintsDb");

        //services.AddDbContext<ComplaintsDbContext>(options =>
        //    options.UseSqlServer(connectionString));

        services.AddTransient<IComplaintsRepository, ComplaintsRepository>(serviceProvider =>
        {
            return new ComplaintsRepository(Configuration.GetConnectionString("ComplaintsDb").ToString());
        });
        services.AddTransient<IComplaintsManager, ComplaintsManager>();
        services.AddMvc();
    }



    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
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
                pattern: "{controller=Complaints}/{action=Home}/{id?}");
        });
    }


}
