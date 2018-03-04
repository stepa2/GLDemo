using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;
using GLib;
using GLib.Buffers;
using GLib.Shaders;
using GLib.VAO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace GLDemo
{
    public class DemoWindow : GameWindow
    {
        [StructLayout(LayoutKind.Sequential,Pack = 0)]
        private struct VertexType //Hack due to Marshal.SizeOf<(Vector3,Color4)>() exception
        {
            public VertexType(Vector3 pos, Color4 color)
            {
                Position = pos;
                Color = new Vector3(color.R,color.G,color.B);
            }
            [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
            public Vector3 Position { get; set; }

            [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
            public Vector3 Color { get; set; }
        }

        private static readonly VertexType[] _vertices =
        {
            new VertexType(new Vector3(0.5F,  0.5F,  0),Color.Red),
            new VertexType(new Vector3(0.5F,  -0.5F, 0),Color.DarkGreen),
            new VertexType(new Vector3(-0.5F, -0.5F, 0),Color.Blue),
            new VertexType(new Vector3(-0.5F, 0.5F,  0),Color.Orange)
        };

        private static readonly uint[] _indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        private GLBuffer _vertexBuffer;
        private GLBuffer _ebo;

        private GLShader _vertexShader;
        private GLShader _fragmentShader;

        private GLShaderProgram _shaderProgram;

        private GLVertexArrayObject _vao;

        protected override void OnLoad(EventArgs e)
        {
            GL.Viewport(ClientRectangle);
            Resize += (sender, _) => GL.Viewport(((DemoWindow) sender).ClientRectangle);
            RenderFrame += OnRenderRequested;
            KeyDown += OnKeyPressed;


            { //Vertex shader

                _vertexShader        = GLFactory.CreateShader(ShaderType.VertexShader);
                _vertexShader.Source = HardcodedShaders.BasicVertex;
                _vertexShader.Compile();
                if (!_vertexShader.CompilationSucceded)
                {
                    Console.Error.WriteLine($"{_vertexShader} compilation error:");
                    Console.Error.WriteLine(_vertexShader.InfoLog);
                }

                Utility.CheckForErrors2();

            }

            { //Fragment shader
                _fragmentShader        = GLFactory.CreateShader(ShaderType.FragmentShader);
                _fragmentShader.Source = HardcodedShaders.BasicFragment;
                _fragmentShader.Compile();
                if (!_fragmentShader.CompilationSucceded)
                {
                    Console.Error.WriteLine($"{_fragmentShader} compilation error:");
                    Console.Error.WriteLine(_fragmentShader.InfoLog);
                }

                Utility.CheckForErrors2();

            }

            { //Shader program
                _shaderProgram = GLFactory.CreateShaderProgram();

                {
                    _shaderProgram.AttachedShaders.Link(_vertexShader);
                    _shaderProgram.AttachedShaders.Link(_fragmentShader);
                }
                _shaderProgram.Link();
                if (!_shaderProgram.LinkSucceded)
                {
                    Console.Error.WriteLine($"{_shaderProgram} compilation error:");
                    Console.Error.WriteLine(_shaderProgram.InfoLog);
                }

                Utility.CheckForErrors2();

            }
            {
                _vao = GLFactory.CreateVertexArray();

                GLVertexArrayBinding.Instance.Binded = _vao;
                Utility.CheckForErrors2();

                _vertexBuffer                       = GLFactory.CreateBuffer();
                GLBufferBindings.ArrayBuffer.Binded = _vertexBuffer;
                GLBufferBindings.ArrayBuffer.BufferData(_vertices, BufferUsageHint.StaticDraw);


                Utility.CheckForErrors2();

                var attribPos = GLVertexArrayBinding.Instance[0];

                var stride = Marshal.SizeOf<VertexType>();
                var posSize = Marshal.SizeOf<Vector3>();
                attribPos.AsPointer(VertexAttribPointerType.Float,3, offset: 0, stride: stride);
                attribPos.Enabled = true;

                var attribColor = GLVertexArrayBinding.Instance[1];

                attribColor.AsPointer(VertexAttribPointerType.Float,3,offset: posSize,stride: stride);
                attribColor.Enabled = true;


                GLBufferBindings.ArrayBuffer.Binded = null;

                GLVertexArrayBinding.Instance.Binded = null;

                Utility.CheckForErrors2();

            }
            {
                _ebo = GLFactory.CreateBuffer();
                GLBufferBindings.ElementArrayBuffer.Binded = _ebo;
                GLBufferBindings.ElementArrayBuffer.BufferData(_indices,BufferUsageHint.StaticDraw);
                GLBufferBindings.ElementArrayBuffer.Binded = null;
            }

            Utility.CheckForErrors2();


            base.OnLoad(e);


        }

        private void OnKeyPressed(object sender, KeyboardKeyEventArgs e)
        {
            var window = (DemoWindow) sender;

            switch (e.Key)
            {
                case Key.Escape:
                    window.Close();
                    break;

                case Key.W:
                    GL.PolygonMode(MaterialFace.FrontAndBack,PolygonMode.Line);

                    break;

                case Key.F:
                    GL.PolygonMode(MaterialFace.FrontAndBack,PolygonMode.Fill);
                    break;

            }

        }


        private void OnRenderRequested(object sender, FrameEventArgs e)
        {
            var window = (DemoWindow) sender;

            GL.ClearColor(Color.DarkCyan);
            GL.Clear(ClearBufferMask.ColorBufferBit);




            GLVertexArrayBinding.Instance.Binded = window._vao;

            GLBufferBindings.ElementArrayBuffer.Binded = _ebo;


            //var color = _shaderProgram.Uniforms.ByName("color1");
            //color.Set(ComputeColorComponent());

            GLShaderProgram.CurrentlyUsed = _shaderProgram;
            GLVertexArrayBinding.Instance.DrawElements(PrimitiveType.Triangles,0,6,DrawElementsType.UnsignedInt);

            GLVertexArrayBinding.Instance.Binded = null;

            window.SwapBuffers();
        }

        //private Vector4 ComputeColorComponent()
        //{
        //    return new Vector4(1,(float)(Math.Sin(new TimeSpan(DateTime.Now.Ticks).TotalSeconds) / 2 + 0.5), 1, 1);
        //}

        protected override void OnUnload(EventArgs e)
        {
            _shaderProgram.Dispose();
            _fragmentShader.Dispose();
            _vertexShader.Dispose();
            GLBufferBindings.ArrayBuffer.Binded = null;
            _vertexBuffer.Dispose();
            _ebo.Dispose();


            _vao.Dispose();
            base.OnUnload(e);
        }
    }
    
}