using Rpg.Interfaces;
using Rpg.Models.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


namespace Rpg.Services
{
    public class ConfigService : IConfigService
    {
        private readonly Settings _settings;

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
                using (StreamReader r = new StreamReader(configFilePath))
                {
                    string json = r.ReadToEnd();

                    _settings = JsonSerializer.Deserialize<Settings>(json, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                    });
                }
            }
            catch (Exception)
            {
                _settings = new();
            }
        }
    }
}
