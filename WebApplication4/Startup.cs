using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
using System.Threading.Tasks;
using WebApplication4.Data;
using WebApplication4.Services;

namespace WebApplication4
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

            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;//����Ĭ����FALSE����ǰ������ͷΪxml��ʱ��Ҳ���᷵��406�����룬�������ɷ���JSON�� ��Ϊtrue���������ͺͷ�����֧�ֵ����Ͳ�һ�¾ͻ᷵��406
                 //setup.OutputFormatters.Add(new Xml)
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ICompanyRepository, CompanyRepository>();//ע��
            services.AddDbContext<RoutineDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("LocalDB"));
            });
            //services.AddDbContext<RoutineDbContext>(option =>
            //{
            //    option.UseSqlite("Data Source=routine.db");
            //});
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApplication4", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication4 v1"));
            }

            app.UseRouting();

            app.UseAuthorization();//�����ǰʾ��ûɶ�ã���Ϊ��Щapi�����Ա�����������������Ȩɶ��

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
