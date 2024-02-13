// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

namespace Kitchen.Utils
{
	public class RoundRobin
	{
		private readonly int m_count;

		public RoundRobin(int count)
		{
			m_count = count;
		}

		public int Current { get; protected set; }

		public int Next
		{
			get => Current = ++Current % m_count;
		}

		public int Previous
		{
			get => Current = Current > 0 ? --Current : m_count - 1;
		}

		public static int Convert(int current, int count)
		{
			return (current + count) % count;
		}
	}
}
