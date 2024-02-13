// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

namespace Kitchen.Management.Commands
{
	public enum Command
	{
		Exit,
		HorizontalNext,
		HorizontalPrevious,
		Select,
		VerticalNext,
		VerticalPrevious
	}

	public static class ControlExtensions
	{
		public static Command GetControl(this Key key)
		{
			return key switch
			{
				Key.Down => Command.VerticalNext,
				Key.Escape => Command.Exit,
				Key.Left => Command.HorizontalPrevious,
				Key.Right => Command.HorizontalNext,
				Key.Up => Command.VerticalPrevious,
				_ => Command.Select
			};
		}
	}
}
