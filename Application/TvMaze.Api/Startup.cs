using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using TvMaze.Data.Contexts;

namespace TvMaze.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });

        services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
        });
        services.AddMvc(op => op.EnableEndpointRouting = false);

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Version = "v1",
                Title = "TvMaze API",
                Description = "TvMaze API Dev: Vinicius Miguel",
            });
        });

        NativeInjector.InjectServicesForApi(services);
        services.AddMediatR(typeof(Startup));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ShowContext>();
            context.Database.EnsureCreated();
        }

        app.UseStaticFiles();
        app.UseCookiePolicy();
        app.UseAuthorization();

        app.UseMvc(routes =>
        {
            routes.MapRoute(
                name: "default",
                template: "{controller=Shows}/{action=Get}");
        });

        app.UseSwagger();
        app.UseSwaggerUI(s =>
        {
            s.SwaggerEndpoint("/swagger/v1/swagger.json", "TvMaze API v1.0");
        });
    }
    internal sealed class DateOnlyConverter : JsonConverter<DateOnly>
    {
        private const int DateOnlyIsoFormatLength = 10; // YYYY-MM-DD

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateTime dateTime = reader.GetDateTime();
            return DateOnly.FromDateTime(dateTime);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            Span<char> buffer = stackalloc char[DateOnlyIsoFormatLength];
            bool formattedSuccessfully = value.TryFormat(buffer, out int charsWritten, "O", CultureInfo.InvariantCulture);
            Debug.Assert(formattedSuccessfully && charsWritten == DateOnlyIsoFormatLength);
            writer.WriteStringValue(buffer);
        }

        internal  DateOnly ReadAsPropertyNameCore(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            DateTime dateTime = reader.GetDateTime();
            return DateOnly.FromDateTime(dateTime);
        }

        internal  void WriteAsPropertyNameCore(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options, bool isWritingExtensionDataProperty)
        {
            Span<char> buffer = stackalloc char[DateOnlyIsoFormatLength];
            bool formattedSuccessfully = value.TryFormat(buffer, out int charsWritten, "O", CultureInfo.InvariantCulture);
            Debug.Assert(formattedSuccessfully && charsWritten == DateOnlyIsoFormatLength);
            writer.WritePropertyName(buffer);
        }
    }
}
