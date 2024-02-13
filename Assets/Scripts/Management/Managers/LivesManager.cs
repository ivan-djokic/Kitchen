// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Audio;
using Kitchen.Common;
using Kitchen.Management.Dispatchers;
using UnityEngine;

namespace Kitchen.Management.Managers
{
	public class LivesManager : UnityObject
	{
		private const int DEAD_ICON = 1;

		private int m_lives;

		private GameObject CurrentLive
		{
			get => transform.GetChild(m_lives).GetChild(DEAD_ICON).gameObject;
		}

		public void Decrease()
		{
			m_lives--;
			CurrentLive.SetActive(true);
			SoundManager.Instance.Play(Sounds.DeliveryFail);

			if (m_lives > 0)
			{
				return;
			}

			Game.Instance.GameOver();
		}

		protected override void Start()
		{
			m_lives = transform.childCount;
		}
	}
}
