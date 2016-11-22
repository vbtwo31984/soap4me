using System;
namespace soap4me
{
	public class Util
	{
		public static DateTime UnixTimestampToDateTime(long timestamp)
		{
			System.DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
			dateTime = dateTime.AddSeconds(timestamp).ToLocalTime();
			return dateTime;
		}
	}
}
