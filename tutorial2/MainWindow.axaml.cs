using System;
using System.Numerics;

using Avalonia.Controls;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using static Avalonia.OpenGL.GlConsts;
using static Common.GlConstExtensions;


namespace Tutorial2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MyOpenGlControl myControl = new MyOpenGlControl();
            this.Content = myControl;
        }

        unsafe class MyOpenGlControl : OpenGlControlBase
        {
            protected override void OnOpenGlInit(GlInterface gl, int fb)
            {
                base.OnOpenGlInit(gl, fb);

                gl.ClearColor(0,0,0,0);

                ConfigureShader(gl);
                CreateVertexBuffer(gl);
                
                gl.CheckError();
            }

            void ConfigureShader(GlInterface gl)
            {
                mShaderProgram = gl.CreateProgram();

                CreateVertexShader(gl);
                CreateVertexFragmentShader(gl);

                Console.WriteLine(gl.LinkProgramAndGetError(mShaderProgram));
            }

            void CreateVertexFragmentShader(GlInterface gl)
            {
                mVertexFragmentShader = gl.CreateShader(GL_FRAGMENT_SHADER);
                Console.WriteLine(gl.CompileShaderAndGetError(mVertexFragmentShader, VertexFragmentShaderSource));
                gl.AttachShader(mShaderProgram, mVertexFragmentShader);
            }

            void CreateVertexShader(GlInterface gl)
            {
                mVertexShader = gl.CreateShader(GL_VERTEX_SHADER);
                Console.WriteLine(gl.CompileShaderAndGetError(mVertexShader, VertexShaderSource));
                gl.AttachShader(mShaderProgram, mVertexShader);
            }

            protected override void OnOpenGlDeinit(GlInterface gl, int fb)
            {
                base.OnOpenGlDeinit(gl, fb);
            }

            protected override void OnOpenGlRender(GlInterface gl, int fb)
            {
                gl.ClearColor(0, 0, 0, 1);
                gl.Clear( GL_COLOR_BUFFER_BIT);

                gl.Viewport(0, 0, (int)Bounds.Width, (int)Bounds.Height);

                gl.UseProgram(mShaderProgram);
                gl.BindBuffer(GL_ARRAY_BUFFER, mVBO);
                gl.BindVertexArray(mVAO);
                gl.EnableVertexAttribArray(0);
                gl.VertexAttribPointer(0,3, GL_FLOAT, GL_FALSE, 0,IntPtr.Zero);

                gl.DrawArrays(GL_POINTS, 0, new IntPtr(1));
                gl.CheckError();
            }


            void CreateVertexBuffer(GlInterface gl)
            {
                Vector3 vertex = new Vector3(0, 0, 0);

                mVBO = gl.GenBuffer();
                gl.BindBuffer(GL_ARRAY_BUFFER, mVBO);
                gl.BufferData(GL_ARRAY_BUFFER, new IntPtr(sizeof(Vector3)),
                        new IntPtr(&vertex), GL_STATIC_DRAW);

                mVAO = gl.GenVertexArray();
                gl.BindVertexArray(mVAO);


                gl.VertexAttribPointer(
                    0, 3, GL_FLOAT, 0, 0, IntPtr.Zero);
                gl.EnableVertexAttribArray(0);
            }


            string VertexShaderSource => GlExtensions.GetShader(GlVersion, false, @" 
                out vec4 vertexColor;

                void main()
                {
                    vertexColor = vec4(1.0, 1.0, 1.0, 1.0);
                }
");
            string VertexFragmentShaderSource => GlExtensions.GetShader(GlVersion, true, @"
                out vec4 FragColor;
              
                in vec4 vertexColor; // the input variable from the vertex shader (same name and same type)  

                void main()
                {
                    FragColor = vertexColor;
                } 
");

            int mVBO;
            int mVAO;
            int mVertexShader;
            int mVertexFragmentShader;
            int mShaderProgram;
        }
    }
}