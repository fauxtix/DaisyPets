using System.Globalization;
using System.Reflection;
using System.Resources;

namespace MauiPets.Resources.Languages
{
    [ContentProperty(nameof(Key))]
    public class LocalizeExtension : IMarkupExtension
    {
        public string Key { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Key == null)
                return "";

            var resourceManager = new ResourceManager("MauiPets.Resources.Languages.AppResources", typeof(LocalizeExtension).GetTypeInfo().Assembly);

            var culture = CultureInfo.CurrentUICulture;
            return resourceManager.GetString(Key, culture) ?? $"[{Key}]";
        }
    }
}