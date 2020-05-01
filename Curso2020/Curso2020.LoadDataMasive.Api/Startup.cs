using Curso.Model.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Curso2020.Service.ABMEmpleado;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Curso2020.Service.ABMEmpleado.Interfaces;
using Curso2020.Seguridad.Service.Interfaces;
using Curso2020.Liquidations.Services.Services;

namespace Curso2020.Management.Api
{
	public class Startup
	{
		readonly string MyAllowSpecificOrigins = "_CORS_";

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			

			// Cors
			services.AddCors(options => {
				options.AddPolicy(MyAllowSpecificOrigins, builder =>
				{
					builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
				});
			});

			// TODO: Conext

			// TODO: Services
			services.AddScoped<ILiquidationsServices, LiquidationsServices>();
			services.AddScoped<IEmployeeABMAsync, EmployeeABMAsync>();

			services.AddControllers();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
			string connectionString = this.Configuration.GetConnectionString("LocalHostDb");
			services.AddDbContext<CursoContext>(options => options.UseSqlServer(connectionString));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CursoContext cursoContext)
		{
			cursoContext.Database.EnsureCreated();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseCors(MyAllowSpecificOrigins);

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			DefaultFilesOptions options = new DefaultFilesOptions();
			options.DefaultFileNames.Clear();
			options.DefaultFileNames.Add("index.html");
			app.UseDefaultFiles(options);
			app.UseStaticFiles();
		}
	}
}
