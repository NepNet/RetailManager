namespace RetailManager
{
	public static class Utilities
	{
		private const float POINT_TO_MM = 0.352777778f;
		private const float MM_TO_POINT = 2.83465f;

		public static float MillimetersToPoints(float mm) => mm * MM_TO_POINT;
		public static float PointsToMillimeters(float mm) => mm * POINT_TO_MM;
	}
}