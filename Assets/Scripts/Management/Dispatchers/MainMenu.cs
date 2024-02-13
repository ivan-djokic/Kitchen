// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using Kitchen.Management.Administration;
using Kitchen.Management.Commands;
using Kitchen.Management.Controls;
using Kitchen.Management.MenuItems;
using Kitchen.Utils;
using Kitchen.Utils.Levels;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Kitchen.Management.Dispatchers
{
	public class MainMenu : Dispatcher
	{
		[SerializeField]
		private TextMeshProUGUI m_level;
		[SerializeField]
		private VerticalLayoutPanel m_mainMenu;
		[SerializeField]
		private VerticalLayoutPanel m_options;
		[SerializeField]
		private TextMeshProUGUI m_points;
		[SerializeField]
		private VerticalLayoutPanel m_records;
		[SerializeField]
		private Button m_reset;
		[SerializeField]
		private TextMeshProUGUI m_user;

		public Action<Action> GameStarting { get; set; }

		public static new MainMenu Instance { get; private set; }

		public override void ProcessButton(Button button)
		{
			switch (button.Action)
			{
				case ButtonAction.Exit:
					Application.Quit();
					//EditorApplication.ExecuteMenuItem("Edit/Play");
					break;

				case ButtonAction.Options:
					ActivateControl(m_options);
					break;

				case ButtonAction.Play:
					StartGame();
					break;

				case ButtonAction.Records:
					ActivateControl(m_records);
					break;

				case ButtonAction.Reset:
					User.Instance.Reset();
					m_points.text = string.Empty;
					StartGame();
					break;

				case ButtonAction.VolumeDecrease:
					button.transform.GetParent<VolumeItem>().Volume--;
					break;

				case ButtonAction.VolumeIncrease:
					button.transform.GetParent<VolumeItem>().Volume++;
					break;

				default:
					DeactivateControl();
					break;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			Instance = this;
		}

		protected override void ControlUpdated(Command command)
		{
			if (command != Command.Exit || !m_controls.TryPeek(out var control) || control == m_mainMenu)
			{
				return;
			}

			DeactivateControl();
		}

		protected override void OnDestroy()
		{
			Instance = null;
		}

		protected override void Start()
		{
			m_level.text = User.Instance.Level.Ordinal.ToString();
			m_points.text = User.Instance.Level.VisualPoints;
			m_reset.gameObject.SetActive(User.Instance.Level.Ordinal > Level.FIRST_LEVEL);
			m_user.text = User.Instance.Name;

			ActivateControl(m_mainMenu);
		}

		private void StartGame()
		{
			DeactivateControl();
			GameStarting.Invoke(() => Scenes.Game.Load());
		}
	}
}
