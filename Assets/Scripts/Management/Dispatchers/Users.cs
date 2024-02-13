// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.Linq;
using Kitchen.Audio;
using Kitchen.Management.Administration;
using Kitchen.Management.Commands;
using Kitchen.Utils;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Kitchen.Management.Dispatchers
{
	public class Users : Dispatcher
	{
		private const int MAX_ITEMS = 6;
		private const float SPEED = 2;
		private const float SPIN_COMPLEATED = 1;
		private const float SPIN_ROTATION = 60;

		[SerializeField]
		private GameObject m_arrows;
		private readonly RoundRobin m_currentItem = new(MAX_ITEMS);
		private Quaternion m_endRotation;
		[SerializeField]
		private TMP_InputField m_inputField;
		[SerializeField]
		private Transform[] m_items;
		private float m_spinProgress = SPIN_COMPLEATED;
		private Quaternion m_startRotation;

		public static new Users Instance { get; private set; }

		private bool IsValid
		{
			get => m_items[m_currentItem.Current].parent.parent.gameObject.activeSelf;
		}

		protected override void Awake()
		{
			base.Awake();
			Instance = this;
		}

		protected override void ControlUpdated(Command control)
		{
			int current;

			switch (control)
			{
				case Command.Select:
					User.Instatiate(GetUser(out var alreadyExist));

					if (!alreadyExist)
					{
						User.Instance.Save();
					}

					Scenes.MainMenu.Load();
					return;

				case Command.VerticalNext:
					do
					{
						current = m_currentItem.Next;
					} while (!IsValid);
					break;

				case Command.VerticalPrevious:
					do
					{
						current = m_currentItem.Previous;
					} while (!IsValid);
					break;

				default:
					return;
			}

			// The value needs to be negative because direction is inverted
			Spin(-current * SPIN_ROTATION);
		}

		protected override void OnDestroy()
		{
			Instance = null;
		}

		protected override void Start()
		{
			m_startRotation = transform.rotation;
			var userNames = FileHelper.GetFiles(typeof(Options).Name).Select(name => User.ConstructName(name)).ToArray();

			if (!userNames.Any())
			{
				m_arrows.SetActive(false);
				return;
			}

			for (var i = 0; i < userNames.Length; i++)
			{
				if (i >= m_items.Length)
				{
					return;
				}

				m_items[i + 1].parent.parent.gameObject.SetActive(true);
				m_items[i + 1].GetComponent<TextMeshProUGUI>().text = userNames[i];
			}
		}

		protected override void Update()
		{
			m_inputField.ActivateInputField();

			if (m_spinProgress >= SPIN_COMPLEATED)
			{
				return;
			}

			m_spinProgress += Time.deltaTime * SPEED;
			transform.rotation = Quaternion.Lerp(transform.rotation, m_endRotation, m_spinProgress);
		}

		private string GetUser(out bool alreadyExist)
		{
			alreadyExist = m_items[m_currentItem.Current].TryGetComponent<TextMeshProUGUI>(out var user);
			return alreadyExist ? user.text : m_inputField.text;
		}

		private void Spin(float rotation)
		{
			SoundManager.Instance.Play(Sounds.Spin);

			m_endRotation = m_startRotation * Quaternion.AngleAxis(rotation, Vector3.up);
			m_spinProgress = 0;
		}
	}
}
