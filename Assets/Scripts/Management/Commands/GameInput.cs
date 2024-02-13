// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using System.Linq;
using Kitchen.Common;
using Kitchen.Management.Dispatchers;
using UnityEngine;

namespace Kitchen.Management.Commands
{
	public class GameInput : UnityObject
	{
		public Action<Command> ControlUpdated { get; set; }

		public static GameInput Instance { get; private set; }

		public Action<bool> InteractUpdated { get; set; }

		public Action<Vector3> MovementUpdated { get; set; }

		protected override void Awake()
		{
			// Unity will always instantiate this class right after Game and before others
			Instance = this;
			Cursor.visible = false;
		}

		protected override void Update()
		{
			NotifyControl();

			if (Dispatcher.Instance.Paused || Dispatcher.Instance.Starting)
			{
				return;
			}

			NotifyGame();
		}

		private Vector3 GetMovementVector()
		{
			var inputVector = Vector3.zero;

			if (Input.GetKey(KeyCode.UpArrow))
			{
				inputVector.z = 1;
			}
			else if (Input.GetKey(KeyCode.DownArrow))
			{
				inputVector.z = -1;
			}

			if (Input.GetKey(KeyCode.RightArrow))
			{
				inputVector.x = 1;
			}
			else if (Input.GetKey(KeyCode.LeftArrow))
			{
				inputVector.x = -1;
			}

			return inputVector;
		}

		private void NotifyControl()
		{
			foreach (var key in Enum.GetValues(typeof(Key)).OfType<Key>())
			{
				if (Input.GetKeyDown(key.GetCode()))
				{
					ControlUpdated?.Invoke(key.GetControl());
					return;
				}
			}
		}

		private void NotifyGame()
		{
			MovementUpdated?.Invoke(GetMovementVector());

			var interactStarted = Input.GetKeyDown(KeyCode.Space);
			var interactEnded = Input.GetKeyUp(KeyCode.Space);

			if (!interactStarted && !interactEnded)
			{
				return;
			}

			InteractUpdated?.Invoke(interactEnded);
		}
	}
}
