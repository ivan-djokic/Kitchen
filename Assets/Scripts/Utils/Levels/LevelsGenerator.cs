// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

namespace Kitchen.Utils.Levels
{
	public static class LevelsGenerator
	{
		public static CurrentLevel GetLevel(int level)
		{
			return level switch
			{
				1 => new CurrentLevel(0.3f, 9.001f, 50, 5, 4, 20, 5, 420),
				2 => new CurrentLevel(0.3f, 9.001f, 100, 5.1f, 3.5f, 18, 6, 390),
				3 => new CurrentLevel(0.25f, 8.001f, 170, 5.3f, 3, 16, 8, 360),
				4 => new CurrentLevel(0.25f, 8.001f, 250, 5.5f, 2.5f, 14, 10, 330),
				5 => new CurrentLevel(0.2f, 7.001f, 350, 5.8f, 2, 12, 13, 300),
				6 => new CurrentLevel(0.2f, 7.001f, 470, 6.1f, 1.5f, 10, 16, 270),
				_ => new CurrentLevel(0.15f, 6.001f, 600, 6.4f, 1, 8, 20, 240),
			};
		}
	}
}
