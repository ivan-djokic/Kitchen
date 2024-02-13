// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Management.Administration;
using Kitchen.Management.Commands;
using Kitchen.Management.Controls;
using Kitchen.Management.Managers;
using Kitchen.Management.MenuItems;
using Kitchen.Utils;
using UnityEngine;

namespace Kitchen.Management.Dispatchers
{
	public class Game : Dispatcher
	{
		[SerializeField]
		private DeliveryManager m_deliveryManager;
		[SerializeField]
		private VerticalLayoutPanel m_gameOver;
		[SerializeField]
		private VerticalLayoutPanel m_gamePaused;
		[SerializeField]
		private VerticalLayoutPanel m_levelUp;
		[SerializeField]
		private GameManager m_manager;
		[SerializeField]
		private VerticalLayoutPanel m_marketPlace;
		[SerializeField]
		private AudioSource m_music;
		[SerializeField]
		private VerticalLayoutPanel m_options;

		public static DeliveryManager DeliveryManager
		{
			get => Instance.m_deliveryManager;
		}

		public new static Game Instance { get; private set; }

		public static GameManager Manager
		{
			get => Instance.m_manager;
		}

		public void GameOver()
		{
			m_music.Stop();
			ActivateControl(m_gameOver);
		}

		public void LevelUp()
		{
			m_music.Stop();
			ActivateControl(m_levelUp);
		}

		public override void ProcessButton(Button button)
		{
			switch (button.Action)
			{
				case ButtonAction.Buy:
					button.transform.GetParent<MarketItem>().BuyKitchenObject();
					break;

				case ButtonAction.LevelUp:
					User.Instance.Level.Ordinal++;
					Restart();
					break;

				case ButtonAction.MarketPlace:
					ActivateControl(m_marketPlace);
					break;

				case ButtonAction.MainMenu:
					OpenMainMenu();
					break;

				case ButtonAction.Options:
					ActivateControl(m_options);
					break;

				case ButtonAction.Restart:
					Restart();
					break;

				case ButtonAction.VolumeDecrease:
					button.transform.GetParent<VolumeItem>().Volume--;
					break;

				case ButtonAction.VolumeIncrease:
					button.transform.GetParent<VolumeItem>().Volume++;
					break;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			Instance = this;
		}

		protected override void ActivateControl(VerticalLayoutPanel control)
		{
			Paused = true;
			base.ActivateControl(control);
		}

		protected override void ControlUpdated(Command command)
		{
			if (command != Command.Exit)
			{
				return;
			}

			if (!Paused)
			{
				ActivateControl(m_gamePaused);
				return;
			}

			if (!m_controls.Contains(m_gamePaused))
			{
				return;
			}

			DeactivateControl();
		}

		protected override void DeactivateControl()
		{
			Paused = false;
			base.DeactivateControl();
		}

		protected override void OnDestroy()
		{
			Instance = null;
		}

		private void OpenMainMenu()
		{
			if (m_controls.Contains(m_levelUp))
			{
				User.Instance.Level.Ordinal++;
			}

			Paused = false;
			Scenes.MainMenu.Load();
		}

		private void Restart()
		{
			User.Instance.Reload();

			Paused = false;
			Scenes.Game.Load();
		}
	}
}
