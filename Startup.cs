using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Mocks
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
            services.AddControllers()
                 .AddNewtonsoftJson(opt =>
                {
                    var contractResolver = (DefaultContractResolver)opt.SerializerSettings.ContractResolver;
                    // � ���������� ������ ����������� ��� ��������, ���������� �� ������ ShouldSerialize
                    contractResolver.IgnoreShouldSerializeMembers = true;
                    // � ���������� ������ ����������� json ��� �����������
                    opt.SerializerSettings.Formatting = Formatting.Indented;
                    contractResolver.NamingStrategy = new CamelCaseNamingStrategy();
                    opt.SerializerSettings.DateParseHandling = DateParseHandling.None;
                    opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    // Set default JSON.NET settings
                    JsonConvert.DefaultSettings = () => opt.SerializerSettings;
                });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            } // ������� ����������� (��)

            app.UseHttpsRedirection(); // ��� ����� ������������

            app.UseRouting(); // ��� ������ ����������

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // ��� ������ ����������, ��� ������ ���� �� ��������
            });
        }
    }
}