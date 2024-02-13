// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Common;
using UnityEngine;

namespace Kitchen.Utils
{
	public class LookAtObjectBehaviour : UnityObject
	{
		private const float SPEED = 2;

		[SerializeField]
		private Transform m_lookAtObject;

		private void LateUpdate()
		{
			if (m_lookAtObject == null)
			{
				transform.LookAt(Camera.main.transform);
				return;
			}

			var relativePosition = m_lookAtObject.position - transform.position;
			var toRotation = Quaternion.LookRotation(relativePosition);
			transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * SPEED);
		}
	}
}
