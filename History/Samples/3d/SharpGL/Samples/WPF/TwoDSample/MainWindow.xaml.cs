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
using SharpGL.Enumerations;

namespace TwoDSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void openGLControl1_OpenGLDraw(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            //  Get the OpenGL instance.
            var gl = args.OpenGL;

            gl.Color(1f, 0f, 0f);
            gl.PointSize(2.0f);

            //  Draw 10000 random points.
            gl.Begin(BeginMode.Points);
            Random random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                gl.Vertex(random.Next(100, 500), random.Next(100, 500), random.Next(-100, 100));
            }
            

            gl.End();
        }

        private void openGLControl1_OpenGLInitialized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {

        }

        private void openGLControl1_Resized(object sender, SharpGL.SceneGraph.OpenGLEventArgs args)
        {
            //  Get the OpenGL instance.
            var gl = args.OpenGL;

            //  Create an orthographic projection.
            gl.MatrixMode(MatrixMode.Projection);
            gl.LoadIdentity();
            gl.Ortho(0, openGLControl1.ActualWidth, openGLControl1.ActualHeight, 0, -10, 10);

            //  Back to the modelview.
            gl.MatrixMode(MatrixMode.Modelview);
        }
    }
}
