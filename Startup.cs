using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AggregateBot.Models.User;
using AggregateBot.Services.TgBotService;
using AggregateBot.Services.TgUpdateService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AggregateBot
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var botConfigurationSection = Configuration.GetSection("BotConfiguration");
            var connectionString = Configuration.GetConnectionString("UserContext");
            services.AddMvc();

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<UserContext>(options => { options.UseNpgsql(connectionString); });

            services.AddScoped<ITgUpdateService, TgUpdateService>();
            services.AddSingleton<ITgBotService, TgBotService>();

            services.Configure<BotConfiguration>(botConfigurationSection);

            SetupTgWebhook(botConfigurationSection);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }

        private async void SetupTgWebhook(IConfiguration section)
        {
            var tgApiWebhookUrl = $"https://api.telegram.org/bot{section["BotToken"]}/setWebhook";

            var client = new HttpClient();

            var data = new StringContent(JsonConvert.SerializeObject(new
            {
                url = section["WebHookUrl"]
            }), Encoding.UTF8, "application/json");

            var res = await client.PostAsync(tgApiWebhookUrl, data);
            _logger.LogInformation(res.ToString());
        }
    }
}