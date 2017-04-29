using System;
using System.Collections.Generic;
using System.Text;
using Fs.Entities;
using System.Windows.Media.Media3D;

namespace FsDelta.UI.WPF.Media3D
{
	public class Model3DParams : DictParams
	{
		public double Interval
		{
			get
			{
				return GetValue<double>("$Interval", 0);
			}
			set
			{
				this["$Interval"] = value;
			}
		}

		public double Remains
		{
			get
			{
				return GetValue<double>("$Remains", 0);
			}
			set
			{
				this["$Remains"] = value;
			}
		}
		public Delegate Processor
		{
			get
			{
				return GetValue<Delegate>("$Processor", null);
			}
			set
			{
				this["$Processor"] = value;
			}
		}
		public Point3D OriginPosition
		{
			get
			{
				return GetValue<Point3D>("$OriginPosition", new Point3D());
			}
			set
			{
				this["$OriginPosition"] = value;
			}
		}
		public Vector3D OffsetVector
		{
			get
			{
				return GetValue<Vector3D>("$OffsetVector", new Vector3D());
			}
			set
			{
				this["$OffsetVector"] = value;
			}
		}
		public double StepX
		{
			get
			{
				return GetValue<double>("$StepX", 0);
			}
			set
			{
				this["$Stepx"] = value;
			}
		}
		public double StepY
		{
			get
			{
				return GetValue<double>("$StepY", 0);
			}
			set
			{
				this["$StepY"] = value;
			}
		}
		public double StepZ
		{
			get
			{
				return GetValue<double>("$StepZ", 0);
			}
			set
			{
				this["$StepZ"] = value;
			}
		}
		public Point3D CenterPoint
		{
			get
			{
				return GetValue<Point3D>("$CenterPoint", new Point3D());
			}
			set
			{
				this["$CenterPoint"] = value;
			}
		}
	}
}
