using OpenTK.Graphics.OpenGL4;

namespace GLib.Buffers
{
    public class GLBuffer:GLHandle
    {
        internal GLBuffer(int handle) : base(handle) { }
        protected override void ReleaseUnmanagedResources() { GL.DeleteBuffer(Handle); }
    }
}