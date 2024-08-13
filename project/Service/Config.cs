using Microsoft.Extensions.Configuration;

namespace project.Service
{
    public class Config
    {
        private static IConfiguration _configuration;

        static Config()
        {
            // Инициализируем конфигурацию из файла
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Укажите путь к вашему файлу конфигурации, если он находится в другом месте
                .AddJsonFile("appsettings.json"); // Укажите имя вашего файла конфигурации

            _configuration = builder.Build();

            // Заполняем свойства значениями из конфигурации
            AppDbConnectionString = _configuration.GetConnectionString("AppDbConnectionString");
            CompanyName = _configuration["ConnectionStrings:CompanyName"];
            CompanyPhone = _configuration["ConnectionStrings:CompanyPhone"];
            CompanyEmail = _configuration["ConnectionStrings:CompanyEmail"];
        }

        public static string AppDbConnectionString { get; private set; }
        public static string CompanyName { get; private set; }
        public static string CompanyPhone { get; private set; }
        public static string CompanyEmail { get; private set; }
    }
}
