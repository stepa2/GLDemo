using System;
using JetBrains.Annotations;

namespace GLib {

    [PublicAPI]
    public abstract class GLHandle
    {
        protected internal int Handle { get; }

        protected GLHandle(int handle) => Handle = handle;

    }
}