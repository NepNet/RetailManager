using System;
using System.Collections.Generic;
using Gtk;

namespace RetailManager
{
	public static class Utilities
	{
		private const float POINT_TO_MM = 0.352777778f;
		private const float MM_TO_POINT = 2.83465f;

		public static float MillimetersToPoints(float mm) => mm * MM_TO_POINT;
		public static float PointsToMillimeters(float mm) => mm * POINT_TO_MM;
		
		public static RadioButton[] CreateRadioFromEnum<T>(Action<T> changedDelegate) where T: struct, Enum
		{
			var buttons = new List<RadioButton>();
			var values = Enum.GetValues<T>();
			
			RadioButton first = null;
			foreach (var value in values)
			{
				var name = value.ToString().Replace('_', ' ');
				var radio = new RadioButton(first, name);
				first ??= radio;
				
				buttons.Add(radio);
				radio.Show();
				radio.Clicked += (sender, args) =>
				{
					if (radio.Active)
					{
						changedDelegate(value);
					}
				};
			}

			return buttons.ToArray();
		}
	}
}