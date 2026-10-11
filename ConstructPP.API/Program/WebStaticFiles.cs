using Microsoft.Extensions.FileProviders;

namespace ConstructPP.API.Program
{
    public static class WebStaticFiles
    {
        private const string EmbeddedRoot = "wwwroot";

        public static WebApplication UseWebStaticFiles(this WebApplication app)
        {
            var fileProvider = CreateFileProvider(app);
            var staticFileOptions = new StaticFileOptions { FileProvider = fileProvider };

            app.UseDefaultFiles(new DefaultFilesOptions { FileProvider = fileProvider });
            app.UseStaticFiles(staticFileOptions);
            app.MapFallbackToFile("index.html", staticFileOptions);

            return app;
        }

        private static IFileProvider CreateFileProvider(WebApplication app)
        {
            var webRootPath = app.Environment.WebRootPath;

            if (app.Environment.IsDevelopment() && !string.IsNullOrEmpty(webRootPath) && Directory.Exists(webRootPath))
            {
                return new PhysicalFileProvider(webRootPath);
            }

            return new ManifestEmbeddedFileProvider(typeof(WebStaticFiles).Assembly, EmbeddedRoot);
        }
    }
}
