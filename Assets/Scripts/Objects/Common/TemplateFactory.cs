// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Common;
using UnityEngine;

namespace Kitchen.Objects.Common
{
	public abstract class TemplateFactory<T> : UnityObject
	{
		public void Destroy()
		{
			Destroy(gameObject);
		}

		protected TemplateFactory<T> Generate(T source, Transform parent)
		{
			var instance = Instantiate(gameObject, parent).GetComponent<TemplateFactory<T>>();
			instance.Initialize(source);

			return instance;
		}

		protected abstract void Initialize(T source);
	}
}
