using Books.Services;

namespace Books.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFileParsers(this IServiceCollection services)
        {
            services.AddTransient<IFileParser, TextFileParser>();

            services.AddScoped<FileProcessor>();
            return services;
        }
    }
}
