using Cairo;

namespace RetailManager.PDF
{
	public class PdfCreator
	{
		private PdfSurface _surface;
		private Context _context;

		public PdfCreator()
		{
			_surface = new PdfSurface(null, 
				Utilities.MillimetersToPoints(210),
				Utilities.MillimetersToPoints(297));
			var cr = new Context(_surface);
			
			var line = new PdfElement.Line();
			line.End = new PointD(500,500);
			line.Thickness = 3;
			
			cr.MoveTo(line.Start);
			cr.LineTo(line.End);
			cr.LineWidth = 5;
		//	cr.SetSourceColor(line.Outline);
			cr.Stroke();
			
			cr.MoveTo(50, 50);
			cr.SetSourceRGB(1,0,0);
			cr.ShowText("hello");
			cr.Rectangle(0,0, 40,50);
			cr.Stroke();
			
			cr.ShowPage();
			
			cr.Dispose();
			_surface.Dispose();
		}
		/*
		var s = new PdfSurface("/home/nepnet/Desktop/test.pdf", 
			Utilities.MillimetersToPoints(210), 
			Utilities.MillimetersToPoints(297));
		Context cr = new Context(s);
			
		cr.MoveTo(50, 50);
		cr.SetSourceRGB(1,0,0);
		cr.ShowText("hello");
		cr.Rectangle(0,0, 40,50);
		cr.Stroke();
		cr.ShowPage();
			
		cr.Dispose();
		s.Dispose();*/
	}
}