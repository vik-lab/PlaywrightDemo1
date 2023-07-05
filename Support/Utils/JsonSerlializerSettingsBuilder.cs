using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.Web.Tests.Support.Utils
{
    public static class JsonSerializerSettingsBuilder
    {
        private static JsonSerializerSettings? _settings;

        public static JsonSerializerSettings GetSettings()
        {
            _settings = new JsonSerializerSettings();
            _settings.MissingMemberHandling = MissingMemberHandling.Error;
            _settings.CheckAdditionalContent = true;
            return _settings;
        }
    }
}