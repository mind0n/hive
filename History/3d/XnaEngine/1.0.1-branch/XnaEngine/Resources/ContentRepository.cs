using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;

namespace XnaEngine.Resources
{
	public class ContentRepository : ContentManager
	{
		public List<ModelController> Controllers = new List<ModelController>();
		public object this[string key]
		{
			get
			{
				return Load<object>(key);
			}
		}

		public ModelController LoadModel(string assetName)
		{
			ModelController result = new ModelController(Load<Model>(assetName));
			Controllers.Add(result);
			return result;
		}

		public T LoadModel<T>(string assetName) where T : ModelController, new()
		{
			ModelController result = ModelController.CreateInstance<T>(Load<Model>(assetName));
			Controllers.Add(result);
			return result as T;
		}

		public T LoadCopyOf<T>(string assetName, Action<IDisposable> disposeCallback)
		{
			return ReadAsset<T>(assetName, disposeCallback);
		}

		public ContentRepository(System.IServiceProvider services)
			: base(services)
		{
		}
	}
}
