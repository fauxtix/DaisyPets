using DaisyPets.UI.Properties;
using Syncfusion.Windows.Forms;

namespace DaisyPets.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjIyMzE2MkAzMjMxMmUzMDJlMzBCdlcySmlaSjFFeU5BQjNaUDNYQ0R3VHFROCttQ3FiWi9TbStncWVGcGlFPQ ==");

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MessageBoxAdv.DropShadow = true;
            MessageBoxAdv.MaximumSize = new Size(520, Screen.PrimaryScreen.WorkingArea.Size.Height);
            MessageBoxAdv.MessageBoxStyle = MessageBoxAdv.Style.Office2016;
            MessageBoxAdv.Office2016Theme = Office2016Theme.DarkGray;
            FontFamily fontFamily = new FontFamily("Segoe UI");
            Font font = new Font(
               fontFamily,
               14,
               FontStyle.Regular,
               GraphicsUnit.Pixel);
            MessageBoxAdv.ButtonFont = font;
            MessageBoxAdv.CaptionFont = font;
            MessageBoxAdv.MessageFont = font;

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new frmMain());
        }

    }
}