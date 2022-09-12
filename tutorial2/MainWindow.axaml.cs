using System;
using System.Numerics;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
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

        class MyOpenGlControl : OpenGlControlBase
        {
            protected override void OnOpenGlInit(GlInterface gl, int fb)
            {
                base.OnOpenGlInit(gl, fb);
                
                gl.ClearColor(0,0,0,0);
                
                CreateVertexBuffer(gl);
            }

            protected override void OnOpenGlDeinit(GlInterface gl, int fb)
            {
                base.OnOpenGlDeinit(gl, fb);
            }

            protected override void OnOpenGlRender(GlInterface gl, int fb)
            {
                gl.Clear( GL_COLOR_BUFFER_BIT);
                
                gl.BindBuffer(GL_ARRAY_BUFFER, VBO);
                
                gl.EnableVertexAttribArray(0);
                
                gl.VertexAttribPointer(0,3, GL_FLOAT, GL_FALSE, 0,IntPtr.Zero);
                
                gl.DrawArrays(GL_POINTS, 0, new IntPtr(1));
                
                
                // glClear(GL_COLOR_BUFFER_BIT);
                //
                // glBindBuffer(GL_ARRAY_BUFFER, VBO);
                //
                // glEnableVertexAttribArray(0);
                //
                // glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 0, 0);
                //
                // glDrawArrays(GL_POINTS, 0, 1);
                //
                // glDisableVertexAttribArray(0);
                //
                // glutSwapBuffers();
            }
            
            
            unsafe void CreateVertexBuffer(GlInterface gl)
            {
                Vector3 vertex = new Vector3(0,0,0);
                //VBO = new IntPtr(&vertex);
                //int* VBOData = (int *)VBO.ToPointer();

                void* vboData = new IntPtr(&vertex).ToPointer();
                VBO = gl.GenBuffer(); 
                gl.BindBuffer(GL_ARRAY_BUFFER, VBO);
                gl.BufferData(GL_ARRAY_BUFFER, new IntPtr(sizeof(Vector3)), new IntPtr(vboData), GL_STATIC_DRAW);
                
                // Vector3f Vertices[1];
                // Vertices[0] = Vector3f(0.0f, 0.0f, 0.0f);
                //
                // glGenBuffers(1, &VBO);
                // glBindBuffer(GL_ARRAY_BUFFER, VBO);
                // glBufferData(GL_ARRAY_BUFFER, sizeof(Vertices), Vertices, GL_STATIC_DRAW);
            }

            private int VBO;

        }
    }
}