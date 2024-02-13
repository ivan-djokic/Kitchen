// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.ComponentModel;

namespace Kitchen.Objects.KitchenObjects
{
	public enum KitchenObjectType
	{
		[Description("Лепиња")]
		Bread,
		[Description("Купус")]
		Cabbage,
		[Description("Сир")]
		Cheese,
		[Description("Пљескавица")]
		MeatPatty,
		[Description("Тањир")]
		Plate,
		[Description("Помфрит")]
		Potato,
		[Description("Парадајз")]
		Tomato
	}
}
