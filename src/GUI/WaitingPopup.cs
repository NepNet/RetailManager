using Gtk;

namespace RetailManager.GUI
{
	public class WaitingPopup : Window
	{
		public WaitingPopup(string message) : base(WindowType.Popup)
		{
			WindowPosition = WindowPosition.Center;
			WidthRequest = 200;
			HeightRequest = 70;
			var box = new Box(Orientation.Horizontal, 6);
			box.Halign = Align.Center;
			box.Valign = Align.Center;
			box.Add(new Spinner() {Active = true});
			box.Add(new Label(message));
			Add(box);

			ShowAll();
		}
	}
}