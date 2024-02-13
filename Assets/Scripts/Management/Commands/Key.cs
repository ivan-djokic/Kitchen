// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using UnityEngine;

namespace Kitchen.Management.Commands
{
	public enum Key
	{
		Down,
		Enter,
		Escape,
		Left,
		Right,
		Space,
		Up
	}

	public static class KeyExtensions
	{
		public static KeyCode GetCode(this Key key)
		{
			return key switch
			{
				Key.Down => KeyCode.DownArrow,
				Key.Escape => KeyCode.Escape,
				Key.Enter => KeyCode.Return,
				Key.Left => KeyCode.LeftArrow,
				Key.Right => KeyCode.RightArrow,
				Key.Space => KeyCode.Space,
				Key.Up => KeyCode.UpArrow,
				_ => KeyCode.None
			};
		}
	}
}
