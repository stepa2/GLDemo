using System;
using System.ComponentModel;
using System.Linq;
using GLib.Buffers;
using GLib.Shaders;
using GLib.VAO;
using JetBrains.Annotations;
using OpenTK.Graphics.OpenGL4;

namespace GLib
{
    public static class GLFactory
    {
    #region Buffers

        /// <summary>
        /// Creates <paramref name="count"/> buffers
        /// </summary>
        /// <param name="count">Count of created buffers</param>
        /// <returns>Created buffers</returns>
        [NotNull, MustUseReturnValue, ItemNotNull]
        public static GLBuffer[] CreateBuffers(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), count, "Value is negative");

            if (count == 0)
                return new GLBuffer[0];

            var raw = new int[count];
            GL.GenBuffers(count, raw);
            return raw.ArraySelect(val => new GLBuffer(val));
        }

        /// <summary>
        /// Deletes <paramref name="buffers"/>
        /// </summary>
        /// <param name="buffers">Buffers to delete</param>
        public static void DeleteBuffers([ItemNotNull, NotNull] params GLBuffer[] buffers)
        {
            if (buffers == null) throw new ArgumentNullException(nameof(buffers));
            if (buffers.Length == 0)
                throw new ArgumentException("Value cannot be an empty collection.", nameof(buffers));
            if (buffers.Any(buffer => buffer == null))
                throw new ArgumentNullException(nameof(buffers), "Array contains null elements");
        #if DEBUG || GL_DEBUG
            if (buffers.Any(buffer => buffer.Handle <= 0))
                throw new ArgumentException("Value contains invalid handles", nameof(buffers));
        #endif


            GL.DeleteBuffers(buffers.Length, buffers.ArraySelect(buf => buf.Handle));
        }

        /// <summary>
        /// Creates one buffer
        /// </summary>
        /// <returns>Created buffer</returns>
        [NotNull, MustUseReturnValue]
        public static GLBuffer CreateBuffer() => new GLBuffer(GL.GenBuffer());

        /// <summary>
        /// Deletes <paramref name="buffer"/>
        /// </summary>
        /// <param name="buffer">Buffer to delete</param>
        public static void DeleteBuffer([NotNull] GLBuffer buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
        #if DEBUG || GL_DEBUG
            if (buffer.Handle <= 0)
                throw new ArgumentException("Value's hadle is invalid", nameof(buffer));

        #endif

            GL.DeleteBuffer(buffer.Handle);
        }

    #endregion

    #region Shaders

        [NotNull, MustUseReturnValue]
        public static GLShader CreateShader(ShaderType type)
        {
            if (!Enum.IsDefined(typeof(ShaderType), type))
                throw new InvalidEnumArgumentException(nameof(type), (int) type, typeof(ShaderType));
            var handle = GL.CreateShader(type);

        #if DEBUG || GL_DEBUG
            if (handle == 0)
                throw new InvalidOperationException($"Error creating shader of type {type}");

        #endif

            return new GLShader(handle, type);
        }

        public static void DeleteShader([NotNull] GLShader shader)
        {
            if (shader == null) throw new ArgumentNullException(nameof(shader));

        #if DEBUG || GL_DEBUG
            if (shader.Handle == 0)
                throw new ArgumentException("Value's handle is invalid", nameof(shader));
        #endif

            GL.DeleteShader(shader.Handle);
        }

    #endregion

    #region Shader Programs

        /// <summary>
        /// Creates new shader program
        /// </summary>
        /// <returns>Created program</returns>
        [NotNull, MustUseReturnValue]
        public static GLShaderProgram CreateShaderProgram()
        {
            var handle = GL.CreateProgram();

        #if DEBUG || GL_DEBUG
            if (handle == 0)
                throw new InvalidOperationException("Error creating shader program");
        #endif

            return new GLShaderProgram(handle);
        }

        /// <summary>
        /// Deletes shader program <paramref name="program"/>
        /// </summary>
        /// <param name="program">Shader program to delete</param>
        public static void DeleteShaderProgram([NotNull] GLShaderProgram program)
        {
            if (program == null)
                throw new ArgumentNullException(nameof(program));

        #if DEBUG || GL_DEBUG
            if (program.Handle == 0)
                throw new ArgumentException("Value's handle is invalid", nameof(program));
        #endif

            GL.DeleteProgram(program.Handle);
        }

    #endregion

    #region Vertex Arrays

        [NotNull, MustUseReturnValue, ItemNotNull]
        public static GLVertexArrayObject[] CreateVertexArrays(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), count, "Value is negative");

            if (count == 0)
                return new GLVertexArrayObject[0];

            var raw = new int[count];
            GL.GenVertexArrays(count, raw);
            return raw.ArraySelect(val => new GLVertexArrayObject(val));
        }

        public static void DeleteVertexArrays([ItemNotNull, NotNull] params GLVertexArrayObject[] vertexArrays)
        {
            if (vertexArrays == null) throw new ArgumentNullException(nameof(vertexArrays));
            if (!vertexArrays.Any()) return;

            if (vertexArrays.Any(arr => arr == null))
                throw new ArgumentNullException(nameof(vertexArrays), "Array contains null elements");
        #if DEBUG || GL_DEBUG
            if (vertexArrays.Any(arr => arr.Handle == 0))
                throw new ArgumentException(nameof(vertexArrays), "Array contains invalid elements");
        #endif
            GL.DeleteVertexArrays(vertexArrays.Length, vertexArrays.ArraySelect(arr => arr.Handle));
        }

        [NotNull, MustUseReturnValue]
        public static GLVertexArrayObject CreateVertexArray() => new GLVertexArrayObject(GL.GenVertexArray());

        public static void DeleteVertexArray([NotNull] GLVertexArrayObject array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

        #if DEBUG || GL_DEBUG
            if (array.Handle == 0)
                throw new ArgumentException("Argument's handle is invalid", nameof(array));
        #endif

            GL.DeleteVertexArray(array.Handle);
        }

    #endregion
    }
}