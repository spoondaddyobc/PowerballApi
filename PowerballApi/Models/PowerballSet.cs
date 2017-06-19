namespace PowerballApi.Api.Models
{
	public class PowerballSet
	{
		public string Date { get; set; }
		public int[] WinNumbers { get; set; }

		public PowerballSet()
		{
			WinNumbers = new int[6];
		}
	}
}