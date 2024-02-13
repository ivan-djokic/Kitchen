// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using Kitchen.Utils;
using UnityEngine;

namespace Kitchen.Objects.KitchenObjects
{
	[CreateAssetMenu]
	public class BaseKitchenObject : ScriptableObject, IEquatable<BaseKitchenObject>
	{
		[SerializeField]
		private Sprite m_icon;
		[SerializeField]
		private float m_lifeTime;
		private Transform m_parent;
		[SerializeField]
		protected int m_price;
		[SerializeField]
		private KitchenObjectState m_state;
		[SerializeField]
		private KitchenObjectType m_type;
		[SerializeField]
		private float m_verticalOffsetOnPlate;
		[SerializeField]
		protected Transform m_visualObject;

		public Sprite Icon
		{
			get => m_icon;
		}

		public float LifeTime
		{
			get => m_lifeTime;
		}

		public Transform Parent
		{
			get => m_parent;

			set
			{
				if (m_parent == value)
				{
					return;
				}

				m_parent = value;
				ResetParent();
			}
		}

		public int Price
		{
			get => m_price;
		}

		public KitchenObjectState State
		{
			get => m_state;
			set => m_state = value;
		}

		public KitchenObjectType Type
		{
			get => m_type;
		}

		public virtual void CopyTo(BaseKitchenObject other)
		{
			other.m_icon = m_icon;
			other.m_lifeTime = m_lifeTime;
			other.m_price = m_price;
			other.m_state = m_state;
			other.m_type = m_type;
			other.m_verticalOffsetOnPlate = m_verticalOffsetOnPlate;
			other.m_visualObject = m_visualObject;
		}

		public void Destroy()
		{
			Destroy(m_visualObject.gameObject);
		}

		public bool Equals(BaseKitchenObject other)
		{
			return m_type == other?.m_type && m_state == other?.m_state;
		}

		public void SetVerticalOffset(float? y = null)
		{
			m_visualObject.localPosition = new Vector3(0, y ?? m_verticalOffsetOnPlate, 0);
		}

		public void SetVerticalRotation(float y)
		{
			m_visualObject.localRotation = Quaternion.Euler(0, y, 0);
		}

		public override string ToString()
		{
			return m_type.Name();
		}

		protected void ResetParent()
		{
			m_visualObject.SetParent(m_parent);
			m_visualObject.Normalize();
		}
	}
}
