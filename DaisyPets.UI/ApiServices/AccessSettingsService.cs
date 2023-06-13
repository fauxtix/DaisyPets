using System.Configuration;

namespace DaisyPets.UI.ApiServices
{
    public static class AccessSettingsService
    {
        public static string PetsEndpoint()
        {
            return $"{BaseAddressSetting()}/Pets";
        }
        public static string RacoesEndpoint()
        {
            return $"{BaseAddressSetting()}/Racao";
        }
        public static string ConsultasEndpoint()
        {
            return $"{BaseAddressSetting()}/Consulta";
        }
        public static string ContactosEndpoint()
        {
            return $"{BaseAddressSetting()}/Contacts";
        }
        public static string DesparasitantesEndpoint()
        {
            return $"{BaseAddressSetting()}/Desparasitante";
        }
        public static string DespesasEndpoint()
        {
            return $"{BaseAddressSetting()}/Despesa";
        }
        public static string LookupTablesEndpoint()
        {
            return $"{BaseAddressSetting()}/LookupTables";
        }
        public static string VacinacoesEndpoint()
        {
            return $"{BaseAddressSetting()}/Vacinacao";
        }
        public static string MailMergeEndpoint()
        {
            return $"{BaseAddressSetting()}/Mailmerge";
        }

        public static string BaseAddressSetting()
        {
            return ReadSetting("ApiBaseAddress");
        }

        private static string ReadSetting(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";
                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
                return "";
            }
        }
    }
}
