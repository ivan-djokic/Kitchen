// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Kitchen.Objects.Common
{
	public class IconTemplate : TemplateFactory<Sprite>
	{
		[SerializeField]
		private Image m_icon;

		public IconTemplate Create(Sprite source, Transform parent)
		{
			return Generate(source, parent) as IconTemplate;
		}

		public void SetParent(Transform parent)
		{
			transform.SetParent(parent);
		}

		protected override void Initialize(Sprite source)
		{
			m_icon.sprite = source;
		}
	}
}
