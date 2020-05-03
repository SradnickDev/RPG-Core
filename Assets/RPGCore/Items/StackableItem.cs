using UnityEngine;

namespace RPGCore.Items
{
	public abstract class StackableItem : IItem
	{
		public int MaxQuantity = 10;
		//TODO uncomment private from setter
		public int Quantity { get; /*private*/ set; } = 1;
		public ItemTemplate ItemTemplate { get; set; }
		
		public abstract IItem Duplicate();

		public virtual StackableItem Take(int quantity)
		{
			if (quantity <= Quantity)
			{
				Quantity -= quantity;
			}

			return (StackableItem) Duplicate();
		}

		public virtual bool CanStack(int quantity) => Quantity + quantity <= MaxQuantity;

		public virtual void Stack(int quantity)
		{
			if (CanStack(quantity))
			{
				Quantity += quantity;
			}
		}
	}
}