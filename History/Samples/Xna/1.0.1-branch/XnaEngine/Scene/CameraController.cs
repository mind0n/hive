using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaEngine.Resources;
using XnaEngine.XnaGame;

namespace XnaEngine.Scene
{
	public class CameraController : SpaceObject
	{
		#region Camera Basic Data

		public float FieldOfView
		{
			get { return fieldOfView; }
		}
		public float FieldOfViewDegrees { get; set; }
		public float AspectRatio
		{
			get
			{
				return aspectRatio;
			}
		}
		public float NearPlaneDistance { get; set; }
		public float FarPlaneDistance { get; set; }
		public Matrix View
		{
			get { return view; }
		}
		public Matrix PerspectiveMatrix
		{
			get
			{
				return perspectiveMatrix;
			}
		}
		public GraphicsDeviceManager Graphics;
		public BoundingFrustum Frustum
		{
			get
			{
				return frustum;
			}
		}
	
		#endregion

		#region privates
		private float fieldOfView;
		private BoundingFrustum frustum;
		private Matrix view;
		private float aspectRatio;
		private Matrix perspectiveMatrix;
		#endregion

		public CameraController()
		{
		}
		public CameraController(GraphicsDeviceManager graphics)
		{
			Graphics = graphics;
			UpdateFrustum();
		}
		public void Initialize()
		{
			UpdateFrustum();
		}
		public void Translate(Vector3 offset)
		{
			Position.X += offset.X;
			Position.Y += offset.Y;
			Position.Z += offset.Z;
		}
		public void Rotate(Vector3 yawPitchRoll)
		{
			YawPitchRoll.X += yawPitchRoll.X;
			YawPitchRoll.Y += yawPitchRoll.Y;
			YawPitchRoll.Z += yawPitchRoll.Z;
		}
		public void Rotate(float x, float y, float z)
		{
			YawPitchRoll += new Vector3 { X = x, Y = y, Z = z };
		}
		public void UpdateFrustum()
		{
			frustum = null;
			view = CreateLookAt();
			frustum = new BoundingFrustum(view * perspectiveMatrix);
		}
		public Matrix UpdatePerspectiveFieldOfView()
		{
			fieldOfView = MathHelper.ToRadians(FieldOfViewDegrees);
			aspectRatio = (float)Graphics.GraphicsDevice.Viewport.Width /
							(float)Graphics.GraphicsDevice.Viewport.Height;

			perspectiveMatrix = Matrix.CreatePerspectiveFieldOfView
								(fieldOfView
								, aspectRatio
								, NearPlaneDistance
								, FarPlaneDistance);
			UpdateFrustum();
			return perspectiveMatrix;
		}
		public override void UpdateRotation()
		{
			base.UpdateRotation();
			UpdateFrustum();
		}
		private Matrix CreateLookAt()
		{
			Matrix rotation = Matrix.CreateFromQuaternion(Rotation); //Matrix.CreateFromYawPitchRoll((float)(YawPitchRoll.Y * Math.PI / 180), (float)(YawPitchRoll.X * Math.PI / 180), (float)(YawPitchRoll.Z * Math.PI / 180));

			// Offset the position and reset the translation
			Vector3 translation = Vector3.Transform(Vector3.Zero, rotation);

			// Calculate the new target
			Vector3 forward = Vector3.Transform(Vector3.Forward, rotation);
			LookAt = Position + forward;

			// Calculate the up vector
			Vector3 up = Vector3.Transform(UpDirection, rotation);
	
			// Calculate the view matrix
			return Matrix.CreateLookAt(Position, LookAt, up);
		}

	}
}
