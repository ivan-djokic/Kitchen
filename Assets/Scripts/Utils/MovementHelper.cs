// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using UnityEngine;

namespace Kitchen.Utils
{
	public class MovementHelper
	{
		private Vector3 m_lastDirection;
		private readonly float m_speed;
		private readonly Transform m_transform;

		public MovementHelper(Transform transform, float speed)
		{
			m_speed = speed;
			m_transform = transform;
		}

		private float Speed
		{
			get => Time.deltaTime * m_speed;
		}

		public void LookForwards(Vector3 direction)
		{
			if (direction != Vector3.zero)
			{
				m_lastDirection = direction;
			}

			if (m_lastDirection == Vector3.zero)
			{
				return;
			}

			m_transform.forward = Vector3.Slerp(m_transform.forward, m_lastDirection, Speed);
		}

		public void Move(Vector3 direction)
		{
			m_transform.position += direction * Speed;
		}

		public bool MoveTowards(Vector3 targetPosition)
		{
			if (m_transform.position == targetPosition)
			{
				return false;
			}

			m_transform.position = Vector3.MoveTowards(m_transform.position, targetPosition, Speed);
			return true;
		}
	}
}
