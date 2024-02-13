// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

namespace Kitchen.Objects.ProgressBar
{
	public class IncreasingProgress : Progress
	{
		private const float PERIOD = 5;

		private float m_progressComplete;

		public void Increase(float progress)
		{
			m_progressComplete = progress;
			Begin(PERIOD);
		}

		protected override void Updated()
		{
			m_isPaused = m_progressComplete < Current;
			base.Updated();
		}
	}
}
