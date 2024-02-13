// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using System.Runtime.Serialization;

namespace Kitchen.Utils
{
	[DataContract]
	public class Options
	{
		private const float DEFAULT_MUSIC_VOLUME = 0.15f;
		private const float DEFAULT_SOUND_VOLUME = 0.875f;

		[DataMember]
		private float m_musicVolume = DEFAULT_MUSIC_VOLUME;
		[DataMember]
		private float m_soundVolume = DEFAULT_SOUND_VOLUME;

		private static readonly Lazy<Options> s_instance = new(FileHelper.Load<Options>(typeof(Options).Name));

		public static Options Instance
		{
			get => s_instance.Value;
		}

		public float MusicVolume
		{
			get => m_musicVolume;

			set
			{
				m_musicVolume = value;
				Save();
			}
		}

		public float SoundVolume
		{
			get => m_soundVolume;

			set
			{
				m_soundVolume = value;
				Save();
			}
		}

		private void Save()
		{
			FileHelper.Save(typeof(Options).Name, this);
		}
	}
}
