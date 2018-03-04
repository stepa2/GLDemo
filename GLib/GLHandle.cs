using System;
using JetBrains.Annotations;

namespace GLib {

    [PublicAPI]
    public abstract class GLHandle : IDisposable
    {
        protected internal int Handle { get; }

        public GLHandle(int handle) => Handle = handle;

        protected abstract void ReleaseUnmanagedResources();

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~GLHandle() {
            ReleaseUnmanagedResources();
        }
    }
}