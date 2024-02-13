// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Audio;
using Kitchen.Common;
using Kitchen.Management.Administration;
using Kitchen.Objects.Characters;
using Kitchen.Objects.KitchenObjects;
using Kitchen.Utils;
using UnityEngine;

namespace Kitchen.Objects.Counters
{
	public class WashingDishesCounter : ProgressCounter<Plate>, ISpawnItem
	{
		[SerializeField]
		private GoldenPlate m_goldenPlateTemplate;
		private PlatesStack m_plates;
		[SerializeField]
		private Plate m_plateTemplate;
		[SerializeField]
		private GameObject m_washing;

		public override bool CanBeSelected
		{
			get => HasKitchenObject && !Player.Instance.HasKitchenObject;
		}

		public static WashingDishesCounter Instance { get; private set; }

		public float SpawnPeriod
		{
			get => User.Instance.Level.PlateSpawnTime;
		}

		protected override Sounds Sound
		{
			get => Sounds.PlatePut;
		}

		public void Spawn()
		{
			if (!m_plates.CanAdd)
			{
				return;
			}

			if (Randomize.Next < User.Instance.Level.GoldenPlateFrequency)
			{
				Waiter.Instance.CarryDirtyPlate(m_goldenPlateTemplate);
				return;
			}

			Waiter.Instance.CarryDirtyPlate(m_plateTemplate);
		}

		protected override void Awake()
		{
			Instance = this;
		}

		protected override void Interact()
		{
			SoundManager.Instance.Play(Sounds.WashingDishes, transform);
			m_washing.SetActive(true);
			m_progress.Begin(KitchenObject.Period);
		}

		protected override void InteractEnded()
		{
			SoundManager.Instance.Stop(Sounds.WashingDishes);
			m_progress.Pause();
			m_washing.SetActive(false);
		}

		protected override void ProgressFinished()
		{
			base.ProgressFinished();

			Player.Instance.KitchenObject = m_plates.Pop();
			Player.Instance.UnselectCounter();
			InteractEnded();
		}

		protected override void Start()
		{
			base.Start();

			m_plates = new PlatesStack(this);
			Waiter.Instance.DirtyPlateCarried += PutDirtyPlate;
		}

		private void PutDirtyPlate()
		{
			m_plates.Push(Waiter.Instance.KitchenObject as Plate, !m_progress.IsVisible);
			Waiter.Instance.KitchenObject = null;
		}
	}
}
