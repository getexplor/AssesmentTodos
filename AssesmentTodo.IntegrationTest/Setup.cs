using AssesmentTodo.IntegrationTest.Features.Todo;
using AssesmentTodo.Persistance;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AssesmentTodo.IntegrationTest
{
    public class Setup
    {
        protected readonly HttpClient _httpClient;

        public Setup()
        {
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(service =>
                    {
                        service.RemoveAll<ApplicationDbContext>();
                        service.AddDbContext<ApplicationDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("AssesmentTodoDb");
                        });
                    });
                });


            _httpClient = appFactory.CreateClient();
        }

        protected async Task Authentication()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetToken());
        }

        private async Task<string> GetToken()
        {
            var content = new
            {
                userName = "agung",
                password = "password123"
            };

            var json = JsonContent.Create(content);
            var response = await _httpClient.PostAsync($"{ConstStringUrl.AuthUrl}/login", json);

            var token = await response.Content.ReadAsStringAsync();

            return token;
        }

    }
}
