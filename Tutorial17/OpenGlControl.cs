using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Common;
using static Avalonia.OpenGL.GlConsts;
using static Common.GlConstExtensions;

namespace Tutorial17
{
    unsafe partial class OpenGlControl : OpenGlControlBase
    {
        protected override void OnOpenGlInit(GlInterface gl, int fb)
        {
            base.OnOpenGlInit(gl, fb);

            gl.FrontFace(GL_CW);
            gl.CullFace(GL_BACK);
            gl.Enable(GL_CULL_FACE);

            gl.CheckError();

            ConfigureShaders(gl);
            CreateVertexBuffer(gl);
            CreateIndexBuffer(gl);

            gl.CheckError();

            _camera.Init((float)Bounds.Width, (float)Bounds.Height);
        }

        protected override void OnOpenGlDeinit(GlInterface gl, int fb)
        {
            base.OnOpenGlDeinit(gl, fb);

            gl.BindBuffer(GL_ARRAY_BUFFER, 0);
            gl.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
            gl.BindVertexArray(0);
            gl.UseProgram(0);

            gl.DeleteBuffer(_ibo);
            gl.DeleteBuffer(_vbo);
            gl.DeleteVertexArray(_vao);
            gl.DeleteProgram(_shaderProgram);
            gl.DeleteShader(_fragmentShader);
            gl.DeleteShader(_vertexShader);

            _texture.Dispose(gl);

            gl.CheckError();
        }

        void ConfigureShaders(GlInterface gl)
        {
            _shaderProgram = gl.CreateProgram();

            CreateVertexShader(gl);
            CreateFragmentShader(gl);
            BindAttributeLocations(gl); // has to be done before linkProgram

            Console.WriteLine(gl.LinkProgramAndGetError(_shaderProgram));

            gl.UseProgram(_shaderProgram);

            _gTransformLoc = gl.GetUniformLocationString(_shaderProgram, "gTransform");
            _gSamplerLoc = gl.GetUniformLocationString(_shaderProgram, "gSampler");
            gl.CheckError();

            gl.Uniform1i(_gSamplerLoc, 0);
            gl.CheckError();

            _texture = new Texture(ResourceLoader.LoadTestTexture());
            _texture.Load(gl);
            _texture.Bind(gl, GL_TEXTURE0);
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

        void BindAttributeLocations(GlInterface gl)
        {
            gl.BindAttribLocationString(_shaderProgram, PositionLocation, "position");
            gl.BindAttribLocationString(_shaderProgram, TexCoordLocation, "texCoord");
        }

        void CreateIndexBuffer(GlInterface gl)
        {
            _indices = new ushort[]
            {
                0, 3, 1,
                1, 3, 2,
                2, 3, 0,
                0, 1, 2
            };

            _ibo = gl.GenBuffer();
            gl.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, _ibo);

            fixed (void* pIndicesData = _indices)
                gl.BufferData(GL_ELEMENT_ARRAY_BUFFER, new IntPtr(sizeof(ushort) * _indices.Length),
                    new IntPtr(pIndicesData), GL_STATIC_DRAW);
        }

        protected override void OnOpenGlRender(GlInterface gl, int fb)
        {
            gl.ClearColor(0, 0, 0, 1);
            gl.Clear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);
            gl.Enable(GL_DEPTH_TEST);

            gl.Viewport(0, 0,
                (int)(Bounds.Width * VisualRoot.RenderScaling),
                (int)(Bounds.Height * VisualRoot.RenderScaling));

            _operations.SetCamera(
                _camera.CameraPosition,
                _camera.CameraTarget,
                _camera.CameraUp);
            _operations.SetPerspective((float)_fieldOfView, (float)Bounds.Width, (float)Bounds.Height, (float)_nearPlane,
                (float)_farPlane);
            _operations.Scale((float)_scaleX, (float)_scaleY, (float)_scaleZ);
            _operations.Position((float)_translateX, (float)_translateY, (float)_translateZ);
            _operations.Rotate((float)_rotateX, (float)_rotateY, (float)_rotateZ);

            Matrix4x4 transformation = _operations.GetTransformation();
            gl.UniformMatrix4fv(_gTransformLoc, 1, false, &transformation);

            gl.DrawElements(GL_TRIANGLES, _indices!.Length, GL_UNSIGNED_SHORT, IntPtr.Zero);
            gl.CheckError();

            _camera.OnRender();
        }

        void CreateVertexBuffer(GlInterface gl)
        {
            Vertex[] vertices = {
                new Vertex()
                {
                    Position = new Vector3(-1.0f, -1.0f, 0),
                    TextCoord = new Vector2(0, 0)
                },
                new Vertex()
                {
                    Position = new Vector3(0.0f, -1.0f, 1),
                    TextCoord = new Vector2(0.5f, 0.0f)
                },
                new Vertex()
                {
                    Position = new Vector3(1.0f, -1.0f, 0),
                    TextCoord = new Vector2(1.0f, 0.0f)
                },
                new Vertex()
                {
                    Position = new Vector3(0.0f, 1.0f, 0.0f),
                    TextCoord = new Vector2(0.5f, 1.0f)
                }
            };

            _vbo = gl.GenBuffer();
            gl.BindBuffer(GL_ARRAY_BUFFER, _vbo);

            fixed (void* pVertices = vertices)
                gl.BufferData(GL_ARRAY_BUFFER, new IntPtr(sizeof(Vertex) * vertices.Length),
                    new IntPtr(pVertices), GL_STATIC_DRAW);

            _vao = gl.GenVertexArray();
            gl.BindVertexArray(_vao);

            gl.VertexAttribPointer(
                PositionLocation, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), IntPtr.Zero);
            gl.VertexAttribPointer(
                TexCoordLocation, 2, GL_FLOAT, GL_FALSE, sizeof(Vertex), new IntPtr(sizeof(Vector3)));

            gl.EnableVertexAttribArray(PositionLocation);
            gl.EnableVertexAttribArray(TexCoordLocation);
        }

        const int PositionLocation = 0;
        const int TexCoordLocation = 1;

        string VertexShaderSource => GlExtensions.GetShader(GlVersion, false, @"
                in vec3 position;
                in vec2 texCoord;
                uniform mat4 gTransform;

                out vec2 texCoord0;

                void main()
                {
                    gl_Position = gTransform * vec4(position, 1.0);
                    texCoord0 = texCoord;
                }
            ");

        string VertexFragmentShaderSource => GlExtensions.GetShader(GlVersion, true, @"
                in vec2 texCoord0;
                out vec4 fragColor;
                uniform sampler2D gSampler;

                void main()
                {
                    fragColor = texture(gSampler, texCoord0.xy);
                }
            ");

        [StructLayout(LayoutKind.Sequential)]
        struct Vertex
        {
            public Vector3 Position;
            public Vector2 TextCoord;
        }

        int _vbo;
        int _vao;
        int _ibo;
        int _vertexShader;
        int _fragmentShader;
        int _shaderProgram;
        int _gTransformLoc;
        int _gSamplerLoc;

        Texture _texture;

        ushort[]? _indices;
        readonly Pipeline _operations = new Pipeline();
    }
}