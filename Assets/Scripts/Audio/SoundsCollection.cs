// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Utils;
using UnityEngine;

namespace Kitchen.Audio
{
	[CreateAssetMenu]
	public class SoundsCollection : ScriptableObject
	{
		[SerializeField]
		private AudioClip[] m_countdown;
		[SerializeField]
		private AudioClip[] m_countdownFinish;
		[SerializeField]
		private AudioClip[] m_countdownStart;
		[SerializeField]
		private AudioClip[] m_cut;
		[SerializeField]
		private AudioClip[] m_deliveryFail;
		[SerializeField]
		private AudioClip[] m_deliverySuccess;
		[SerializeField]
		private AudioClip[] m_footstep;
		[SerializeField]
		private AudioClip[] m_gameOver;
		[SerializeField]
		private AudioClip[] m_levelUp;
		[SerializeField]
		private AudioClip[] m_money;
		[SerializeField]
		private AudioClip[] m_noMoney;
		[SerializeField]
		private AudioClip[] m_objectDrop;
		[SerializeField]
		private AudioClip[] m_objectPickup;
		[SerializeField]
		private AudioClip[] m_platePut;
		[SerializeField]
		private AudioClip[] m_quackPlayer;
		[SerializeField]
		private AudioClip[] m_quackWaiter;
		[SerializeField]
		private AudioClip[] m_records;
		[SerializeField]
		private AudioClip[] m_spin;
		[SerializeField]
		private AudioClip[] m_stoveSizzle;
		[SerializeField]
		private AudioClip[] m_trash;
		[SerializeField]
		private AudioClip[] m_warning;
		[SerializeField]
		private AudioClip[] m_washingDishes;


		public AudioClip GetAudio(Sounds sound)
		{
			var audio = sound switch
			{
				Sounds.Countdown => m_countdown,
				Sounds.CountdownFinish => m_countdownFinish,
				Sounds.CountdownStart => m_countdownStart,
				Sounds.Cut => m_cut,
				Sounds.DeliveryFail => m_deliveryFail,
				Sounds.DeliverySuccess => m_deliverySuccess,
				Sounds.Footstep => m_footstep,
				Sounds.GameOver => m_gameOver,
				Sounds.LevelUp => m_levelUp,
				Sounds.Money => m_money,
				Sounds.NoMoney => m_noMoney,
				Sounds.ObjectDrop => m_objectDrop,
				Sounds.ObjectPickup => m_objectPickup,
				Sounds.PlatePut => m_platePut,
				Sounds.QuackPlayer => m_quackPlayer,
				Sounds.QuackWaiter => m_quackWaiter,
				Sounds.Records => m_records,
				Sounds.Spin => m_spin,
				Sounds.StoveSizzle => m_stoveSizzle,
				Sounds.Trash => m_trash,
				Sounds.Warning => m_warning,
				_ => m_washingDishes
			};

			return Randomize.NextItem(audio);
		}
	}
}
