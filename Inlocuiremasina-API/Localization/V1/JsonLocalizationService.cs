using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.Text.Json;

namespace Localization.V1
{
    public class JsonLocalizationService
    {
        private readonly string _resourcesPath = "Resources";
        private readonly Dictionary<string, Dictionary<string, string>> _translations = new();

        public JsonLocalizationService()
        {
            LoadAllTranslations();
        }

        private void LoadAllTranslations()
        {
            if (!Directory.Exists(_resourcesPath))
            {
                throw new DirectoryNotFoundException($"Resources directory '{_resourcesPath}' not found.");
            }

            foreach (var filePath in Directory.GetFiles(_resourcesPath, "messages.*.json"))
            {
                var culture = Path.GetFileNameWithoutExtension(filePath).Split('.').Last();
                var jsonData = File.ReadAllText(filePath);
                var translations = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonData);

                if (translations != null)
                {
                    _translations[culture] = translations;
                }
            }
        }

        public string GetLocalizedString(string key, string? culture = null)
        {
            culture ??= CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            if (_translations.TryGetValue(culture, out var langDict) && langDict.TryGetValue(key, out var value))
            {
                return value;
            }

            return $"--{key}--";
        }
    }
}
