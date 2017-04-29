using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XnaEngine.Resources;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XnaEngine.Scene;
using System.Threading;
using XnaEngine.IO;
using XnaEngine.Threads;
using XnaEngine.Scene.Rendering;

namespace XnaEngine.XnaGame
{
	public class GameBase : Game
	{
		private const int ThreadSleepInterval = 1;
		public delegate void RenderHandler(ModelController modelController);
		public event RenderHandler OnRenderModelCompleted;
		public bool IsGameRunning = true;

		public bool IsGameRendering
		{
			get { return isGameRendering; }
		}

		public bool IsGameUpdated
		{
			get { return isGameUpdated; }
		}
		public SpriteBatch SpriteBatch;

		public GraphicsDeviceManager Graphics;
		public List<ModelController> VisibleModels
		{
			get { return visibleModels; }
		}
		public TimeSpan TotalElapsedGameTime;
		public TimeSpan ElapsedGameTime;
		public ContentRepository Repository
		{
			get 
			{
				return Content as ContentRepository;
			}
		}
		public CameraController Camera;
		public MouseController Mouse;
		public KeyboardController Keyboard;
		public DrawingContext Renderer;

		private bool isGameRendering;
		private bool isGameUpdated;
		private DateTime lastGameTime;
		private DateTime gameStartupTime;
		private List<ModelController> visibleModels = new List<ModelController>();
		private Thread updateThread;

		public GameBase()
		{
			GameController.CurrentGame = this;
			IsMouseVisible = true;
			Graphics = new GraphicsDeviceManager(this);
			Content = new ContentRepository(Services);
			Content.RootDirectory = "Content";
			ResetGameTime();
			Camera = new CameraController { Graphics = Graphics, LookAt = Vector3.Zero, Position = new Vector3(0, 0, 15), FieldOfViewDegrees = 45f, FarPlaneDistance=10000, NearPlaneDistance=1 };
			Window.AllowUserResizing = true;
			Window.ClientSizeChanged += new EventHandler<EventArgs>(OnWindowClientSizeChanged);
			Mouse = new MouseController { WindowRect = Window.ClientBounds, IsCenterMouse = false, InvertX = true, InvertY = true };
			Mouse.OnRightButtonDown += new MouseController.MouseEventHandler(OnMouseRightButtonDown);
			Mouse.OnRightButtonUp += new MouseController.MouseEventHandler(OnMouseRightButtonUp);
			Keyboard = new KeyboardController();
			Keyboard.OnKeyPress += new KeyboardController.KeyPressDelegate(OnGameBaseKeyPress);
			//visibleModelCleanerThread.Worker = CleanVisibleModelsList;
		}
		protected override void LoadContent()
		{
			Camera.Initialize();
			Renderer = new DrawingContext(GraphicsDevice);
			OnLoadContent();
		}
		protected virtual void OnLoadContent() { }
		protected virtual void OnMouseRightButtonUp(MouseState state)
		{
			IsMouseVisible = true;
			Mouse.IsCenterMouse = false;
		}
		protected virtual void OnMouseRightButtonDown(MouseState state)
		{
			IsMouseVisible = false;
			Mouse.IsCenterMouse = true;
		}
		protected virtual void OnKeyPress(Keys[] keys, KeyboardState state) {
			float translateOffsetUnit = 0.1f, rotateOffsetUnit = 1f;
			if (state.IsKeyDown(Keys.Up) || state.IsKeyDown(Keys.W))
			{
				Camera.Position = GameUtil.Offset(Camera.Position, new Vector3 { X = 0, Y = 0, Z = -1 * translateOffsetUnit }, Camera.Rotation);
			}
			if (state.IsKeyDown(Keys.Down) || state.IsKeyDown(Keys.S))
			{
				Camera.Position = GameUtil.Offset(Camera.Position, new Vector3 { X = 0, Y = 0, Z = translateOffsetUnit }, Camera.Rotation);
			}
			if (state.IsKeyDown(Keys.Left) || state.IsKeyDown(Keys.A))
			{
				Camera.Position = GameUtil.Offset(Camera.Position, new Vector3 { X = -1 * translateOffsetUnit, Y = 0, Z = 0 }, Camera.Rotation);
			}
			if (state.IsKeyDown(Keys.Right) || state.IsKeyDown(Keys.D))
			{
				Camera.Position = GameUtil.Offset(Camera.Position, new Vector3 { X = translateOffsetUnit, Y = 0, Z = 0 }, Camera.Rotation);
			}
			if (state.IsKeyDown(Keys.Q))
			{
				Camera.YawPitchRoll.Z += rotateOffsetUnit;
			}
			if (state.IsKeyDown(Keys.E))
			{
				Camera.YawPitchRoll.Z += -1 * rotateOffsetUnit;
			}
			if (state.IsKeyDown(Keys.K))
			{
				Camera.YawPitchRoll.X += rotateOffsetUnit;
			}
			if (state.IsKeyDown(Keys.I))
			{
				Camera.YawPitchRoll.X += -1 * rotateOffsetUnit;
			}
			if (state.IsKeyDown(Keys.J))
			{
				Camera.YawPitchRoll.Y += rotateOffsetUnit;
			}
			if (state.IsKeyDown(Keys.L))
			{
				Camera.YawPitchRoll.Y += -1 * rotateOffsetUnit;
			}
			Camera.UpdateRotation();
		}
		protected override void OnExiting(object sender, EventArgs args)
		{
			IsGameRunning = false;
			base.OnExiting(sender, args);
		}
		protected Model Models(string assetName)
		{
			object asset = (Content as ContentRepository)[assetName];
			if (asset != null)
			{
				return asset as Model;
			}
			return null;
		}
		protected void StartUpdateThread(Action updateCallback)
		{
			ParameterizedThreadStart ts = new ParameterizedThreadStart(UpdateGame);
			updateThread = new Thread(ts);
			updateThread.Start(updateCallback);
		}
		protected virtual void Render()
		{
			Graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
			//visibleModelCleanerThread.StartNew("VisibleModelListCleaner", visibleModels);
			//wildModels.Add(visibleModels);
			//visibleModels = new List<ModelController>();
			visibleModels.Clear();
			foreach (ModelController model in Repository.Controllers)
			{
				if (model.Visible && model.IsInsideCameraView(Camera))
				{
					if ((model.ScreenBound.Width > 15 || model.ScreenBound.Height > 15) && model.OnRender())
					{
						Renderer.RenderModel(model);
					}
					model.IsRendered = true;
					visibleModels.Add(model);
				}
			}
			Renderer.Begin();
			foreach (ModelController model in visibleModels)
			{
				if (model.OnRenderModelCompleted() && OnRenderModelCompleted != null)
				{
					OnRenderModelCompleted(model);
				}
			}
			Renderer.End();
		}
		protected void UpdateGame(object updateCallback)
		{
			Action updateCallbackMethod = updateCallback as Action;

			while (IsGameRunning)
			{
				if (isGameRendering)
				{
					isGameUpdated = false;
					Thread.Sleep(ThreadSleepInterval);
					continue;
				}
				UpdateGameTime();
				if (updateCallbackMethod != null)
				{
					updateCallbackMethod();
				}
				else
				{
					break;
				}
				isGameUpdated = true;
				Thread.Sleep(ThreadSleepInterval);
			}

		}
		protected override bool BeginDraw()
		{
			if (!IsGameUpdated)
			{
				isGameRendering = false;
				return false;
			}
			else
			{
				isGameRendering = true;
			}
			Mouse.UpdateState();
			Keyboard.UpdateState();
			Camera.UpdatePerspectiveFieldOfView();

			if (Mouse.IsRightButtonDown)
			{
				if (Math.Abs(Mouse.OffsetY) >= 0)
				{
					Camera.YawPitchRoll.X += (float)(Mouse.OffsetY) / 50;
				}
				if (Math.Abs(Mouse.OffsetX) >= 0)
				{
					Camera.YawPitchRoll.Y += (float)(Mouse.OffsetX) / 50;
				}
				Camera.UpdateRotation();
			}

			Render();
			GraphicsDevice.Present();
			isGameRendering = false;
			return false;
		}
		private void ResetGameTime()
		{
			gameStartupTime = DateTime.Now;
			lastGameTime = DateTime.Now;
		}
		private void UpdateGameTime()
		{
			TotalElapsedGameTime = DateTime.Now - gameStartupTime;
			ElapsedGameTime = DateTime.Now - lastGameTime;
			lastGameTime = DateTime.Now;
		}
		private void OnGameBaseKeyPress(Keys[] keys, Microsoft.Xna.Framework.Input.KeyboardState state)
		{
			if (state.IsKeyDown(Keys.F12))
			{
				IsGameRunning = false;
				Exit();
			}
			else if (keys.Length > 0)
			{
				OnKeyPress(keys, state);
			}
		}
		private void OnWindowClientSizeChanged(object sender, EventArgs e)
		{
			Mouse.WindowRect = Window.ClientBounds;
		}
		private ThreadResult CleanVisibleModelsList(ThreadParameter parameter)
		{
			List<ModelController> visibleModels = parameter.Argument<List<ModelController>>(0);
			if (visibleModels != null)
			{
				visibleModels.Clear();
			}
			return null;
		}
	}
}
