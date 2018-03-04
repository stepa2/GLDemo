using OpenTK.Graphics.OpenGL4;

namespace GLib.Buffers
{
    public static class GLBufferBindings
    {
        public static GLBufferBinding ArrayBuffer { get; } = new GLBufferBinding(BufferTarget.ArrayBuffer);

        public static GLBufferBinding ElementArrayBuffer { get; } = new GLBufferBinding(BufferTarget.ElementArrayBuffer);
    }
}