using System.Configuration;

namespace DaisyPets.UI.ApiServices
{
    public static class AccessSettingsService
    {
        public static readonly string BaseAddressSetting = ReadSetting("ApiBaseAddress");

        public static readonly string ConsultasEndpoint = $"{BaseAddressSetting}/Consulta";
        public static readonly string ContactosEndpoint = $"{BaseAddressSetting}/Contacts";
        public static readonly string DesparasitantesEndpoint = $"{BaseAddressSetting}/Desparasitante";
        public static readonly string DespesasEndpoint = $"{BaseAddressSetting}/Despesa";
        public static readonly string DocumentosEndpoint = $"{BaseAddressSetting}/Document";
        public static readonly string LookupTablesEndpoint = $"{BaseAddressSetting}/LookupTables";
        public static readonly string MailMergeEndpoint = $"{BaseAddressSetting}/Mailmerge";
        public static readonly string PetsEndpoint = $"{BaseAddressSetting}/Pets";
        public static readonly string RacoesEndpoint = $"{BaseAddressSetting}/Racao";
        public static readonly string ServerPdfEndpoint = $"{BaseAddressSetting}/ServerPdf";
        public static readonly string TipoDespesaEndpoint = $"{BaseAddressSetting}/TipoDespesas";
        public static readonly string VacinacoesEndpoint = $"{BaseAddressSetting}/Vacinacao";


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
