namespace PowerballApi.Api.Models
{
	using System;

	public class PowerballSet
	{
		public DateTime Date { get; set; }
		public int? PowerPlay { get; set; }
		public int[] WinNumbers { get; set; }

		public PowerballSet()
		{
			WinNumbers = new int[6];
		}
	}
}