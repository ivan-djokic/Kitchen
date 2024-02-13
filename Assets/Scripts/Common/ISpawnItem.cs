// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

namespace Kitchen.Common
{
	public interface ISpawnItem
	{
		float SpawnPeriod { get; }

		void Spawn();
	}
}
