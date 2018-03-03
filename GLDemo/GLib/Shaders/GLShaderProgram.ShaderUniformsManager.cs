using JetBrains.Annotations;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace GLDemo.GLib.Shaders
{
    public partial class GLShaderProgram
    {
        public class ShaderUniformsManager
        {
            private readonly GLShaderProgram _program;

            internal ShaderUniformsManager(GLShaderProgram program) => _program = program;

            public ShaderUniform ByName([NotNull] string name) => 
                new ShaderUniform(_program,GL.GetUniformLocation(_program.Handle, name));
        }

        public class ShaderUniform
        {
            private readonly GLShaderProgram _program;
            private readonly int _location;
            internal ShaderUniform(GLShaderProgram program, int location)
            {
                _program = program;
                _location = location;
            }

            public void Set(Vector4 value) => GL.ProgramUniform4(_program.Handle, _location, value);
        }
    }
}