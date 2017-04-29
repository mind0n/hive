using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using XNA = Microsoft.Xna.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaEngine.Winform;

namespace XNAWinForms
{
    /// <summary>
    /// Windows form that inherits from XNAWinForms and adds the rendering of a simple rotating triangle
    /// 
    /// Author: Iñaki Ayucar (http://graphicdna.blogspot.com)
    /// Date: 14/11/2007
    /// 
    /// This software is distributed "for free" for any non-commercial usage. The software is provided “as-is.” 
    /// You bear the risk of using it. The contributors give no express warranties, guarantees or conditions.
    /// </summary>
    public partial class MainForm : Form
    {
        private float mRotation = 0f;
        private Matrix mViewMat, mWorldMat, mProjectionMat;
        private BasicEffect mSimpleEffect;        

        VertexPositionColor[] triVerts = new VertexPositionColor[] {
            new VertexPositionColor(Vector3.Zero*2,
                        Microsoft.Xna.Framework.Color.Blue),
            new VertexPositionColor(Vector3.Right*2,
                        Microsoft.Xna.Framework.Color.Green),
            new VertexPositionColor(Vector3.Up*2,
                        Microsoft.Xna.Framework.Color.Red)};


        /// <summary>
        /// 
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

			this.xnaPanel.DeviceResetting += new XnaPanel.EmptyEventHandler(mWinForm_DeviceResetting);
			this.xnaPanel.DeviceReset += new XnaPanel.GraphicsDeviceDelegate(mWinForm_DeviceReset);
			this.xnaPanel.OnFrameRender += new XnaPanel.GraphicsDeviceDelegate(mWinForm_OnFrameRender);
			this.xnaPanel.OnFrameMove += new XnaPanel.GraphicsDeviceDelegate(Form1_OnFrameMove);

			mViewMat = mWorldMat = mProjectionMat = Matrix.Identity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pDevice"></param>
        void Form1_OnFrameMove(Microsoft.Xna.Framework.Graphics.GraphicsDevice pDevice)
        {
            mRotation += 0.1f;
            this.mWorldMat = Matrix.CreateRotationY(mRotation);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pDevice"></param>
        void mWinForm_OnFrameRender(GraphicsDevice pDevice)
        {
            // Configure effect
            mSimpleEffect.World = this.mWorldMat;
            mSimpleEffect.View = this.mViewMat;
            mSimpleEffect.Projection = this.mProjectionMat;
            mSimpleEffect.DiffuseColor = XNA.Color.DarkRed.ToVector3();
            //mSimpleEffect.CommitChanges();


            // Draw
			mSimpleEffect.CurrentTechnique.Passes[0].Apply();
			//mSimpleEffect.Begin();
            //mSimpleEffect.Techniques[0].Passes[0].Begin();
            pDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList,
                triVerts, 0, 1);
			//mSimpleEffect.Techniques[0].Passes[0].End();
			//mSimpleEffect.End();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pDevice"></param>
        void mWinForm_DeviceReset(GraphicsDevice pDevice)
        {
            // Re-Create effect
            mSimpleEffect = new BasicEffect(pDevice);

            // Configure device
			VertexBuffer vb = new VertexBuffer(pDevice, typeof(VertexPositionColor), 3, BufferUsage.None);
			pDevice.SetVertexBuffer(vb);
            //pDevice.VertexDeclaration = new VertexDeclaration(pDevice, VertexPositionColor.VertexElements);
            //pDevice.RenderState.CullMode = CullMode.None;

            // Create camera and projection matrix
            mWorldMat = Matrix.Identity;
            mViewMat = Matrix.CreateLookAt(Vector3.Backward * 10,Vector3.Zero, Vector3.Up);
            mProjectionMat = Matrix.CreatePerspectiveFieldOfView(MathHelper.Pi / 4.0f,
                    (float)pDevice.PresentationParameters.BackBufferWidth / (float)pDevice.PresentationParameters.BackBufferHeight,
                    1.0f, 100.0f);
        }
        /// <summary>
        /// 
        /// </summary>
        void mWinForm_DeviceResetting()
        {
            // Dispose all
            if (mSimpleEffect != null)
                mSimpleEffect.Dispose();
        }
    }
}