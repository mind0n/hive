using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XnaEngine.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XnaEngine.XnaGame;
using XnaEngine.Scene;
using XnaEngine;

namespace XnaGame
{
	class GameModel : ModelController
	{

		public GameModel():base() {
		}

		public void Update(params object[] e)
		{
			float modelRotation = (float)e[0];
			Transform = Matrix.CreateRotationY(modelRotation)
				* Matrix.CreateTranslation((Vector3)e[1]);
		}

		public override bool OnRenderModelCompleted()
		{
			RectangleF rect = ScreenBound;

			#region Comments: Origin Code
			//GameController.CurrentGame.SpriteBatch.Draw(GameMain.dummyTexture, new Rectangle(rect.Left, rect.Top, rect.Width, 1), Color.White);
			//GameController.CurrentGame.SpriteBatch.Draw(GameMain.dummyTexture, new Rectangle(rect.Left, rect.Top, 1, rect.Height), Color.White);
			//GameController.CurrentGame.SpriteBatch.Draw(GameMain.dummyTexture, new Rectangle(rect.Right, rect.Top, 1, rect.Height), Color.White);
			//GameController.CurrentGame.SpriteBatch.Draw(GameMain.dummyTexture, new Rectangle(rect.Left, rect.Bottom, rect.Width, 1), Color.White);
			#endregion

			GameController.CurrentGame.Renderer.DrawRectangle
				(rect.Left, rect.Top, rect.Width, rect.Height, Color.White);
			
			return false;
		}  
	}
}
