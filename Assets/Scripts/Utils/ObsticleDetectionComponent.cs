// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.Collections.Generic;
using Kitchen.Common;
using UnityEngine;

namespace Kitchen.Utils
{
	public class ObsticleDetectionComponent
	{
		private const int MAX_OBSTICALES_COUNT = 3;

		private Vector3 m_lastDirection;
		private readonly LayerMask m_layerMask;
		private readonly float m_speed;
		private readonly Transform m_transform;

		public ObsticleDetectionComponent(Transform transform, LayerMask layerMask, float speed)
		{
			m_layerMask = layerMask;
			m_speed = speed;
			m_transform = transform;
		}

		private float Speed
		{
			get => Time.deltaTime * m_speed;
		}

		public bool AnyObsticles(Vector3 direction, float radious)
		{
			return Physics.CapsuleCast(m_transform.position, m_transform.position, radious, direction, Speed);
		}

		public T GetDirectedObsticle<T>(Vector3 direction, float maxDistance) where T : UnityObject
		{
			if (direction != Vector3.zero)
			{
				m_lastDirection = direction;
			}

			if (!Physics.Raycast(m_transform.position, m_lastDirection, out var obstacle, maxDistance, m_layerMask))
			{
				return default;
			}

			return obstacle.transform.GetComponent<T>();
		}

		public IList<T> GetNearByObsticles<T>(float maxDistance) where T : UnityObject
		{
			var obstacles = new Collider[MAX_OBSTICALES_COUNT];
			Physics.OverlapSphereNonAlloc(m_transform.position, maxDistance, obstacles, m_layerMask);

			var obstacleDistances = new Dictionary<float, T>();

			foreach (var obstacle in obstacles)
			{
				if (obstacle == null)
				{
					continue;
				}

				obstacleDistances[Vector3.Distance(m_transform.position, obstacle.transform.position)] = obstacle.transform.GetComponent<T>();
			}

			return obstacleDistances.ToSortedList();
		}
	}
}
