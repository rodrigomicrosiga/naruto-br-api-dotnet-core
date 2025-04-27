using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace NarutoApiConsumer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient(); // Registrando HttpClient
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class NarutoController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private readonly HttpClient _httpClient;

        public NarutoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet("naruto")]
        public async Task<IActionResult> GetNarutoData()
        {
            const string apiUrl = "https://api.narutobr.com/endpoint"; // Substitua pelo URL correto

            try
            {
                var response = await _httpClient.GetFromJsonAsync<object>(apiUrl); // Adapte o tipo de objeto para o esperado
                return Ok(response);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}