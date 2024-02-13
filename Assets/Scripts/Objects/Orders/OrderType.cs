// -------------------------------
// © 2023 Unity Kitchen. BATARUKI.
// -------------------------------

using System.ComponentModel;

namespace Kitchen.Objects.Orders
{
	public enum OrderType
	{
		[Description("Лепиња")]
		Bread,
		[Description("Пљескавица")]
		Burger,
		[Description("Седвич са сиром")]
		CheeseSandwich,
		[Description("Помфрит")]
		FrenchFries,
		[Description("Салата")]
		Salad
	}
}
