using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace XnaEngine.IO
{
	public class MouseController
	{
		public delegate void MouseEventHandler(MouseState state);
		public event MouseEventHandler OnLeftButtonDown;
		public event MouseEventHandler OnLeftButtonUp;
		public event MouseEventHandler OnRightButtonDown;
		public event MouseEventHandler OnRightButtonUp;

		public MouseState CurrentState { get; set; }
		public MouseState PreviousState { get; set; }
		public bool IsCenterMouse { get; set; }
		public bool InvertX { get; set; }
		public bool InvertY { get; set; }
		public bool IsRightButtonDown
		{
			get
			{
				return Mouse.GetState().RightButton == ButtonState.Pressed;
			}
		}
		public bool IsLeftButtonDown
		{
			get
			{
				return Mouse.GetState().LeftButton == ButtonState.Pressed;
			}
		}

		public bool IsInitialized
		{
			get {
				if (!isInitialized)
				{
					isInitialized = true;
					return false;
				}
				return true;
			}
		}
		public Rectangle WindowRect { get; set; }
		public int OffsetX
		{
			get {
				if (!isUpdating)
				{
					if (InvertX)
					{
						return PreviousState.X - CurrentState.X;
					}
					else
					{
						return CurrentState.X - PreviousState.X;
					}
				}
				return 0;
			}
		}
		public int OffsetY
		{
			get
			{
				if (!isUpdating)
				{
					if (InvertY)
					{
						return PreviousState.Y - CurrentState.Y;
					}
					else
					{
						return CurrentState.Y - PreviousState.Y;
					}
				}
				return 0;
			}
		}
		private bool isUpdating { get; set; }
		private bool isInitialized;

		public MouseController() { }
		public virtual void UpdateState()
		{
			if (!isUpdating)
			{
				isUpdating = true;
				if (!IsInitialized)
				{
					CenterMouse();
					PreviousState = Mouse.GetState();
				}
				else if (!IsCenterMouse)
				{
					PreviousState = CurrentState;
				}

				CurrentState = Mouse.GetState();
				if (IsCenterMouse)
				{
					CenterMouse();
				}
				CheckEvent(CurrentState, PreviousState.LeftButton, CurrentState.LeftButton, OnLeftButtonDown, OnLeftButtonUp);
				CheckEvent(CurrentState, PreviousState.RightButton, CurrentState.RightButton, OnRightButtonDown, OnRightButtonUp);
				isUpdating = false;
			}
		}
		public void CenterMouse()
		{
			if (WindowRect != null)
			{
				int width = WindowRect.Right - WindowRect.Left;
				int height = WindowRect.Bottom - WindowRect.Top;
				Mouse.SetPosition(width / 2, (height - WindowRect.Top) / 2);
				if (IsCenterMouse)
				{
					PreviousState = Mouse.GetState();
				}
			}
		}
		protected void CheckEvent(MouseState state, ButtonState previousState, ButtonState currentState, MouseEventHandler buttonDownEventHandler, MouseEventHandler buttonUpEventHandler)
		{
			MouseEventHandler eventCallback = null;
			if (previousState == ButtonState.Released && currentState == ButtonState.Pressed && buttonDownEventHandler != null)
			{
				eventCallback = buttonDownEventHandler;
			}
			else if (previousState == ButtonState.Pressed && currentState == ButtonState.Released && buttonUpEventHandler != null)
			{
				eventCallback = buttonUpEventHandler;
			}
			else if (previousState == ButtonState.Pressed && currentState == ButtonState.Pressed && buttonDownEventHandler != null)
			{
				eventCallback = buttonDownEventHandler;
			}
			else if (previousState == ButtonState.Released && currentState == ButtonState.Released && buttonUpEventHandler != null)
			{
				eventCallback = buttonUpEventHandler;
			}
			if (eventCallback != null)
			{
				eventCallback(state);
			}
		}
	}
}
