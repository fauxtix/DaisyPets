namespace DaisyPets.Core.Application.Enums
{
    public class Common
    {
        public enum Roles
        {
            Admin = 1,
            Utilizador = 2,
        }

        public enum AlertMessageType
        {
            Error, Warning, Success, Info
        }


        public enum Modules
        {
            Pets,
            Dewormers,
            PetFood,
            Vaccines,
            Documents,
            Consultations,
            Pagamentos,
            ToDos,
            Posts,
            PdfViewer
        }

        public enum TipoBackup
        {
            SqLiteBackup,
            AccessBackup,
            SqlServerBackup
        }

        /// <summary>
        /// Operações a efetuar sobre os registos
        /// </summary>
        public enum OpcoesRegisto
        {
            Inserir,
            Gravar,
            Apagar,
            Duplicar,
            Warning,
            Navegar,
            Backup,
            CriandoNovoRegisto,
            Info,
            Error,
            Zip
        }

        /// <summary>
        /// User roles supported by system
        /// </summary>
        public enum UserRole
        {
            Admin, GeneralUser
        }


        public enum OpcaoCRUD
        {
            Inserir,
            Atualizar,
            Anular,
            Ler
        }

        public enum TipoBD
        {
            SqLite,
            Access,
            SqlServer
        }

        public enum Idioma
        {
            Portugues,
            Espanhol,
            Ingles,
            Frances
        }

        public enum OpcaoSeguranca
        {
            Backup,
            Restore
        }

        public enum OpcaoAlteracao
        {
            Permitir,
            Inibir
        }

        public enum Tamanhos
        {
            Pequeno = 1,
            Medio = 2, 
            Grande = 3, 
            MuitoGrande = 4
        }
    }
}
