// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.Runtime.Serialization;
using Kitchen.Objects.KitchenObjects;

namespace Kitchen.Utils.Levels
{
	[DataContract]
	public class Groceries
	{
		private const int DEFAULT_BREAD_COUNT = 9;
		private const int DEFAULT_CABBAGE_COUNT = 5;
		private const int DEFAULT_CHEESE_COUNT = 4;
		private const int DEFAULT_MEAT_PATTY_COUNT = 3;
		private const int DEFAULT_POTATO_COUNT = 2;
		private const int DEFAULT_TOMATO_COUNT = 5;

		[DataMember]
		private int m_breadCount = DEFAULT_BREAD_COUNT;
		[DataMember]
		private int m_cabbageCount = DEFAULT_CABBAGE_COUNT;
		[DataMember]
		private int m_cheeseCount = DEFAULT_CHEESE_COUNT;
		[DataMember]
		private int m_meatPattyCount = DEFAULT_MEAT_PATTY_COUNT;
		[DataMember]
		private int m_potatoCount = DEFAULT_POTATO_COUNT;
		[DataMember]
		private int m_tomatoCount = DEFAULT_TOMATO_COUNT;

		public ref int GetKitchenObjectCount(KitchenObjectType type)
		{
			switch (type)
			{
				case KitchenObjectType.Bread:
					return ref m_breadCount;

				case KitchenObjectType.Cabbage:
					return ref m_cabbageCount;

				case KitchenObjectType.Cheese:
					return ref m_cheeseCount;

				case KitchenObjectType.MeatPatty:
					return ref m_meatPattyCount;

				case KitchenObjectType.Potato:
					return ref m_potatoCount;

				default:
					return ref m_tomatoCount;
			}
		}

		public void SetKitchenObjectCount(KitchenObjectType type, int count)
		{
			GetKitchenObjectCount(type) = count;
		}
	}
}
