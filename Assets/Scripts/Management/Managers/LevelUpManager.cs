// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Audio;
using Kitchen.Common;
using Kitchen.Management.Administration;
using Kitchen.Management.Dispatchers;
using TMPro;
using UnityEngine;

namespace Kitchen.Management.Managers
{
	public class LevelUpManager : UnityObject
	{
		private const int SECONDS = 60;

		[SerializeField]
		private TextMeshProUGUI m_level;
		[SerializeField]
		private TextMeshProUGUI m_money;
		[SerializeField]
		private TextMeshProUGUI m_points;
		[SerializeField]
		private TextMeshProUGUI m_time;

		protected override void OnEnable()
		{
			SoundManager.Instance.Play(Sounds.LevelUp, inPause: true);

			m_level.text = User.Instance.Level.Ordinal.ToString();
			m_money.text = $"{User.Instance.Level.MoneyBalance} / {User.Instance.Level.MoneyLimit} (+{User.Instance.Level.EarnedMoney})";

			User.Instance.Level.CalculatePoints();
			m_points.text = User.Instance.Level.VisualPoints;

			var elapsedTime = FormatTime(User.Instance.Level.Time * Game.Manager.Time);
			var totalTime = FormatTime(User.Instance.Level.Time);
			m_time.text = $"{elapsedTime.Minutes}:{elapsedTime.Seconds:D2} / {totalTime.Minutes}:{totalTime.Seconds:D2}";
		}

		private (int Minutes, int Seconds) FormatTime(float time)
		{
			return ((int) (time / SECONDS), (int) (time % SECONDS));
		}
	}
}
