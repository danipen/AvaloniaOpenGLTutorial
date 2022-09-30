using System;
using System.Numerics;

using Avalonia.Input;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;
using Common;
using static Avalonia.OpenGL.GlConsts;
using static Common.GlConstExtensions;

namespace Tutorial22
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

            IModelLoader cube1 = new CubeModelLoader();
            cube1.LoadMesh();

            IModelLoader cube2 = new CubeModelLoader();
            cube2.LoadMesh();

            Matrix4x4 translateTransform = Matrix4x4.CreateTranslation(3, 0, 0);
            cube2.Model.Meshes[0].Transform = translateTransform;

            _modelLoader = new ModelLoader(ResourceLoader.LoadLanciaIntegraleModel());
            // _modelLoader = new CubeModelLoader();
            // _modelLoader = new MultiModelLoader(cube2, cube1);
            _modelLoader.LoadMesh();

            _vao = gl.GenVertexArray();
            gl.BindVertexArray(_vao);

            ConfigureShaders(gl);
            CreateVertexBuffer(gl);
            CreateIndexBuffer(gl);

            gl.CheckError();

            var delta = _modelLoader.Model.MaxPosition - _modelLoader.Model.MinPosition;
            var maxScale = Math.Max(Math.Max(delta.X, delta.Y), delta.Z);

            _camera.Init((float)Bounds.Width, (float)Bounds.Height, _modelLoader.Model.MaxPosition / maxScale, _modelLoader.Model.MinPosition / maxScale);

            if (maxScale == 0)
                return;

            ScaleX = 10 / maxScale;
            ScaleY = 10 / maxScale;
            ScaleZ = 10 / maxScale;
        }

        protected override void OnOpenGlDeinit(GlInterface gl, int fb)
        {
            base.OnOpenGlDeinit(gl, fb);

            gl.BindBuffer(GL_ARRAY_BUFFER, 0);
            gl.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, 0);
            gl.BindVertexArray(0);
            gl.UseProgram(0);

            gl.DeleteBuffer(_ibo);
            gl.DeleteBuffer(_positionsVBO);
            gl.DeleteBuffer(_TexCoordVBO);
            gl.DeleteBuffer(_NormalVBO);
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
            _gMeshTransformLoc = gl.GetUniformLocationString(_shaderProgram, "gMeshTransform");
            _gCameraDirLoc = gl.GetUniformLocationString(_shaderProgram, "gCameraDir");
            _gSamplerLoc = gl.GetUniformLocationString(_shaderProgram, "gSampler");
            gl.CheckError();

            _gDirectionalLightColorLoc = gl.GetUniformLocationString(_shaderProgram, "gDirectionalLight.Color");
            _gDirectionalLightDirectionLoc = gl.GetUniformLocationString(_shaderProgram, "gDirectionalLight.Direction");
            _gDirectionalLightGlobalIntensityLoc = gl.GetUniformLocationString(_shaderProgram, "gDirectionalLight.GlobalIntensity");
            _gDirectionalLightAmbientIntensityLoc = gl.GetUniformLocationString(_shaderProgram, "gDirectionalLight.AmbientIntensity");
            _gDirectionalLightDiffuseIntensityLoc = gl.GetUniformLocationString(_shaderProgram, "gDirectionalLight.DiffuseIntensity");
            _gDirectionalLightSpecularIntensityLoc = gl.GetUniformLocationString(_shaderProgram, "gDirectionalLight.SpecularIntensity");
            gl.CheckError();

            _gMaterialAmbientColorLoc = gl.GetUniformLocationString(_shaderProgram, "gMaterial.AmbientColor");
            _gMaterialDiffuseColorLoc = gl.GetUniformLocationString(_shaderProgram, "gMaterial.DiffuseColor");
            _gMaterialSpecularColorLoc = gl.GetUniformLocationString(_shaderProgram, "gMaterial.SpecularColor");
            _gMaterialShininessLoc = gl.GetUniformLocationString(_shaderProgram, "gMaterial.Shininess");
            _gMaterialShininessStrengthLoc = gl.GetUniformLocationString(_shaderProgram, "gMaterial.ShininessStrength");
            gl.CheckError();

            gl.Uniform1i(_gSamplerLoc, 0);
            gl.CheckError();

            _texture = new Texture(ResourceLoader.LoadTestTexture());
            _texture.Load(gl);
            _texture.Bind(gl, GL_TEXTURE0);
        }

        void BindAttributeLocations(GlInterface gl)
        {
            gl.BindAttribLocationString(_shaderProgram, PositionLocation, "position");
            gl.BindAttribLocationString(_shaderProgram, TexCoordLocation, "texCoord");
            gl.BindAttribLocationString(_shaderProgram, NormalLocation, "normal");
        }

        void CreateFragmentShader(GlInterface gl)
        {
            var source = FragmentShaderSource;
            _fragmentShader = gl.CreateShader(GL_FRAGMENT_SHADER);
            Console.WriteLine(gl.CompileShaderAndGetError(_fragmentShader, FragmentShaderSource));
            gl.AttachShader(_shaderProgram, _fragmentShader);
        }

        void CreateVertexShader(GlInterface gl)
        {
            _vertexShader = gl.CreateShader(GL_VERTEX_SHADER);
            Console.WriteLine(gl.CompileShaderAndGetError(_vertexShader, VertexShaderSource));
            gl.AttachShader(_shaderProgram, _vertexShader);
        }

        void CreateIndexBuffer(GlInterface gl)
        {
            _ibo = gl.GenBuffer();
            gl.BindBuffer(GL_ELEMENT_ARRAY_BUFFER, _ibo);

            uint[] indices = _modelLoader.Model.Indices.ToArray();
            fixed (void* pIndicesData = indices)
                gl.BufferData(
                    GL_ELEMENT_ARRAY_BUFFER,
                    new IntPtr(indices.Length * sizeof(uint)),
                    new IntPtr(pIndicesData),
                    GL_STATIC_DRAW);

            gl.CheckError();
        }

        protected override void OnOpenGlRender(GlInterface gl, int fb)
        {
            gl.ClearColor(0.1f, 0.1f, 0.1f, 1);
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

            gl.UniformMatrix4fv(_gWorldTransformLoc, 1, false, &worldTransformation);
            gl.UniformMatrix4fv(_gLocalTransformLoc, 1, false, &localTransformation);

            gl.Uniform3f(_gDirectionalLightDirectionLoc, (float)LightDirX, -(float)LightDirY, -(float)LightDirZ);
            gl.Uniform1f(_gDirectionalLightGlobalIntensityLoc, (float)LightGlobalIntensity);
            gl.Uniform1f(_gDirectionalLightAmbientIntensityLoc, (float)LightAmbientIntensity);
            gl.Uniform1f(_gDirectionalLightDiffuseIntensityLoc, (float)LightDiffuseIntensity);
            gl.Uniform1f(_gDirectionalLightSpecularIntensityLoc, (float)LightSpecularIntensity);
            gl.Uniform3f(_gDirectionalLightColorLoc, (float) LightColorRed, (float) LightColorGreen, (float) LightColorBlue);

            gl.Uniform3f(_gCameraDirLoc, _camera.CameraTarget.X, _camera.CameraTarget.Y, _camera.CameraTarget.Z);

            gl.BindVertexArray(_vao);

            int indexOffsetBytes = 0;
            int vertexOffset = 0;
            foreach (Mesh mesh in _modelLoader.Model.Meshes)
            {
                Matrix4x4 meshTransformation = mesh.Transform;
                gl.UniformMatrix4fv(_gMeshTransformLoc, 1, false, &meshTransformation);

                gl.Uniform3f(_gMaterialAmbientColorLoc, mesh.Material.ColorAmbient.X, mesh.Material.ColorAmbient.Y, mesh.Material.ColorAmbient.Z);
                gl.Uniform3f(_gMaterialDiffuseColorLoc, mesh.Material.ColorDiffuse.X, mesh.Material.ColorDiffuse.Y, mesh.Material.ColorDiffuse.Z);
                gl.Uniform3f(_gMaterialSpecularColorLoc, mesh.Material.ColorSpecular.X, mesh.Material.ColorSpecular.Y, mesh.Material.ColorSpecular.Z);

                gl.Uniform1f(_gMaterialShininessLoc, mesh.Material.Shininess);
                gl.Uniform1f(_gMaterialShininessStrengthLoc, mesh.Material.ShininessStrength);

                gl.DrawElementsBaseVertex(GL_TRIANGLES, mesh.IndicesCount, GL_UNSIGNED_INT,  new IntPtr(indexOffsetBytes), vertexOffset);
                indexOffsetBytes += mesh.IndicesCount * sizeof(uint);
                vertexOffset += mesh.PositionsCount;
            }

            gl.CheckError();

            _camera.OnRender();

            gl.BindVertexArray(0);

            if (_pressedKey == Key.None)
                return;

            ProcessInputKey(_pressedKey);
            Dispatcher.UIThread.Post(InvalidateVisual, DispatcherPriority.Background);
        }

        void CreateVertexBuffer(GlInterface gl)
        {
            Vector3[] positions = _modelLoader.Model.Positions.ToArray();
            _positionsVBO = SetAndEnableData(gl, positions, PositionLocation);
            gl.CheckError();

            Vector2[] texCoords = _modelLoader.Model.TexCoords.ToArray();
            _TexCoordVBO = SetAndEnableData(gl, texCoords, TexCoordLocation);
            gl.CheckError();

            Vector3[] normals = _modelLoader.Model.Normals.ToArray();
            _NormalVBO = SetAndEnableData(gl, normals, NormalLocation);
            gl.CheckError();
        }

        static int SetAndEnableData(GlInterface gl, Vector3[] data, int location)
        {
            fixed (void* pData = data)
                return SetAndEnableData(gl, pData, location, data.Length * sizeof(Vector3), 3);
        }

        static int SetAndEnableData(GlInterface gl, Vector2[] data, int location)
        {
            fixed (void* pData = data)
                return SetAndEnableData(gl, pData, location, data.Length * sizeof(Vector2), 2);
        }

        static int SetAndEnableData(GlInterface gl, void* pData, int location, int sizeBytes, int amountFloats)
        {
            int vbo = gl.GenBuffer();
            gl.BindBuffer(GL_ARRAY_BUFFER, vbo);
            gl.CheckError();

            gl.BufferData(
                GL_ARRAY_BUFFER,
                new IntPtr(sizeBytes),
                new IntPtr(pData),
                GL_STATIC_DRAW);
            gl.CheckError();

            gl.VertexAttribPointer(location, amountFloats, GL_FLOAT, GL_FALSE, 0, IntPtr.Zero);
            gl.CheckError();

            gl.EnableVertexAttribArray(location);
            gl.CheckError();

            return vbo;
        }

        string VertexShaderSource =>
            GlExtensions.GetShader(
                GlVersion,
                false,
                ResourceLoader.LoadVertexShader(nameof(Tutorial22) + ".Shaders"));

        string FragmentShaderSource =>
            GlExtensions.GetShader(
                GlVersion,
                true,
                ResourceLoader.LoadFragmentShader(nameof(Tutorial22) + ".Shaders"));

        int _positionsVBO;
        int _TexCoordVBO;
        int _NormalVBO;
        int _vao;
        int _ibo;
        int _vertexShader;
        int _fragmentShader;
        int _shaderProgram;
        int _gLocalTransformLoc;
        int _gWorldTransformLoc;
        int _gMeshTransformLoc;
        int _gCameraDirLoc;
        int _gSamplerLoc;
        int _gMaterialShininessLoc;
        int _gMaterialShininessStrengthLoc;
        int _gDirectionalLightColorLoc;
        int _gDirectionalLightGlobalIntensityLoc;
        int _gDirectionalLightDirectionLoc;
        int _gDirectionalLightAmbientIntensityLoc;
        int _gDirectionalLightDiffuseIntensityLoc;
        int _gDirectionalLightSpecularIntensityLoc;
        int _gMaterialAmbientColorLoc;
        int _gMaterialDiffuseColorLoc;
        int _gMaterialSpecularColorLoc;

        IModelLoader _modelLoader;
        Texture _texture;

        readonly Pipeline _operations = new Pipeline();

        const int PositionLocation = 0;
        const int TexCoordLocation = 1;
        const int NormalLocation = 2;
    }
}