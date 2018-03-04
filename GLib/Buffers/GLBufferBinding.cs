using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;

namespace GLib.Buffers {
    public class GLBufferBinding
    {
        private readonly BufferTarget _type;
        public GLBufferBinding(BufferTarget type) { _type = type; }

        public GLBuffer Binded
        {
            set => GL.BindBuffer(_type, value?.Handle ?? 0);
        }

        public void BufferData<T>(T[] data, BufferUsageHint usageHint, int? count = null) where T : struct => 
            GL.BufferData(_type, (count ?? data.Length) * Marshal.SizeOf<T>(), data, usageHint);




    }
}