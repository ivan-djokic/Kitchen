// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Common;
using Kitchen.Objects.ProgressBar;
using UnityEngine;

namespace Kitchen.Management.MenuItems
{
	public abstract class VolumeItem : UnityObject
	{
		private const float MAX_VOLUME = 8;

		[SerializeField]
		private CursorProgress m_progress;

		public float Volume 
		{
			get => VolumeInternal * MAX_VOLUME;

			set
			{
				if (value < 0 || value > MAX_VOLUME)
				{
					return;
				}

				VolumeInternal = value / MAX_VOLUME;
				UpdateProgress();
			}
		}

		protected abstract float VolumeInternal { get; set; }

		protected override void Start()
		{
			UpdateProgress();
		}

		private void UpdateProgress()
		{
			m_progress.SetProgress(Volume / MAX_VOLUME);
		}
	}
}
