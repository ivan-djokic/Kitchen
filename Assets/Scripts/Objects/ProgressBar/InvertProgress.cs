// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

namespace Kitchen.Objects.ProgressBar
{
	public class InvertProgress : Progress
	{
		private const float FINISHED = 0;

		protected override bool ShowFlush
		{
			get => Current < FlushLimit;
		}

		protected override bool IsFinished(float step)
		{
			return Current - step < FINISHED;
		}

		protected override void UpdateProgress(float step)
		{
			Current -= step;
		}
	}
}
