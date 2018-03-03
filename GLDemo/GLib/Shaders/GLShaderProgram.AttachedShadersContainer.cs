using JetBrains.Annotations;
using OpenTK.Graphics.OpenGL4;

namespace GLDemo.GLib.Shaders
{
    public partial class GLShaderProgram
    {
        public class AttachedShadersContainer
        {
            [NotNull] public GLShaderProgram Program { get; }

            internal AttachedShadersContainer([NotNull] GLShaderProgram program) { Program = program; }

            public void Link(GLShader shader) => GL.AttachShader(Program.Handle, shader.Handle);
            public void UnLink(GLShader shader) => GL.DetachShader(Program.Handle, shader.Handle);


        }
    }
}