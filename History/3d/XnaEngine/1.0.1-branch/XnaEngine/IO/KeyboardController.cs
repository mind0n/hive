using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace XnaEngine.IO
{
	public class KeyboardController
	{
		public delegate void KeyPressDelegate(Keys[] keys, KeyboardState state);
		public event KeyPressDelegate OnKeyPress;
		public void UpdateState()
		{
			KeyboardState state = Keyboard.GetState();
			Keys [] pressedKeys = state.GetPressedKeys();
			if (OnKeyPress != null)
			{
				OnKeyPress(pressedKeys, state);
			}
		}
	}
}
