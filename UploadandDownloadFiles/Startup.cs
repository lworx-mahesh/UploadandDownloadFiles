using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadandDownloadFiles.DbcontextData;
using UploadandDownloadFiles.Helpers;
using UploadandDownloadFiles.Ilogics;
using UploadandDownloadFiles.IRepository;
using UploadandDownloadFiles.Logics;
using UploadandDownloadFiles.Repository;
using UploadandDownloadFiles.Services;
using UploadandDownloadFiles.TableModel;

namespace UploadandDownloadFiles
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UploadandDownloadFiles", Version = "v1" });
            });

            services.AddTransient<IFileService, FileService>();

            services.AddTransient<ISavedocumentsignparameters, SavedocumentsignparametersLogic>();
            //services.AddTransient<IRepository, Repository>();
            //   services.AddScoped<StoreSignerInfo>();
            //   services.AddScoped<SignerColourListModel>();
            services.AddScoped<IRepository<StoreSignerInfo>, Repository<StoreSignerInfo>>();
            services.AddScoped<IRepository<SignerColourListModel>, Repository<SignerColourListModel>>();
            services.AddScoped<IRepository<SavePdfBtnValues>, Repository<SavePdfBtnValues>>();
            services.AddScoped<IRepository<SaveControlAxis>, Repository<SaveControlAxis>>();
            services.AddScoped<IRepository<SenddocumentForSigns>, Repository<SenddocumentForSigns>>();
            services.AddScoped<IRepository<RecipientSignerForDocuments>, Repository<RecipientSignerForDocuments>>();
            services.AddScoped<ISenddocumentForSign,SenddocumentForSignlogic >();
            // Connect to Database    
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<docusigndbcontext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction:sqlOptions=>
                {
                    sqlOptions.EnableRetryOnFailure();
                }
                ));



            //Here I setup to read appsettings with DTO AppSetting now we can discard helper models.
            services.Configure<DTODocuSign.AppSettingsDTO>(Configuration.GetSection("AppSettings"));

            //// Here I Setup to read with DTO for getting Connection DB dynamically
            //services.Configure<JaniView.Bal.DTO.ConnectionDTO>(Configuration.GetSection("ConnectionStrings"));


            var appSettingsSection = Configuration.GetSection("AppSettings");

            var ForteSection = Configuration.GetSection("Forte");


            //services.Configure<JaniView.Bal.DTO.AppSettingsDTO>(appSettingsSection);


            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            // for getting AUTH values Smarty Street API

            var SmartyStreetsAuthID = appSettings.SmartyStreetsAuthID;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UploadandDownloadFiles v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
