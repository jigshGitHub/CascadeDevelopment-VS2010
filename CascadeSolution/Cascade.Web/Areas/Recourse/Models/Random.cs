using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cascade.Web.Areas.Recourse.Models
{
    public static class RandomGen2
    {
        private static Random _global = new Random();
        [ThreadStatic]
        private static Random _local;

        public static int Next()
        {
            Random inst = _local;
            if (inst == null)
            {
                int seed;
                lock (_global) seed = _global.Next();
                _local = inst = new Random(seed);
            }
            return inst.Next();
        }
    }
}