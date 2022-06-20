using Cairo;

namespace RetailManager.PDF
{
	public abstract partial class PdfElement
	{
		public class Line : PdfElement
		{
			public PointD Start { get; set; }
			public PointD End { get; set; }
			public double Thickness { get; set; } = 1;
			public Color Outline { get; set; }
		}
	}
}