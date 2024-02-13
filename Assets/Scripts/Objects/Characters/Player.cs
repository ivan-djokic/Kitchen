// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.Collections.Generic;
using System.Linq;
using Kitchen.Management.Administration;
using Kitchen.Management.Commands;
using Kitchen.Objects.Counters;
using Kitchen.Utils;
using UnityEngine;

namespace Kitchen.Objects.Characters
{
	public class Player : BaseCharacter<Player>
	{
		private const float INTERACT_DISTANCE = 1.2f;
		private const float PLAYER_RADIUS = 1.1f;

		[SerializeField]
		private LayerMask m_counterLayerMask;
		private ObsticleDetectionComponent m_obsticleDetectionHelper;
		private BaseCounter m_selectedCounter;

		protected override float MovementSpeed
		{
			get => User.Instance.Level.MovementSpeed;
		}

		private IList<BaseCounter> NearByCounters
		{
			get
			{
				var counters = m_obsticleDetectionHelper.GetNearByObsticles<BaseCounter>(INTERACT_DISTANCE);
				var priorityCounters = new Dictionary<int, BaseCounter>();

				for (var i = 0; i < counters.Count; i++)
				{
					priorityCounters[i + (byte)counters[i].Priority] = counters[i];
				}

				return priorityCounters.ToSortedList();
			}
		}

		public void UnselectCounter()
		{
			if (m_selectedCounter == null)
			{
				return;
			}

			m_selectedCounter.Selected = false;
			m_selectedCounter = null;
		}

		protected override void Start()
		{
			base.Start();

			m_obsticleDetectionHelper = new ObsticleDetectionComponent(transform, m_counterLayerMask, MovementSpeed);
			GameInput.Instance.MovementUpdated += InputUpdated;
		}

		private bool CanMove(float x, float z, out Vector3 direction)
		{
			// Vector needs to be normalized in order to have the same speed on diagonally and one direction movements
			direction = new Vector3(x, 0, z).normalized;
			return !m_obsticleDetectionHelper.AnyObsticles(direction, PLAYER_RADIUS);
		}

		private Vector3 GetDirection(float x, float z)
		{
			if (CanMove(x, z, out var direction))
			{
				return direction;
			}

			// Try horizontal movement
			if (CanMove(x, 0, out direction))
			{
				return direction;
			}

			// Try vertical movement
			if (CanMove(0, z, out direction))
			{
				return direction;
			}

			// Can not move in any direction
			return Vector3.zero;
		}

		private void InputUpdated(Vector3 movementVector)
		{
			m_movementHelper.Move(GetDirection(movementVector.x, movementVector.z));
			m_movementHelper.LookForwards(movementVector);

			SelectCounter(movementVector);
		}

		private void SelectCounter(Vector3 direction)
		{

			if (TrySelectCounter(m_obsticleDetectionHelper.GetDirectedObsticle<BaseCounter>(direction, INTERACT_DISTANCE)))
			{
				return;
			}

			if (NearByCounters.Any(item => TrySelectCounter(item)))
			{
				return;
			}

			UnselectCounter();
		}

		private bool TrySelectCounter(BaseCounter counter)
		{
			if (counter == null || !counter.CanBeSelected)
			{
				return false;
			}

			if (!counter.Selected)
			{
				UnselectCounter();
				counter.Selected = true;
				m_selectedCounter = counter;
			}

			return true;
		}
	}
}
