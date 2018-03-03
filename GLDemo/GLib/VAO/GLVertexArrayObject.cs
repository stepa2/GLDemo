using OpenTK.Graphics.OpenGL4;

namespace GLDemo.GLib.VAO
{
    public class GLVertexArrayObject:GLHandle

    {
        internal GLVertexArrayObject(int handle) : base(handle) { }
        protected override void ReleaseUnmanagedResources() => GL.DeleteVertexArray(Handle);
    }
}