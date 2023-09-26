using FluentValidation;
using System.Dynamic;

namespace MauiPetsApp.Infrastructure.Validators
{
    /// <summary>
    /// Validação de tabelas auxiliares
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TabAuxGenericValidator<T> : AbstractValidator<T>
    {
        /// <summary>
        ///  Construtor
        /// </summary>
        public TabAuxGenericValidator()
        {
            var sDescriptionField = "Descricao";

            RuleFor(p => ItemTextValue(p, sDescriptionField).ToString())
                .NotEmpty().NotNull().WithMessage("Preencha descrição, p.f.")
                .Length(2, 100).WithMessage("Nº caracteres inválido (entre 2 e 100)");
        }

        private string ItemTextValue(T Item, string sDescription)
        {
            if (Item is ExpandoObject)
            {
                IDictionary<string, object> dictionary_object = Item as ExpandoObject;
                return (string)dictionary_object[sDescription];
            }
            else
            {
                string? DescriptionField = GetFieldNames()[1];

                var output = Item.GetType()
                .GetProperty(DescriptionField)
                .GetValue(Item, null);

                string? sRet = output == null ? "" : output.ToString();
                return sRet!;
            }
        }

        protected string[] GetFieldNames() // not expando...
        {
            var tipo = typeof(T);
            var p = tipo.GetProperties();

            string[] fieldNames = typeof(T)
            .GetProperties()
            .Select(p => p.Name)
            .ToArray();

            return fieldNames;
        }
    }

}

