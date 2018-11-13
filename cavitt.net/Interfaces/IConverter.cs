using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cavitt.net.Interfaces
{
    public interface IConverter<TSource, TDestination>
    {
        TDestination Convert(TSource source_object);
        TSource Convert(TDestination source_object);
    }
}
