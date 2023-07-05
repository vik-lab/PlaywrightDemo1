using MercuryWebsite.Tests.UI.Model.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MercuryWebsite.Tests.UI.Settings
{
    public sealed class ConfigurationService
    {
        private static readonly IConfigurationRoot Root = InitializeConfiguration();

        public ConfigurationService()
        {
        }

        public static TSection GetSection<TSection>()
            where TSection : class, new()
        {
            string sectionName = MakeFirstLetterToLower(typeof(TSection).Name);

            return Root.GetSection(sectionName).Get<TSection>()!;
        }

        private static string MakeFirstLetterToLower(string text)
        {
            return char.ToLower(text[0]) + text.Substring(1);
        }

        public static WebSettings GetWebSettings()
        {
            var result = Root.GetSection("webSettings").Get<WebSettings>();
            if (result == null)
            {
                throw new ConfigurationNotFoundException(typeof(WebSettings).ToString());
            }
            return result;
        }

        public static TraceSettings GetTraceSettings()
        {
            var result = Root.GetSection("traceSettings").Get<TraceSettings>();
            if (result == null)
            {
                throw new ConfigurationNotFoundException(typeof(TraceSettings).ToString());
            }
            return result;
        }

        public static VideoSettings GetVideoSettings()
        {
            var result = Root.GetSection("videoSettings").Get<VideoSettings>();
            if (result == null)
            {
                throw new ConfigurationNotFoundException(typeof(VideoSettings).ToString());
            }
            return result;
        }

        public static LoginData GetTestUser()
        {
            LoginData loginData = new LoginData(Environment.GetEnvironmentVariable("TEST_USER_EMAIL")!,
                                                Environment.GetEnvironmentVariable("TEST_USER_PWD")!);
            
            return loginData;
        }

        private static IConfigurationRoot InitializeConfiguration()
        {
            var filesInExecutionDir = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!);
            var settingsFile = filesInExecutionDir.FirstOrDefault(x => x.Contains("appSettings") && x.EndsWith(".json"));
            var builder = new ConfigurationBuilder();
            builder.AddEnvironmentVariables();
            
            if (settingsFile != null)
            {
                builder.AddJsonFile(settingsFile, optional: true, reloadOnChange: true);
            }
            
            return builder.Build();
        }
    }
}
