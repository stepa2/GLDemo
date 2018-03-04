using JetBrains.Annotations;
using OpenTK.Graphics.OpenGL4;

namespace GLib.Shaders
{
    public class GLShader:GLHandle
    {
        public ShaderType ShaderType { get; }

        internal GLShader(int handle, ShaderType type) : base(handle) => ShaderType = type;

        public override string ToString() => $"Shader #{(uint)Handle}";

        protected override void ReleaseUnmanagedResources() {GL.DeleteShader(Handle); }

        public string Source
        {
            [NotNull] set => GL.ShaderSource(Handle, value);
        }

        public void Compile()
        {
            GL.CompileShader(Handle);
            GL.GetShader(Handle,ShaderParameter.CompileStatus,out int success);
            CompilationSucceded = success != (int) Boolean.False;
        }

        public bool CompilationSucceded { get; private set; }

        [NotNull] public string InfoLog => GL.GetShaderInfoLog(Handle);


    }
}