// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Audio;
using Kitchen.Utils;

namespace Kitchen.Management.MenuItems
{
	public class SoundItem : VolumeItem
	{
		protected override float VolumeInternal
		{
			get => Options.Instance.SoundVolume;

			set
			{
				Options.Instance.SoundVolume = value;
				SoundManager.Instance.Play(Sounds.NoMoney, inPause: true);
			}
		}
	}
}
