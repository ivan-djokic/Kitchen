// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Utils;
using UnityEngine;

namespace Kitchen.Management.MenuItems
{
	public class MusicItem : VolumeItem
	{
		private const float VOLUME_FACTOR = 0.2f;

		[SerializeField]
		private AudioSource m_music;

		protected override float VolumeInternal
		{
			get => Options.Instance.MusicVolume / VOLUME_FACTOR;
			set => Options.Instance.MusicVolume = m_music.volume = value * VOLUME_FACTOR;
		}
	}
}
