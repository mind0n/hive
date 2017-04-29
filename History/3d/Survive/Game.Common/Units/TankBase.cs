using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Common.Units
{
	public class TankBase : SpaceElement
	{
		protected string bodyImageUrl;
		protected CannonTower tower;
		public TankBase()
		{
			tower = new CannonTower(this);
		}
	}
	public class CannonTower : SpaceElement
	{
		protected string towerImageUrl;
		public CannonTower(SpaceElement parent) : base(parent) { }
	}
}
