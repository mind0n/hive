using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XnaEngine.Scene;
using XnaEngine.XnaGame;

namespace XnaEngine.Resources
{
	public class Box
	{
		public Vector3 Min;
		public Vector3 Max;
		public Vector3 LeftTop;
		public Vector3 RightBottom;
		public int Width;
		public int Height;
		public List<Vector3> GetCorners()
		{
			List<Vector3> rlt = new List<Vector3>();
			rlt.Add(new Vector3(Min.X, Min.Y, Min.Z));
			rlt.Add(new Vector3(Max.X, Min.Y, Min.Z));
			rlt.Add(new Vector3(Max.X, Max.Y, Min.Z));
			rlt.Add(new Vector3(Min.X, Min.Y, Max.Z));

			rlt.Add(new Vector3(Min.X, Max.Y, Min.Z));
			rlt.Add(new Vector3(Max.X, Max.Y, Min.Z));
			rlt.Add(new Vector3(Min.X, Max.Y, Max.Z));
			rlt.Add(new Vector3(Max.X, Max.Y, Max.Z));
			return rlt;
		}
		public Box GetScreenBound(Matrix transform)
		{
			List<Vector3> corners = GetCorners();
			Vector3? min = null, max = null;
			foreach (Vector3 corner in corners)
			{
				Vector3? p = GameUtil.Project(corner, transform);
				if (p != null)
				{
					if (min == null)
					{
						min = p;
					}
					if (max == null)
					{
						max = p;
					}
					if (p.Value.X < min.Value.X)
					{
						min = new Vector3(p.Value.X, min.Value.Y, p.Value.Z);
					}
					if (p.Value.Y < min.Value.Y)
					{
						min = new Vector3(min.Value.X, p.Value.Y, p.Value.Z);
					}
					if (p.Value.X > max.Value.X)
					{
						max = new Vector3(p.Value.X, max.Value.Y, p.Value.Z);
					}
					if (p.Value.Y > max.Value.Y)
					{
						max = new Vector3(max.Value.X, p.Value.Y, p.Value.Z);
					}
				}
				Width = Convert.ToInt32(max.Value.X - min.Value.X);
				Height = Convert.ToInt32(max.Value.Y - min.Value.Y);
				LeftTop = min.Value;
				RightBottom = max.Value;
			}
			return this;
		}
	}
	public static class Extensions
	{
		public static RectangleF GetScreenBound(this BoundingBox box, Matrix transform, Action<Vector3> callback)
		{
			Vector3[] corners = box.GetCorners();
			float minx = float.MaxValue, miny = float.MaxValue, maxx = float.MinValue, maxy = float.MinValue;
			foreach (Vector3 corner in corners)
			{
				Vector3? p = GameUtil.Project(corner, transform);
				if (p != null)
				{
					if (callback != null)
					{
						callback(p.Value);
					}
					if (p.Value.X < minx)
					{
						minx = p.Value.X;
					}
					if (p.Value.Y < miny)
					{
						miny = p.Value.Y;
					}
					if (p.Value.X > maxx)
					{
						maxx = p.Value.X;
					}
					if (p.Value.Y > maxy)
					{
						maxy = p.Value.Y;
					}
				}
			}
			return new RectangleF(minx, miny, maxx - minx, maxy - miny);
		}
	}
	public class RectangleF
	{
		public Vector3 Min;
		public Vector3 Max;
		public float Width
		{
			get
			{
				return Max.X - Min.X;
			}
		}
		public float Height
		{
			get
			{
				return Max.Y - Min.Y;
			}
		}
		public float Left
		{
			get
			{
				return Min.X;
			}
		}
		public float Top
		{
			get
			{
				return Min.Y;
			}
		}
		public float Bottom
		{
			get
			{
				return Max.Y;
			}
		}
		public float Right
		{
			get
			{
				return Max.X;
			}
		}

		#region Integer Property
		public int IntTop
		{
			get
			{
				return (int)Top;
			}
		}
		public int IntWidth
		{
			get
			{
				return (int)Width;
			}
		}
		public int IntLeft
		{
			get
			{
				return (int)Left;
			}
		}
		public int IntHeight
		{
			get
			{
				return (int)Height;
			}
		}
		public int IntBottom
		{
			get
			{
				return (int)Bottom;
			}
		}
		public int IntRight
		{
			get
			{
				return (int)Right;
			}
		}
		#endregion


		public RectangleF(float x, float y, float width, float height)
		{
			Min = new Vector3(x, y, 0);
			Max = new Vector3(x + width, y + height, 0);
		}
		public RectangleF() { }
		public RectangleF CenterAt(Vector3 center)
		{
			float ox = Width / 2, oy = Height / 2;
			Min.X = center.X - ox;
			Min.Y = center.Y - oy;
			Max.X = center.X + ox;
			Max.Y = center.Y + oy;
			return this;
		}
	}

	public class ModelController
	{
		public Vector3 ScreenPosition = new Vector3();
		public Matrix Transform;
		public Model TargetModel
		{
			get { return targetModel; }
			set
			{
				if (value != null)
				{
					targetModel = value;
					bones = new Matrix[targetModel.Bones.Count];
				}
				else
				{
					targetModel = null;
					bones = null;
				}
			}
		}
		public Matrix[] Bones
		{
			get { return bones; }
			set { bones = value; }
		}
		public bool IsRendered;
		public RectangleF ScreenBound
		{
			get
			{
				return BoundingBox.GetScreenBound(Transform, null);
			}
		}
		public BoundingBox BoundingBox
		{
			get {
				if (boundingBox == null)
				{
					boundingBox = CalculateBoundingBox();
				}
				return boundingBox.Value;
			}
			set { boundingBox = value; }
		}
		public bool Visible = true;

		#region Non-public members

		private BoundingBox? boundingBox;
		protected Model targetModel;
		private Matrix[] bones;

		#endregion


		public ModelController(Model model) : this()
		{
			targetModel = model;
			Transform = Matrix.CreateTranslation(0, 0, 0);
		}
		protected ModelController()
		{
		}

		public bool IsInsideCameraView(CameraController camera)
		{
			//Inside your draw method
			ContainmentType currentContainmentType = ContainmentType.Disjoint;

			//For each gameobject
			//(If you have more than one mesh in the model, this wont work. Use BoundingSphere.CreateMerged() to add them together)
			BoundingSphere meshBoundingSphere = targetModel.Meshes[0].BoundingSphere.Transform(Transform);
			currentContainmentType = camera.Frustum.Contains(meshBoundingSphere);
			if (currentContainmentType != ContainmentType.Disjoint)
			{
				return true;
			}
			return false;
		}

		public virtual bool OnRender() { return true; }
		public virtual bool OnRenderModelCompleted() { return true; }
		public BoundingBox CalculateBoundingBox()
		{
			BoundingBox result = new BoundingBox();
			Model m_model = targetModel;
			// Create variables to hold min and max xyz values for the model. Initialise them to extremes
			//Vector3 modelMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			//Vector3 modelMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

			foreach (ModelMesh mesh in m_model.Meshes)
			{
				//Create variables to hold min and max xyz values for the mesh. Initialise them to extremes
				//Vector3 meshMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);
				//Vector3 meshMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
				// There may be multiple parts in a mesh (different materials etc.) so loop through each
				foreach (ModelMeshPart part in mesh.MeshParts)
				{
					BoundingBox? meshPartBoundingBox = GetBoundingBox(part);
					if (meshPartBoundingBox != null)
						result = BoundingBox.CreateMerged(result, meshPartBoundingBox.Value);
				}

				// transform by mesh bone transforms
				//meshMin = Vector3.Transform(meshMin, mesh.ParentBone.Transform);
				//meshMax = Vector3.Transform(meshMax, mesh.ParentBone.Transform);

				// Expand model extents by the ones from this mesh
				//modelMin = Vector3.Min(modelMin, meshMin);
				//modelMax = Vector3.Max(modelMax, meshMax);
			}

			// Create and return the model bounding box
			//BoundingBox box = new BoundingBox(modelMin, modelMax);
			//return box;
			return result;
		}

		public static BoundingBox? GetBoundingBox(ModelMeshPart meshPart)
		{
			VertexElementUsage usage = VertexElementUsage.Position;
			VertexDeclaration vd = meshPart.VertexBuffer.VertexDeclaration;
			VertexElement[] elements = vd.GetVertexElements();

			Func<VertexElement, bool> elementPredicate = ve => ve.VertexElementUsage == usage && ve.VertexElementFormat == VertexElementFormat.Vector3;
			if (!elements.Any(elementPredicate))
				return null;

			VertexElement element = elements.First(elementPredicate);

			Vector3[] vertexData = new Vector3[meshPart.NumVertices];
			meshPart.VertexBuffer.GetData((meshPart.VertexOffset * vd.VertexStride) + element.Offset,
				vertexData, 0, vertexData.Length, vd.VertexStride);

			return BoundingBox.CreateFromPoints(vertexData);
		}

		public static ModelController CreateInstance<T>(Model model) where T:ModelController, new()
		{
			if (model != null)
			{
				T result = new T();
				result.targetModel = model;
				return result;
			}
			return null;
		}


	}
}
