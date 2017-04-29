using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Media3D;
using Fs.Entities;
using Fs.Native.Windows;
using System.Windows.Threading;
using System.Windows.Markup;
using System.IO;

namespace FsDelta.UI.WPF.Media3D
{
	public class UnitVisual3D : ModelVisual3D
	{
		public class ModelAgent : MessageQueue
		{
			public delegate void VoidDictInvokes(object sender, Model3DParams mp);
			public bool IsHostReady
			{
				get
				{
					return hostVisual != null;
				}
			}
			protected UnitVisual3D hostVisual;
			protected Transform3D bakTransform;
			//protected Model3DGroup hostGroup;
			//public ModelAgent(Model3DGroup group)
			//{
			//    hostGroup = group;
			//    hostVisual = new UnitVisual3D();
			//    hostVisual.Content = group;
			//}
			public ModelAgent(UnitVisual3D unit)
			{
				hostVisual = unit;
			}
			public FrequentMessage OffsetTo(Point3D targetPos, double duration, bool isParallel)
			{
				Vector3D d = targetPos - hostVisual.Position;
				return Offset(d.X, d.Y, d.Z, duration, isParallel);
			}
			public FrequentMessage Offset(double x, double y, double z, double duration, bool isParallel)
			{
				if (IsHostReady)
				{
					FrequentMessage msg = Create(hostVisual.Invoke, null, 0, isParallel);
					Model3DParams mp = new Model3DParams();
					mp.OffsetVector = new Vector3D(x / duration * msg.Interval, y / duration * msg.Interval, z / duration * msg.Interval);
					mp.OriginPosition = hostVisual.OriginPosition;
					mp.Remains = duration;
					mp.Interval = msg.Interval;
					mp.Processor = new VoidDictInvokes(ProceedOffset);
					msg.Parameters = mp;
					return (FrequentMessage)Add(msg);
				}
				return null;
			}
			public FrequentMessage Rotate(double axisX, double axisY, double axisZ, double duration, bool isParallel)
			{
				if (IsHostReady)
				{
					FrequentMessage msg = Create(hostVisual.Invoke, null, 0, isParallel);
					Model3DParams mp = new Model3DParams();
					msg.Parameters = mp;
					mp.StepX = axisX / duration * msg.Interval;
					mp.StepY = axisY / duration * msg.Interval;
					mp.StepZ = axisZ / duration * msg.Interval;
					mp.Interval = msg.Interval;
					mp.Remains = duration;
					mp.Processor = new VoidDictInvokes(ProceedRotate);
					return (FrequentMessage)Add(msg);
				}
				return null;
			}
			void ProceedOffset(object sender, Model3DParams mp)
			{
				ParamTimer tmr = (ParamTimer)sender;
				if (IsHostReady)
				{
					double remains = mp.Remains;
					double interval = mp.Interval;
					mp.Remains = remains - interval;
					FrequentMessage msg = (FrequentMessage)tmr.Param;
					hostVisual.Transform = MatrixHelper.Offset(hostVisual.Transform, mp.OffsetVector.X, mp.OffsetVector.Y, mp.OffsetVector.Z);
					if (remains > interval)
					{
						tmr.Start();
					}
					else
					{
						msg.Release();
					}
				}
			}
			void ProceedRotate(object sender, Model3DParams mp)
			{
				ParamTimer tmr = (ParamTimer)sender;
				if (IsHostReady)
				{
					double remains = mp.Remains;
					double interval = mp.Interval;
					mp.Remains = remains - interval;
					FrequentMessage msg = (FrequentMessage)tmr.Param;
					hostVisual.Transform = MatrixHelper.Rotate3D(hostVisual.Transform, mp.StepX, mp.StepY, mp.StepZ, hostVisual.OriginPosition, hostVisual.OriginUpDir, hostVisual.OriginLookDir, MatrixHelper.RotationType.LockAxisY);
					if (remains > interval)
					{
						tmr.Start();
					}
					else
					{
						msg.Release();
					}
				}
			}
		}
		public Point3D Position
		{
			get
			{
				return Transform.Transform(OriginPosition);
			}
		}
		public Vector3D LookDir
		{
			get
			{
				return Transform.Transform(OriginLookDir);
			}
		}
		public Vector3D UpDir
		{
			get
			{
				return Transform.Transform(OriginUpDir);
			}
		}
		public Point3D OriginPosition = MatrixHelper.O;
		public Vector3D OriginLookDir = MatrixHelper.XAxis;
		public Vector3D OriginUpDir = MatrixHelper.YAxis;
		public ModelAgent Agent;
		public Model3DGroup Load(string path)
		{
			object oVisual = XamlReader.Load
				(new FileStream
				(path, FileMode.Open));
				//(@"..\..\3DModules\Fighter.xaml", FileMode.Open));
			Model3DGroup grp = oVisual as Model3DGroup;
			Content = grp;
			return grp;
		}
		public void Invoke(object sender, System.Timers.ElapsedEventArgs notUsed)
		{
			ParamTimer tmr = sender as ParamTimer;
			tmr.Stop();
			Model3DParams mp = tmr.DictParams as Model3DParams;
			Delegate method = mp.Processor;
			Dispatcher.BeginInvoke(method, new object[] { tmr, mp });
		}

		public UnitVisual3D()
			: base()
		{
			Init(new Point3D(0, 0, 0), new Vector3D(0, 0, -1), new Vector3D(0, 1, 0));
		}
		protected void Init(Point3D pos, Vector3D look, Vector3D up)
		{
			OriginPosition = pos;
			OriginLookDir = look;
			OriginUpDir = up;
			Agent = new ModelAgent(this);
		}
	}
}
