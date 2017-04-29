using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaEngine.Resources
{
	public class SpaceObject
	{
		#region Camera Space Data

		public bool IsRotationUpdated
		{
			get
			{
				if (isRotationUpdated)
				{
					isRotationUpdated = false;
					return true;
				}
				return false;
			}
		}
		public Vector3 YawPitchRoll = new Vector3();
		public Vector3 Position = new Vector3();
		public Vector3 UpDirection = Vector3.Up;
		public Vector3 LookAt = new Vector3();
		public Quaternion Rotation = Quaternion.Identity;

		#region Previous Space Data

		protected Vector3 PrevPosition = new Vector3();
		protected Vector3 PrevYawPitchRoll = new Vector3();

		#endregion Previous Space Data

		#endregion Space Data

		#region Privates

		private bool isRotationUpdated;

		#endregion

		public virtual void UpdateRotation()
		{
			float dx = YawPitchRoll.X - PrevYawPitchRoll.X;
			float dy = YawPitchRoll.Y - PrevYawPitchRoll.Y;
			float dz = YawPitchRoll.Z - PrevYawPitchRoll.Z;

			//Rotation = Quaternion.CreateFromYawPitchRoll(YawPitchRoll.Y.ToRadius(), YawPitchRoll.X.ToRadius(), YawPitchRoll.Z.ToRadius());
			Quaternion dq = Quaternion.CreateFromYawPitchRoll(dy.ToRadius(), dx.ToRadius(), dz.ToRadius());
			Rotation = Quaternion.Multiply(Rotation, dq);
			PrevYawPitchRoll = YawPitchRoll;
			isRotationUpdated = true;
		}
		public void Offset(Vector3 offset)
		{
			Position = GameUtil.Offset(Position, offset, Rotation);
		}
	}

}
