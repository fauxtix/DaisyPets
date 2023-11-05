using System.Reflection;

namespace MauiPets.Extensions
{
    public static class ObjectMapping
    {
        public static void MatchAndMap<TSource, TDestination>(this TSource source, TDestination destination)
          where TSource : class, new()
          where TDestination : class, new()
        {
            if (source != null && destination != null)
            {
                List<PropertyInfo> sourceProperties = source.GetType().GetProperties().ToList();
                List<PropertyInfo> destinationProperties = destination.GetType().GetProperties().ToList();

                foreach (PropertyInfo sourceProperty in sourceProperties)
                {
                    PropertyInfo destinationProperty = destinationProperties.Find(item => item.Name == sourceProperty.Name);

                    if (destinationProperty != null)
                    {
                        try
                        {
                            destinationProperty.SetValue(destination, sourceProperty.GetValue(source, null), null);
                        }
                        catch (Exception)
                        {
                            // Handle any exceptions that occur during property mapping here.
                            // You might want to log the exception or take appropriate action.
                        }
                    }
                }
            }
        }

        public static TDestination MapProperties<TDestination>(this object source)
            where TDestination : class, new()
        {
            var destination = Activator.CreateInstance<TDestination>();
            MatchAndMap(source, destination);

            return destination;
        }
    }
}

