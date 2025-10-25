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
            if (Item is ExpandoObject expando)
            {
                var dictionary_object = expando as IDictionary<string, object>;
                if (dictionary_object != null && dictionary_object.TryGetValue(sDescription, out var value))
                {
                    return value as string ?? string.Empty;
                }
                return string.Empty;
            }
            else
            {
                string? DescriptionField = GetFieldNames()[1];

                var itemType = Item?.GetType();
                if (itemType == null)
                {
                    return string.Empty;
                }

                var propertyInfo = itemType.GetProperty(DescriptionField);
                if (propertyInfo == null)
                {
                    return string.Empty;
                }

                var output = propertyInfo.GetValue(Item, null);

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

