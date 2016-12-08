using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace REGAL.WMS.WebApi.Core
{
    public class CacheLayer
    {
        private static CacheManager cache = null;

        public static CacheManager Init()
        {
            if (cache == null)
                cache = new CacheManager();

            return cache;
        }
    }
}
