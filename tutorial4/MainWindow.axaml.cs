using System;
using System.Numerics;

using Avalonia.Controls;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using static Avalonia.OpenGL.GlConsts;
using static Common.GlConstExtensions;

namespace Tutorial4
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

                ConfigureShader(gl);
                CreateVertexBuffer(gl);

                gl.CheckError();
            }

            void ConfigureShader(GlInterface gl)
            {
                _shaderProgram = gl.CreateProgram();

                CreateVertexShader(gl);
                CreateVertexFragmentShader(gl);

                Console.WriteLine(gl.LinkProgramAndGetError(_shaderProgram));

                gl.UseProgram(_shaderProgram);
            }

            void CreateVertexFragmentShader(GlInterface gl)
            {
                _vertexFragmentShader = gl.CreateShader(GL_FRAGMENT_SHADER);
                Console.WriteLine(gl.CompileShaderAndGetError(_vertexFragmentShader, VertexFragmentShaderSource));
                gl.AttachShader(_shaderProgram, _vertexFragmentShader);
            }

            void CreateVertexShader(GlInterface gl)
            {
                _vertexShader = gl.CreateShader(GL_VERTEX_SHADER);
                Console.WriteLine(gl.CompileShaderAndGetError(_vertexShader, VertexShaderSource));
                gl.AttachShader(_shaderProgram, _vertexShader);
            }

            protected override void OnOpenGlRender(GlInterface gl, int fb)
            {
                gl.ClearColor(0, 0, 0, 1);
                gl.Clear( GL_COLOR_BUFFER_BIT);

                gl.Viewport(0, 0, (int)Bounds.Width, (int)Bounds.Height);

                gl.DrawArrays(GL_TRIANGLES, 0, new IntPtr(3));
                gl.CheckError();
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

                void main()
                {
                    gl_Position = vec4(0.5 * Position.x, 0.5 * Position.y, Position.z, 1.0);
                }
            ");
            string VertexFragmentShaderSource => GlExtensions.GetShader(GlVersion, true, @"
                out vec4 FragColor;

                void main()
                {
                    FragColor = vec4(1.0, 0.0, 0.0, 1.0);
                }
            ");

            int _vbo;
            int _vao;
            int _vertexShader;
            int _vertexFragmentShader;
            int _shaderProgram;
        }
    }
}