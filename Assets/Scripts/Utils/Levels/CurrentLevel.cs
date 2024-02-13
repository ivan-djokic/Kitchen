// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

namespace Kitchen.Utils.Levels
{
	public class CurrentLevel
	{
		public CurrentLevel(
			float goldenPlateFrequency,
			float maxKitchenObjectCount,
			int moneyLimit,
			float movementSpeed,
			float orderLifeTimeFactor,
			float orderSpawnTime,
			float plateSpawnTime,
			int time)
		{
			GoldenPlateFrequency = goldenPlateFrequency;
			MaxKitchenObjectCount = maxKitchenObjectCount;
			MoneyLimit = moneyLimit;
			MovementSpeed = movementSpeed;
			OrderLifeTimeFactor = orderLifeTimeFactor;
			OrderSpawnTime = orderSpawnTime;
			PlateSpawnTime = plateSpawnTime;
			Time = time;
		}

		public float GoldenPlateFrequency { get; }

		public float MaxKitchenObjectCount { get; }

		public int MoneyLimit { get; }

		public float MovementSpeed { get; }

		public float OrderLifeTimeFactor { get; }

		public float OrderSpawnTime { get; }

		public float PlateSpawnTime { get; }

		public int Time { get; }
	}
}
