using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace IOptions
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Languages { get; set; }
        public Company Company { get; set; }
    }

    public class Company
    {
        public string Title { get; set; }
        public string Country { get; set; }
    }

    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder().AddJsonFile("person.json");
            AppConfiguration = builder.Build();
        }

        public IConfiguration AppConfiguration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Person>(AppConfiguration);
            services.Configure<Company>(AppConfiguration.GetSection("company"));
            services.Configure<Person>(pers =>
            {
                pers.Age = 22;
            });
            services.Configure<Person>(name =>
            {
                name.Name = "Mark";
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<PersonMiddleware>();
        }
    }
}