namespace DaisyPets.Core.Application.Enums
{
    public class Common
    {
        public enum Roles
        {
            Admin = 1,
            Utilizador = 2,
        }

        public enum SituacaoFracao
        {
            Livre,
            Alugada,
            Reservada,
            Vendida
        }

        public enum EstadoConservacaoImovel
        {
            Bom,
            PrecisaObras,
            Degradado
        }

        public enum EstadoConservacaoFracao
        {
            Bom,
            PrecisaObras,
            Degradado
        }

        public enum TipoBackup
        {
            SqLiteBackup,
            AccessBackup,
            SqlServerBackup
        }

        /// <summary>
        /// Tabelas a carregar nas comboboxes
        /// </summary>
        public enum ComboBoxItem
        {
            TipoDespesas,
            Role,
            TipoContacto,
            TipoPropriedade,
            TipoRecebimento,
            Utilizadores
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
            Navegar,
            CriandoNovoRegisto
        }

        /// <summary>
        /// User roles supported by system
        /// </summary>
        public enum UserRole
        {
            Admin, GeneralUser
        }

        public enum TipoPesquisa
        {
            Todos,
            Autor,
            Editora,
            Genero,
            Formato,
            Arquivo,
            Lido,
            SemPaginas,
            SemCapa,
            Pago,
            Duplicados,
            Utilizador,
            Titulo
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

        public enum ImputacaoDespesa
        {
            Imovel = 1,
            Fracao = 2,
            Geral = 3
        }

        public enum OpcaoAlteracao
        {
            Permitir,
            Inibir
        }
    }
}
