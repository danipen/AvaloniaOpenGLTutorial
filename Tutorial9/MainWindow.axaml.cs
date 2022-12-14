using System;
using System.Drawing.Drawing2D;
using System.Numerics;

using Avalonia.Controls;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;
using static Avalonia.OpenGL.GlConsts;
using static Common.GlConstExtensions;

namespace Tutorial9
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MyOpenGlControl myControl = new MyOpenGlControl();
            Content = myControl;
        }

        unsafe class MyOpenGlControl : OpenGlControlBase
        {
            protected override void OnOpenGlInit(GlInterface gl, int fb)
            {
                base.OnOpenGlInit(gl, fb);

                ConfigureShaders(gl);
                CreateVertexBuffer(gl);

                gl.CheckError();
            }

            protected override void OnOpenGlDeinit(GlInterface gl, int fb)
            {
                base.OnOpenGlDeinit(gl, fb);

                gl.BindBuffer(GL_ARRAY_BUFFER, 0);
                gl.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
                gl.BindVertexArray(0);
                gl.UseProgram(0);

                gl.DeleteBuffer(_vbo);
                gl.DeleteVertexArray(_vao);
                gl.DeleteProgram(_shaderProgram);
                gl.DeleteShader(_fragmentShader);
                gl.DeleteShader(_vertexShader);

                gl.CheckError();
            }

            void ConfigureShaders(GlInterface gl)
            {
                _shaderProgram = gl.CreateProgram();

                CreateVertexShader(gl);
                CreateFragmentShader(gl);

                Console.WriteLine(gl.LinkProgramAndGetError(_shaderProgram));

                gl.UseProgram(_shaderProgram);

                _gTransformLoc = gl.GetUniformLocationString(_shaderProgram, "gTransform");
            }

            void CreateFragmentShader(GlInterface gl)
            {
                _fragmentShader = gl.CreateShader(GL_FRAGMENT_SHADER);
                Console.WriteLine(gl.CompileShaderAndGetError(_fragmentShader, VertexFragmentShaderSource));
                gl.AttachShader(_shaderProgram, _fragmentShader);
            }

            void CreateVertexShader(GlInterface gl)
            {
                _vertexShader = gl.CreateShader(GL_VERTEX_SHADER);
                Console.WriteLine(gl.CompileShaderAndGetError(_vertexShader, VertexShaderSource));
                gl.AttachShader(_shaderProgram, _vertexShader);
            }

            protected override void OnOpenGlRender(GlInterface gl, int fb)
            {
                _scale += Delta;
                if ((_scale >= 1.5f) || (_scale <= 0.5f)) {
                    Delta *= -1.0f;
                }

                gl.ClearColor(0, 0, 0, 1);
                gl.Clear( GL_COLOR_BUFFER_BIT);

                gl.Viewport(0, 0, (int)Bounds.Width, (int)Bounds.Height);

                Matrix4x4 translating = Matrix4x4.CreateTranslation(_scale, 0, 0);
                Matrix4x4 scaling = Matrix4x4.CreateScale(_scale);
                Matrix4x4 rotating = Matrix4x4.CreateRotationX(_scale);

                Matrix4x4 finalTransform = translating * rotating * scaling;
                
                gl.UniformMatrix4fv(_gTransformLoc, 1, false, &finalTransform);

                gl.DrawArrays(GL_TRIANGLES, 0, new IntPtr(3));
                gl.CheckError();

                Dispatcher.UIThread.Post(InvalidateVisual, DispatcherPriority.Background);
            }

            void CreateVertexBuffer(GlInterface gl)
            {
                Vector3[] vertices = new Vector3[]
                {
                    new Vector3(-1f, -1f, 0.0f),
                    new Vector3(1f, -1f, 0.0f),
                    new Vector3(0.0f, 1f, 0.0f),
                };

                _vbo = gl.GenBuffer();
                gl.BindBuffer(GL_ARRAY_BUFFER, _vbo);

                fixed(void* pVertices = vertices)
                    gl.BufferData(GL_ARRAY_BUFFER, new IntPtr(sizeof(Vector3) * vertices.Length),
                    new IntPtr(pVertices), GL_STATIC_DRAW);

                _vao = gl.GenVertexArray();
                gl.BindVertexArray(_vao);

                gl.VertexAttribPointer(
                    0, 3, GL_FLOAT, GL_FALSE, sizeof(Vector3), IntPtr.Zero);
                gl.EnableVertexAttribArray(0);
            }


            string VertexShaderSource => GlExtensions.GetShader(GlVersion, false, @" 
                in vec3 Position;
                uniform mat4 gTransform;

                out vec4 Color;

                void main()
                {
                    gl_Position = gTransform * vec4(Position, 1.0);
                    Color = vec4(clamp(Position, 0.0, 1.0), 1.0);
                }
            ");
            string VertexFragmentShaderSource => GlExtensions.GetShader(GlVersion, true, @"
                in vec4 Color;
                out vec4 FragColor;

                void main()
                {
                    FragColor = Color;
                }
            ");

            int _vbo;
            int _vao;
            int _vertexShader;
            int _fragmentShader;
            int _shaderProgram;
            int _gTransformLoc;

            float _scale = 0.6f;
            float Delta = 0.005f;
        }
    }
}