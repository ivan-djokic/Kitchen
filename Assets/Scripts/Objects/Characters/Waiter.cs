// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using Kitchen.Objects.Counters;
using Kitchen.Objects.KitchenObjects;
using Kitchen.Utils;
using UnityEngine;

namespace Kitchen.Objects.Characters
{
	public class Waiter : BaseCharacter<Waiter>
	{
		private const float GO_OFFSET_X = -5;
		private const float GO_OFFSET_Y = -8;
		private const float GO_OFFSET_Z = 12;
		private const float MOVEMENT_SPEED = 7;
		private const int PRIORITY_CARRY_DIRTY_PLATE = 0;
		private const int PRIORITY_GO = 3;
		private const int PRIORITY_PICK_DELIVERY = 2;
		private const int PRIORITY_TAKE_ORDER = 1;

		private Vector3 m_camePosition;
		private Vector3 m_destination;
		private Vector3 m_gonePosition;
		private readonly PriorityAction m_onCame = new();
		private Action m_onGone;
		private WaiterState m_state = WaiterState.Came;

		public Action DirtyPlateCarried { get; set; }

		public Action ReadyToTakeOrder { get; set; }

		protected override float MovementSpeed
		{
			get => MOVEMENT_SPEED;
		}

		private bool LookAtPlayer
		{
			set => GetComponent<LookAtObjectBehaviour>().enabled = value;
		}

		public void CarryDirtyPlate(Plate plateTemplate)
		{
			Go(() =>
			{
				KitchenObject = plateTemplate.Create(KitchenObjectLocation);
				Come(PRIORITY_CARRY_DIRTY_PLATE, DirtyPlateCarried);
			});
		}

		public void PickDelivery(DeliveryCounter deliveryCounter)
		{
			Come(PRIORITY_PICK_DELIVERY, () =>
			{
				SwapKitchenObjectWith(deliveryCounter);
				Go(DestroyKitchenObject);
			});
		}

		public void TakeOrder()
		{
			if (m_state == WaiterState.Coming)
			{
				Come(PRIORITY_TAKE_ORDER, ReadyToTakeOrder);
				return;
			}

			Go(() => Come(PRIORITY_TAKE_ORDER, ReadyToTakeOrder));
		}

		protected override void Start()
		{
			m_gonePosition = transform.position + new Vector3(GO_OFFSET_X, GO_OFFSET_Y, GO_OFFSET_Z);
			m_destination = m_camePosition = transform.position;
		}

		protected override void Updated()
		{
			if (m_movementHelper.MoveTowards(m_destination))
			{
				m_movementHelper.LookForwards(m_destination - transform.position);
				return;
			}

			switch (m_state)
			{
				case WaiterState.Coming:
					m_state = WaiterState.Came;
					m_onCame.Invoke();
					// m_onCame will be auto cleared
					break;

				case WaiterState.Going:
					m_state = WaiterState.Gone;
					m_onGone?.Invoke();
					m_onGone = null;
					break;
			}
		}

		private void Come(int priority, Action onCame)
		{
			switch (m_state)
			{
				case WaiterState.Came:
					// Already came
					onCame.Invoke();
					return;

				case WaiterState.Going:
					// Come when waiter gone
					Go(() => Come(priority, onCame));
					return;

				case WaiterState.Gone:
					// Initialization
					m_state = WaiterState.Coming;
					m_destination = m_camePosition;
					LookAtPlayer = true;
					break;
			}

			m_onCame.Subscribe(priority, onCame);
		}

		private void Go(Action onGone = null)
		{
			switch (m_state)
			{
				case WaiterState.Gone:
					// Already gone
					onGone.Invoke();
					return;

				case WaiterState.Coming:
					// Go when waiter came
					Come(PRIORITY_GO, () => Go(onGone));
					return;

				case WaiterState.Came:
					// Initialization
					m_state = WaiterState.Going;
					m_destination = m_gonePosition;
					LookAtPlayer = false;
					break;
			}

			m_onGone += onGone;
		}
	}
}
