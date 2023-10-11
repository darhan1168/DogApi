namespace DogApi.Helpers;

public static class MappingLibrary
{
    public static void MapTo<TSource, TDestination>(this TSource source, TDestination destination)
    {
        var sourceProperties = typeof(TSource).GetProperties();
        var destinationProperties = typeof(TDestination).GetProperties();

        foreach (var sourceProperty in sourceProperties)
        {
            var destinationProperty = destinationProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);
            
            if (destinationProperty != null)
            {
                destinationProperty.SetValue(destination, sourceProperty.GetValue(source));
            }
        }
    }
}