// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Audio;
using Kitchen.Common;
using Kitchen.Management.Administration;
using Kitchen.Management.Dispatchers;
using Kitchen.Objects.Counters;
using Kitchen.Objects.ProgressBar;
using TMPro;
using UnityEngine;

namespace Kitchen.Management.Managers
{
	public class GameManager : UnityObject
	{
		[SerializeField]
		private MoneyManager m_money;
		[SerializeField]
		private TextMeshProUGUI m_level;
		[SerializeField]
		private LivesManager m_lives;
		[SerializeField]
		private Progress m_timerProgress;

		public LivesManager Lives
		{
			get => m_lives;
		}

		public MoneyManager Money
		{
			get => m_money;
		}

		public float Time
		{
			get => m_timerProgress.Current;

			set
			{
				m_timerProgress.Begin(value);
				m_timerProgress.Finished += Game.Instance.GameOver;
				m_timerProgress.Flush += TimerWarning;
			}
		}

		protected override void Start()
		{
			m_level.text = User.Instance.Level.Ordinal.ToString();

			InitializeSpawn(Game.DeliveryManager);
			InitializeSpawn(WashingDishesCounter.Instance);
		}

		private void InitializeSpawn(ISpawnItem spawnItem)
		{
			(spawnItem as UnityObject).InvokeRepeating(spawnItem.SpawnPeriod, spawnItem.Spawn);
		}

		private void TimerWarning(bool show)
		{
			if (!show)
			{
				return;
			}

			SoundManager.Instance.Play(Sounds.Warning);
		}
	}
}
