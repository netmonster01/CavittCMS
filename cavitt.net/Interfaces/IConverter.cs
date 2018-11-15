namespace cavitt.net.Interfaces
{
    public interface IConverter<TSource, TDestination>
    {
        TDestination Convert(TSource source_object);
        TSource Convert(TDestination source_object);
    }
}
