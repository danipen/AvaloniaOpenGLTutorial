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
            }

            void ConfigureShader(GlInterface gl)
            {

                mShaderProgram = gl.CreateProgram();
                gl.CheckError();

                CreateVertexShader(gl);
                CreateVertexFragmentShader(gl);

                Console.WriteLine(gl.LinkProgramAndGetError(mShaderProgram));
                gl.CheckError();
            }

            void CreateVertexFragmentShader(GlInterface gl)
            {
                mVertexFragmentShader = gl.CreateShader(GL_FRAGMENT_SHADER);
                gl.CheckError();

                Console.WriteLine(gl.CompileShaderAndGetError(mVertexFragmentShader, VertexFragmentShaderSource));
                gl.CheckError();

                gl.AttachShader(mShaderProgram, mVertexFragmentShader);
                gl.CheckError();
            }

            void CreateVertexShader(GlInterface gl)
            {
                mVertexShader = gl.CreateShader(GL_VERTEX_SHADER);
                gl.CheckError();

                Console.WriteLine(gl.CompileShaderAndGetError(mVertexShader, VertexShaderSource));
                gl.CheckError();

                gl.AttachShader(mShaderProgram, mVertexShader);
                gl.CheckError();

                gl.BindAttribLocationString(mShaderProgram, 0, "aPos");
                gl.CheckError();
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
                gl.CheckError();

                gl.BindBuffer(GL_ARRAY_BUFFER, mVBO);
                gl.CheckError();

                gl.BindVertexArray(mVAO);
                gl.CheckError();

                gl.EnableVertexAttribArray(0);
                gl.CheckError();

                gl.VertexAttribPointer(0,3, GL_FLOAT, GL_FALSE, 0,IntPtr.Zero);
                gl.CheckError();

                gl.DrawArrays(GL_POINTS, 0, new IntPtr(1));
                gl.CheckError();
            }


            void CreateVertexBuffer(GlInterface gl)
            {
                Vector3 vertex = new Vector3(0, 0, 0);

                mVBO = gl.GenBuffer();
                gl.BindBuffer(GL_ARRAY_BUFFER, mVBO);
                gl.CheckError();

                gl.BufferData(GL_ARRAY_BUFFER, new IntPtr(sizeof(Vector3)),
                        new IntPtr(&vertex), GL_STATIC_DRAW);
                gl.CheckError();

                mVAO = gl.GenVertexArray();
                gl.BindVertexArray(mVAO);
                gl.CheckError();

                gl.VertexAttribPointer(
                    0, 3, GL_FLOAT, 0, 0, IntPtr.Zero);
                gl.CheckError();

                gl.EnableVertexAttribArray(0);
                gl.CheckError();
            }


            string VertexShaderSource => GlExtensions.GetShader(GlVersion, false, @"
       vec3 aPos;
  
        out vec4 vertexColor;

        void main()
        {
            gl_Position = vec4(aPos, 1.0);
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