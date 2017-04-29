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
using XnaEngine.XnaGame;
using XnaEngine.Resources;
using System.Timers;
using System.Threading;
using XnaEngine;

namespace XnaGame
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class GameMain : GameBase
	{
		private const string ModelEntities = "Entities";

		float modelRotation = 0.0f;
		int TotalModels = 1300;
		Vector3 modelPosition = Vector3.Zero;

		public static Texture2D dummyTexture;

		protected override void OnLoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
			SpriteBatch = new SpriteBatch(GraphicsDevice);
			for (int i = 0; i < TotalModels; i++)
			{
				Repository.LoadModel<GameModel>(ModelEntities);
			}

			dummyTexture = new Texture2D(GraphicsDevice, 1, 1);
			dummyTexture.SetData(new Color[] { Color.White });

			StartUpdateThread(Update);
		}

		private void Update()
		{
			modelRotation += (float)ElapsedGameTime.TotalMilliseconds * MathHelper.ToRadians(0.1f);
			int i = -TotalModels / 10; 
			int j = 0;

			foreach (GameModel controller in Repository.Controllers)
			{
				Vector3 pos = new Vector3(modelPosition.X + (j % 10) * 10, modelPosition.Y + i, modelPosition.Z);
				controller.ScreenPosition = GraphicsDevice.Viewport.Project(Vector3.Zero, Camera.PerspectiveMatrix, Camera.View, controller.Transform);
				controller.Update(modelRotation, pos);
				if (j % 10 == 0)
				{
					i += 12;
				}
				j += 1;
			}
		}
	}
}
