using GLDemo.GLib.Buffers;
using JetBrains.Annotations;
using OpenTK.Graphics.OpenGL4;

namespace GLDemo.GLib.VAO
{
    public class GLVertexArrayBinding
    {
        private GLVertexArrayBinding()
        {
            
        }

        [NotNull] public static GLVertexArrayBinding Instance { get; } = new GLVertexArrayBinding();

        [CanBeNull]
        public GLVertexArrayObject Binded
        {
            set => GL.BindVertexArray(value?.Handle ?? 0);
        }

        [NotNull]
        public GLShaderVertexAttribute this[int index] => 
            new GLShaderVertexAttribute(index);

        public class GLShaderVertexAttribute
        {
            private int _index;
            internal GLShaderVertexAttribute(int index) { _index = index; }

            public void AsPointer(
                VertexAttribPointerType type,
                int                     count,
                int                     offset,
                int                     stride,
                bool                    normalized = false)
                => GL.VertexAttribPointer(_index, count, type, normalized, stride, offset);

            public bool Enabled
            {
                set
                {
                    if (value)
                        GL.EnableVertexAttribArray(_index);
                    else
                        GL.DisableVertexAttribArray(_index);
                }
            }
        }


        public void DrawArrays(PrimitiveType primitive, int offset, int count) =>
            GL.DrawArrays(primitive, offset, count);

        public void DrawElements(PrimitiveType primitive, int offset, int count, DrawElementsType type) => 
            GL.DrawElements(primitive, count, type, offset);
    }
}