// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using UnityEngine.SceneManagement;

namespace Kitchen.Utils
{
	public enum Scenes
	{
		Game,
		MainMenu,
		Users
	}

	public static class ScenesExtensions
	{
		private const string SCENE = "Scene";

		public static void Load(this Scenes scene)
		{
			SceneManager.LoadScene($"{scene}{SCENE}");
		}
	}
}
