using Microsoft.Extensions.Caching.Memory;
using Agile.BaseLib.IoC;

namespace Agile.BaseLib.Cache
{
    public class MemoryCacheManager
    {
        public static IMemoryCache GetInstance() => AspectCoreContainer.Resolve<IMemoryCache>();
    }
}
