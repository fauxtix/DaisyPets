using Syncfusion.Blazor;

namespace DaisyPets.Web.Blazor.Shared
{
    public class SyncfusionLocalizer : ISyncfusionStringLocalizer
    {
        public string GetText(string key)
        {
            return this.ResourceManager.GetString(key);
        }

        public System.Resources.ResourceManager ResourceManager
        {
            get
            {
                // Replace the ApplicationNamespace with your application name.
                return DaisyPets.Web.Blazor.Resources.SfResources.ResourceManager;
            }
        }
    }
}
