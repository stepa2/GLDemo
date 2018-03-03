using System;
using GLDemo.GLib.Buffers;
using GLDemo.GLib.Shaders;
using GLDemo.GLib.VAO;
using JetBrains.Annotations;
using OpenTK.Graphics.OpenGL4;

namespace GLDemo.GLib
{
    public static class GLFactory
    {
        [NotNull,MustUseReturnValue,ItemNotNull]
        public static GLBuffer[] CreateBuffers(int count)
        {
            if (count == 0)
                return new GLBuffer[0];

            var raw = new int[count];
            GL.GenBuffers(count,raw);
            return raw.ArraySelect(val => new GLBuffer(val));
        }

        [NotNull,MustUseReturnValue]
        public static GLBuffer CreateBuffer() => new GLBuffer(GL.GenBuffer());

        public static GLShader CreateShader(ShaderType type) => new GLShader(GL.CreateShader(type),type);

        public static GLShaderProgram CreateShaderProgram() => new GLShaderProgram(GL.CreateProgram());

        [NotNull, MustUseReturnValue, ItemNotNull]
        public static GLVertexArrayObject[] CreateVertexArrays(int count)
        {
            if (count == 0)
                return new GLVertexArrayObject[0];

            var raw = new int[count];
            GL.GenVertexArrays(count, raw);
            return raw.ArraySelect(val => new GLVertexArrayObject(val));
        }

        [NotNull, MustUseReturnValue]
        public static GLVertexArrayObject CreateVertexArray() => new GLVertexArrayObject(GL.GenVertexArray());
    }
}