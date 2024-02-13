// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Audio;
using Kitchen.Common;
using Kitchen.Management.Administration;
using TMPro;
using UnityEngine;

namespace Kitchen.Management.Managers
{
	public class GameOverManager : UnityObject
	{
		[SerializeField]
		private TextMeshProUGUI m_level;
		[SerializeField]
		private TextMeshProUGUI m_points;
		[SerializeField]
		private TextMeshProUGUI m_user;

		protected override void OnEnable()
		{
			SoundManager.Instance.Play(Sounds.GameOver, inPause: true);
			User.Instance.Level.CalculatePoints();

			m_level.text = User.Instance.Level.Ordinal.ToString();
			m_points.text = User.Instance.Level.VisualPoints;
			m_user.text = User.Instance.Name;
		}
	}
}
