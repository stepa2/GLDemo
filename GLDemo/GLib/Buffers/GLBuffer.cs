using OpenTK.Graphics.OpenGL4;

namespace GLDemo.GLib.Buffers
{
    public class GLBuffer:GLHandle
    {
        internal GLBuffer(int handle) : base(handle) { }
        protected override void ReleaseUnmanagedResources() { GL.DeleteBuffer(Handle); }
    }
}