// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using Kitchen.Utils;
using UnityEngine;

namespace Kitchen.Objects.ProgressBar
{
	public class CursorProgress : Progress
	{
		private const float HALT_PROGRESS = 0.5f;

		[SerializeField]
		private Transform m_cursor;

		private float CursorPosition
		{
			set => m_cursor.localPosition = new Vector2(value, m_cursor.transform.localPosition.y);
		}

		public void SetProgress(float progress)
		{
			Current = progress;

			// Normalize progress to shift Cursor from beginning instead of center of ProgressBar
			CursorPosition = m_progress.GetWidth() * (progress - HALT_PROGRESS);
		}
	}
}
