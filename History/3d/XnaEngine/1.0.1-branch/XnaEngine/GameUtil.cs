using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XnaEngine.XnaGame;
using Microsoft.Xna.Framework.Graphics;
using XnaEngine.Scene;

namespace XnaEngine
{
	public static class GameUtil
	{
		public static Vector3 Offset(Vector3 point, Vector3 offset, float yaw, float pitch, float roll)
		{
			Matrix rotation = Matrix.CreateFromYawPitchRoll((float)(yaw * Math.PI / 180), (float)(pitch * Math.PI / 180), (float)(roll * Math.PI / 180));
			offset = Vector3.Transform(offset, rotation);
			Vector3 rlt = new Vector3(point.X + offset.X, point.Y + offset.Y, point.Z + offset.Z);
			return rlt;
		}
		public static Vector3 Offset(Vector3 point, Vector3 offset, Quaternion q)
		{
			Matrix rotation = Matrix.CreateFromQuaternion(q);
			offset = Vector3.Transform(offset, rotation);
			Vector3 rlt = new Vector3(point.X + offset.X, point.Y + offset.Y, point.Z + offset.Z);
			return rlt;
		}
		public static Vector3 Offset(Vector3 point, Vector3 offset, Vector3 YPR)
		{
			Matrix rotation = Matrix.CreateFromYawPitchRoll((float)(YPR.Y * Math.PI / 180), (float)(YPR.X * Math.PI / 180), (float)(YPR.Z * Math.PI / 180));
			offset = Vector3.Transform(offset, rotation);
			Vector3 rlt = new Vector3(point.X + offset.X, point.Y + offset.Y, point.Z + offset.Z);
			return rlt;
		}
		public static Vector3 Offset(Vector3 point, Vector3 offset)
		{
			Vector3 rlt = new Vector3(point.X + offset.X, point.Y + offset.Y, point.Z + offset.Z);
			return rlt;
		}
		public static Vector3? Project(Vector3 point, Matrix transform)
		{
			GraphicsDevice device = GameController.CurrentGame.GraphicsDevice;
			CameraController camera = GameController.CurrentGame.Camera;
			if (device != null)
			{
				return device.Viewport.Project(point, camera.PerspectiveMatrix, camera.View, transform);
			}
			return null;
		}
		public static float ToRadius(this float angle)
		{
			return (float)(angle * Math.PI / 180);
		}
	}
}
