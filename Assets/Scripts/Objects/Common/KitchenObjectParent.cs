// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System;
using Kitchen.Audio;
using Kitchen.Common;
using Kitchen.Objects.KitchenObjects;
using UnityEngine;

namespace Kitchen.Objects.Common
{
	public abstract class KitchenObjectParent : UnityObject
	{
		private KitchenObject m_kitchenObject;
		[SerializeField]
		private Transform m_kitchenObjectLocation;

		public bool HasKitchenObject
		{
			get => KitchenObject != null;
		}

		public KitchenObject KitchenObject
		{
			get => m_kitchenObject;

			set
			{
				m_kitchenObject = value;
				KitchenObjectChanged?.Invoke();

				if (value == null)
				{
					return;
				}

				m_kitchenObject.Parent = KitchenObjectLocation;
			}
		}

		public Action KitchenObjectChanged { get; set; }

		public Transform KitchenObjectLocation
		{
			get => m_kitchenObjectLocation;
		}

		protected abstract Sounds Sound { get; }

		public void DestroyKitchenObject()
		{
			KitchenObject?.Destroy();
			KitchenObject = null;
		}

		public void SwapKitchenObjectWith(KitchenObjectParent other)
		{
			(KitchenObject, other.KitchenObject) = (other.KitchenObject, KitchenObject);
		}

		protected override void Start()
		{
			KitchenObjectChanged += PlaySound;
		}

		protected void PlaySound()
		{
			if (!HasKitchenObject)
			{
				return;
			}

			SoundManager.Instance.Play(Sound, transform);
		}
	}
}
