﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroLog.Internal
{
    // Enables types within PclContrib to use platform-specific features in a platform-agnostic way
    internal static class PlatformAdapter
    {
        private static readonly string[] KnownPlatformNames = new[] { "NetFx", "NetCore" };
        private static IAdapterResolver _resolver = new ProbingAdapterResolver(KnownPlatformNames);

        public static T Resolve<T>()
        {
            T value = (T)_resolver.Resolve(typeof(T));

            if (value == null)
                throw new PlatformNotSupportedException(Strings.AdapterNotSupported);

            return value;
        }

        // Unit testing helper
        internal static void SetResolver(IAdapterResolver resolver)
        {
            _resolver = resolver;
        }
    }

}