// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using System.Runtime.Serialization;
using Kitchen.Management.Administration;
using Kitchen.Management.Dispatchers;

namespace Kitchen.Utils.Levels
{
	[DataContract]
	public class Level
	{
		public const int FIRST_LEVEL = 1;
		private const string RECORD_ICON = " <sprite=7>";

		private CurrentLevel m_currentLevel = LevelsGenerator.GetLevel(FIRST_LEVEL);
		[DataMember]
		private int m_ordinal = FIRST_LEVEL;
		private int m_previousMoneyBalance;

		public Action Changed { get; set; }

		public int EarnedMoney
		{
			get => MoneyBalance - m_previousMoneyBalance;
		}

		public float GoldenPlateFrequency
		{
			get => m_currentLevel.GoldenPlateFrequency;
		}

		[DataMember]
		public Groceries Groceries { get; set; } = new();

		public float MaxKitchenObjectCount
		{
			get => m_currentLevel.MaxKitchenObjectCount;
		}

		[DataMember]
		public int MoneyBalance { get; set; }

		public int MoneyLimit
		{
			get => m_currentLevel.MoneyLimit;
		}

		public float MovementSpeed
		{
			get => m_currentLevel.MovementSpeed;
		}

		public float OrderLifeTimeFactor
		{
			get => m_currentLevel.OrderLifeTimeFactor;
		}

		public float OrderSpawnTime
		{
			get => m_currentLevel.OrderSpawnTime;
		}

		public int Ordinal
		{
			get => m_ordinal;

			set
			{
				m_previousMoneyBalance = MoneyBalance;
				m_ordinal = value;
				m_currentLevel = LevelsGenerator.GetLevel(m_ordinal);

				Changed?.Invoke();
			}
		}

		public float PlateSpawnTime
		{
			get => m_currentLevel.PlateSpawnTime;
		}

		[DataMember]
		public int Points { get; set; }

		public int Time
		{
			get => m_currentLevel.Time;
		}

		public string VisualPoints
		{
			get => Points == User.Instance.Record ? User.Instance.VisualRecord : $"{Points} ({User.Instance.VisualRecord})";
		}

		public void CalculatePoints()
		{
			var gainPoints = EarnedMoney * Ordinal * (1 - Game.Manager.Time);
			User.Instance.Record = Points += (int)gainPoints;
		}

		[OnDeserialized]
		private void SetDefaults(StreamingContext context)
		{
			Ordinal = m_ordinal;
		}
	}
}
