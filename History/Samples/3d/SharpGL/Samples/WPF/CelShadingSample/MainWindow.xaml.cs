using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SharpGL;
using SharpGL.SceneGraph.Primitives;
using SharpGL.SceneGraph.Shaders;
using SharpGL.SceneGraph;
using System.Runtime.InteropServices;

namespace CelShadingSample
{
    public struct ShaderUniforms
    {
        public int Projection;
        public int Modelview;
        public int NormalMatrix;
        public int LightPosition;
        public int AmbientMaterial;
        public int DiffuseMaterial;
        public int SpecularMaterial;
        public int Shininess;
    };

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private uint attrPosition = 0;
        private uint attrNormal = 1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;	

            gl.ClearColor(0f, 0f, 0f, 1f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.UseProgram(shaderProgram.ProgramObject);

            gl.Uniform3(toonUniforms.DiffuseMaterial, 0f, 0.75f, 0.75f);
            gl.Uniform3(toonUniforms.AmbientMaterial, 0.04f, 0.04f, 0.04f);
            gl.Uniform3(toonUniforms.SpecularMaterial, 0.5f, 0.5f, 0.5f);
            gl.Uniform1(toonUniforms.Shininess, 50f);
    
            float[] lightPosition = new float[4] { 0.25f, 0.25f, 1f, 0f };
            gl.Uniform3((int)toonUniforms.LightPosition, 1, lightPosition);
    
            gl.UniformMatrix4(toonUniforms.Projection, 1, false, projection.AsColumnMajorArrayFloat);
            gl.UniformMatrix4(toonUniforms.Modelview, 1, false, modelView.AsColumnMajorArrayFloat);
            gl.UniformMatrix3(toonUniforms.NormalMatrix, 1, false, normalMatrix.AsColumnMajorArrayFloat);
    
            gl.BindBuffer(OpenGL.GL_ARRAY_BUFFER,vertexBuffer);
            gl.BindBuffer(OpenGL.GL_ELEMENT_ARRAY_BUFFER, indexBuffer);
    
            gl.EnableVertexAttribArray(attrPosition);
            gl.EnableVertexAttribArray(attrNormal);
    
            gl.VertexAttribPointer(attrPosition, 3, OpenGL.GL_FLOAT, false, Marshal.SizeOf(typeof(Vertex)), IntPtr.Zero);
            int normalOffset = Marshal.SizeOf(typeof(Vertex));
            gl.VertexAttribPointer(attrNormal, 3, OpenGL.GL_FLOAT, false, Marshal.SizeOf(typeof(Vertex)), IntPtr.Add(new IntPtr(0), normalOffset));

            gl.DrawElements(OpenGL.GL_TRIANGLES, trefoilKnot.IndexCount, OpenGL.GL_UNSIGNED_SHORT, IntPtr.Zero);
        }

        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            OpenGL gl = args.OpenGL;

          /* RenderContext& rc = GlobalRenderContext;

    glswInit();
    glswSetPath("../", ".glsl");
    glswAddDirectiveToken("GL3", "#version 130");*/

            vertexBuffer = trefoilKnot.CreateVertexNormalBuffer(gl);
            indexBuffer = trefoilKnot.CreateIndexBuffer(gl);

            //  Create a shader program.
            shaderProgram.CreateInContext(gl);

            //  Create the vertex program.
            vertexShader.CreateInContext(gl);
            vertexShader.LoadSource("PerPixelLightingVertex.glsl");
            vertexShader.Compile();

            var compileStatus = vertexShader.CompileStatus;
            var info = vertexShader.InfoLog;

            //  Create the fragment program.
            fragmentShader.CreateInContext(gl); 
            fragmentShader.LoadSource("PerPixelLightingFragment.glsl");
            fragmentShader.Compile();

            //  Attach the shaders to the program.
            shaderProgram.AttachShader(vertexShader);
            shaderProgram.AttachShader(fragmentShader);
            shaderProgram.Link();
    /*
#if defined(__APPLE__)
    rc.ToonHandle = BuildProgram("Toon.Vertex.GL2", "Toon.Fragment.GL2");
#else
    rc.ToonHandle = BuildProgram("Toon.Vertex.GL3", "Toon.Fragment.GL3");
#endif*/

            toonUniforms.Projection = gl.GetUniformLocation(shaderProgram.ProgramObject, "Projection");
            toonUniforms.Modelview = gl.GetUniformLocation(shaderProgram.ProgramObject, "Modelview");
            toonUniforms.NormalMatrix = gl.GetUniformLocation(shaderProgram.ProgramObject, "NormalMatrix");
            toonUniforms.LightPosition = gl.GetUniformLocation(shaderProgram.ProgramObject, "LightPosition");
            toonUniforms.AmbientMaterial = gl.GetUniformLocation(shaderProgram.ProgramObject, "AmbientMaterial");
            toonUniforms.DiffuseMaterial = gl.GetUniformLocation(shaderProgram.ProgramObject, "DiffuseMaterial");
            toonUniforms.SpecularMaterial = gl.GetUniformLocation(shaderProgram.ProgramObject, "SpecularMaterial");
            toonUniforms.Shininess = gl.GetUniformLocation(shaderProgram.ProgramObject, "Shininess");

            gl.Enable(OpenGL.GL_DEPTH_TEST);
        }
        /*
        private void OpenGLControl_Resized(object sender, OpenGLEventArgs args)
        {

    RenderContext& rc = GlobalRenderContext;

    static float theta = 0;
    static float time = 0;
    const float InitialPause = 0;
    const bool LoopForever = true;
    time += elapsedMilliseconds;
    if (time > InitialPause && (LoopForever || theta < 360))
    {
        theta += elapsedMilliseconds * 0.1f;
    }

    mat4 rotation = mat4::Rotate(theta, vec3(0, 1, 0));
    mat4 translation = mat4::Translate(0, 0, -7);

    rc.Modelview = rotation * translation;
    rc.NormalMatrix = rc.Modelview.ToMat3();

    const float S = 0.46f;
    const float H = S * ViewportHeight / ViewportWidth;
    rc.Projection = mat4::Frustum(-S, S, -H, H, 4, 10);
        }*/

        private TrefoilKnot trefoilKnot = new TrefoilKnot();

        private uint vertexBuffer = 0;
        private uint indexBuffer = 0;
        private ShaderUniforms toonUniforms = new ShaderUniforms();
        private ShaderProgram shaderProgram = new ShaderProgram();
        private VertexShader vertexShader = new VertexShader();
        private FragmentShader fragmentShader = new FragmentShader();
        private SharpGL.SceneGraph.Matrix modelView = new SharpGL.SceneGraph.Matrix(4, 4);
        private SharpGL.SceneGraph.Matrix projection = new SharpGL.SceneGraph.Matrix(4, 4);
        private SharpGL.SceneGraph.Matrix normalMatrix = new SharpGL.SceneGraph.Matrix(3, 3);
    }
}
