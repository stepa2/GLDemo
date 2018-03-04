using JetBrains.Annotations;
using OpenTK.Graphics.OpenGL4;

namespace GLib.Shaders
{
    public partial class GLShaderProgram : GLHandle
    {
        internal GLShaderProgram(int handle) : base(handle)
        {
            AttachedShaders = new AttachedShadersContainer(this); 
            Uniforms = new ShaderUniformsManager(this);
        }

        public override string ToString() => $"Shader program #{(uint)Handle}";

        [NotNull] public AttachedShadersContainer AttachedShaders { get; }

        public void Link()
        {
            GL.LinkProgram(Handle);
            GL.GetProgram(Handle,GetProgramParameterName.LinkStatus,out int success);
            LinkSucceded = success != (int) Boolean.False;
        }

        public bool LinkSucceded { get; private set; }

        [NotNull] public string InfoLog => GL.GetProgramInfoLog(Handle);

        [NotNull]
        public static GLShaderProgram CurrentlyUsed
        {
            set => GL.UseProgram(value.Handle);
        }

        public ShaderUniformsManager Uniforms { get; }
    }
}