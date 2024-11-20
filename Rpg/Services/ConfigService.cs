using Rpg.Core.Services;
using Rpg.Core.Services.Interfaces;
using Rpg.Models.Config;
using System;
using System.IO;
using System.Text.Json;


namespace Rpg.Services
{
    public class ConfigService : BaseService, IConfigService
    {
        private readonly Settings _settings;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };

        public bool IsDebug => _settings.Globals.EnableDebug;

        public ConfigService(string configFilePath)
        {
            if (string.IsNullOrEmpty(configFilePath))
            {
                _settings = new();
                return;
            }

            try
            {
                using StreamReader r = new StreamReader(configFilePath);
                string json = r.ReadToEnd();

                _settings = JsonSerializer.Deserialize<Settings>(json, _jsonSerializerOptions);
            }
            catch (Exception)
            {
                _settings = new();
            }
        }
    }
}
