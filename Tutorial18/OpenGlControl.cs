using System;
using System.Numerics;
using System.Runtime.InteropServices;
using Avalonia.Input;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;
using Common;
using static Avalonia.OpenGL.GlConsts;
using static Common.GlConstExtensions;

namespace Tutorial18
{
    unsafe partial class OpenGlControl : OpenGlControlBase
    {
        protected override void OnOpenGlInit(GlInterface gl, int fb)
        {
            base.OnOpenGlInit(gl, fb);

            gl.FrontFace(GL_CW);
            gl.CullFace(GL_BACK);

            gl.CheckError();

            _model = new CubeModel();

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

            _gLocalTransformLoc = gl.GetUniformLocationString(_shaderProgram, "gLocalTransform");
            _gWorldTransformLoc = gl.GetUniformLocationString(_shaderProgram, "gWorldTransform");
            _gSamplerLoc = gl.GetUniformLocationString(_shaderProgram, "gSampler");
            _gDirectionalLightColorLoc = gl.GetUniformLocationString(_shaderProgram, "gDirectionalLight.Color");
            _gDirectionalLightAmbientIntensityLoc = gl.GetUniformLocationString(_shaderProgram, "gDirectionalLight.AmbientIntensity");
            _gDirectionalLightDirectionLoc = gl.GetUniformLocationString(_shaderProgram, "gDirectionalLight.Direction");
            _gDirectionalLightDiffuseIntensityLoc = gl.GetUniformLocationString(_shaderProgram, "gDirectionalLight.DiffuseIntensity");
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
            gl.BindAttribLocationString(_shaderProgram, NormalLocation, "normal");
        }

        void CreateIndexBuffer(GlInterface gl)
        {
            _ibo = gl.GenBuffer();
            gl.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, _ibo);

            fixed (void* pIndicesData = _model.Indices)
                gl.BufferData(GL_ELEMENT_ARRAY_BUFFER, new IntPtr(sizeof(ushort) * _model.Indices.Length),
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
            _operations.SetPerspective((float)_fieldOfView, (float)Bounds.Width, (float)Bounds.Height,
                (float)_nearPlane,
                (float)_farPlane);
            _operations.Scale((float)_scaleX, (float)_scaleY, (float)_scaleZ);
            _operations.Position((float)_translateX, (float)_translateY, (float)_translateZ);
            _operations.Rotate((float)_rotateX, (float)_rotateY, (float)_rotateZ);

            Matrix4x4 localTransformation = _operations.GetLocalTransformation();
            Matrix4x4 worldTransformation = _operations.GetWorldTransformation();

            gl.UniformMatrix4fv(_gLocalTransformLoc, 1, false, &localTransformation);
            gl.UniformMatrix4fv(_gWorldTransformLoc, 1, false, &worldTransformation);

            gl.Uniform3f(_gDirectionalLightColorLoc, 1f, 1f, 1f);
            gl.Uniform1f(_gDirectionalLightAmbientIntensityLoc, 0.4f);
            gl.Uniform3f(_gDirectionalLightDirectionLoc, 1f, 0f, 0f);
            gl.Uniform1f(_gDirectionalLightDiffuseIntensityLoc, 0.75f);

            gl.DrawElements(GL_TRIANGLES, _model.Indices.Length, GL_UNSIGNED_SHORT, IntPtr.Zero);
            gl.CheckError();

            _camera.OnRender();

            if (_pressedKey == Key.None)
                return;
            
            ProcessInputKey(_pressedKey);
            Dispatcher.UIThread.Post(InvalidateVisual, DispatcherPriority.Background);
        }

        void CreateVertexBuffer(GlInterface gl)
        {
            _vbo = gl.GenBuffer();
            gl.BindBuffer(GL_ARRAY_BUFFER, _vbo);

            fixed (void* pVertices = _model.Vertices)
                gl.BufferData(GL_ARRAY_BUFFER, new IntPtr(sizeof(Vertex) * _model.Vertices.Length),
                    new IntPtr(pVertices), GL_STATIC_DRAW);

            _vao = gl.GenVertexArray();
            gl.BindVertexArray(_vao);

            gl.VertexAttribPointer(
                PositionLocation, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), IntPtr.Zero);
            gl.VertexAttribPointer(
                TexCoordLocation, 2, GL_FLOAT, GL_FALSE, sizeof(Vertex), new IntPtr(sizeof(Vector3)));
            gl.VertexAttribPointer(
                NormalLocation, 3, GL_FLOAT, GL_FALSE, sizeof(Vertex), new IntPtr(sizeof(Vector3) + sizeof(Vector2)));

            gl.EnableVertexAttribArray(PositionLocation);
            gl.EnableVertexAttribArray(TexCoordLocation);
            gl.EnableVertexAttribArray(NormalLocation);
        }

        const int PositionLocation = 0;
        const int TexCoordLocation = 1;
        const int NormalLocation = 2;

        string VertexShaderSource => GlExtensions.GetShader(GlVersion, false, @"
                in vec3 position;
                in vec2 texCoord;
                in vec3 normal;

                uniform mat4 gLocalTransform;
                uniform mat4 gWorldTransform;

                out vec2 texCoord0;
                out vec3 normal0;

                void main()
                {
                    gl_Position = gLocalTransform * vec4(position, 1.0);
                    texCoord0 = texCoord;
                    normal0 = (gWorldTransform * vec4(normal, 0.0)).xyz;
                }
            ");

        string VertexFragmentShaderSource => GlExtensions.GetShader(GlVersion, true, @"
                in vec2 texCoord0;
                in vec3 normal0;

                out vec4 fragColor;

                struct DirectionalLight
                {
                    vec3 Color;
                    float AmbientIntensity;
                    vec3 Direction;
                    float DiffuseIntensity;
                };

                uniform DirectionalLight gDirectionalLight;
                uniform sampler2D gSampler;

                void main()
                {
                    vec4 ambientColor = vec4(gDirectionalLight.Color, 1) * gDirectionalLight.AmbientIntensity;
                                                                                    
                    float diffuseFactor = dot(normalize(normal0), gDirectionalLight.Direction);    
                                                                                                    
                    vec4 diffuseColor;                                                              
                                                                                                    
                    if (diffuseFactor > 0) {                                                        
                        diffuseColor = vec4(gDirectionalLight.Color, 1.0f) *                        
                                    gDirectionalLight.DiffuseIntensity *                         
                                    diffuseFactor;                                               
                    }                                                                               
                    else {                                                                          
                        diffuseColor = vec4(0, 0, 0, 0);                                            
                    }                                                                               
                                                                                                    
                    fragColor = texture(gSampler, texCoord0.xy) *                                 
                                vec4((ambientColor + diffuseColor).xyz, 1);                                 
                }
            ");

        int _vbo;
        int _vao;
        int _ibo;
        int _vertexShader;
        int _fragmentShader;
        int _shaderProgram;
        int _gLocalTransformLoc;
        int _gWorldTransformLoc;
        int _gSamplerLoc;
        int _gDirectionalLightColorLoc;
        int _gDirectionalLightAmbientIntensityLoc;
        int _gDirectionalLightDirectionLoc;
        int _gDirectionalLightDiffuseIntensityLoc;

        IModel _model;
        Texture _texture;

        readonly Pipeline _operations = new Pipeline();
    }
}