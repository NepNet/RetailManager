using System;
using System.Collections;
using System.Collections.Generic;

namespace RetailManager.Data
{
	public class Cart : IEnumerable<CartItem>
	{
		private List<CartItem> _cartItems;

		public Cart()
		{
			_cartItems = new List<CartItem>();
		}
		
		public CartItem this[int index] => _cartItems[index];
		public int Count => _cartItems.Count;

		public CartItem AddItem(Item item, decimal quantity = 1)
		{
			var cartItem = new CartItem(item)
			{
				Quantity = quantity
			};
			_cartItems.Add(cartItem);
			Added?.Invoke(cartItem);
			return cartItem;
		}

		public void RemoveItem(CartItem item)
		{ 
			_cartItems.Remove(item);
			Removed?.Invoke(item);
		}

		public void Clear()
		{
			_cartItems = new List<CartItem>();
			Cleared?.Invoke();
		}

		public event Action<CartItem> Added;
		public event Action<CartItem> Removed;
		public event Action Cleared;
		
		public IEnumerator<CartItem> GetEnumerator()
		{
			return new CartEnumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		
		private class CartEnumerator : IEnumerator<CartItem>
		{
			private Cart _cart;
			private int _index = -1;
			public CartEnumerator(Cart cart)
			{
				_cart = cart;
			}
			
			public bool MoveNext()
			{
				_index++;
				return _index < _cart.Count;
			}

			public void Reset()
			{
				_index = -1;
			}

			public CartItem Current => _cart[_index];

			object IEnumerator.Current => Current;

			public void Dispose()
			{
				
			}
		}
	}
}