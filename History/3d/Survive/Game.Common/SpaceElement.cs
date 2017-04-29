using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Common
{
	public abstract class SpaceElement
	{
		protected delegate void UpdateHandler(TimeSpan elapsedTime);
		protected delegate void InitHandler();
		protected delegate void LoadContentHandler();
		protected event InitHandler OnInit;
		protected event UpdateHandler OnUpdate;
		protected event LoadContentHandler OnLoadContent;
		protected SpaceElement parent;

		public SpaceElement(SpaceElement parent = null)
		{
			this.parent = parent;
		}

		public virtual void Init()
		{
			if (OnInit != null)
			{
				OnInit();
			}
		}

		public virtual void LoadContent()
		{
			if (OnLoadContent != null)
			{
				OnLoadContent();
			}
		}

		public virtual void Update(TimeSpan elapsedTime)
		{
			if (OnUpdate != null)
			{
				OnUpdate(elapsedTime);
			}
		}
	}
}
