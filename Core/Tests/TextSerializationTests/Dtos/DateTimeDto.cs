using System;

namespace TextSerializationTests
{
	public class DateTimeDto
	{
		public TimeSpan TimeSpan { get; set; }
		public DateTime DateTime { get; set; }
		public DateTimeOffset DateTimeOffset { get; set; }

		public DateTimeDto ()
		{
		}

		public static DateTimeDto Create(long ticksToAdvance)
		{
			return new DateTimeDto () 
			{
				TimeSpan = TimeSpan.FromTicks(ticksToAdvance),
				DateTime = DateTime.Now.AddTicks(ticksToAdvance),
				DateTimeOffset = DateTime.Now.AddTicks(ticksToAdvance)
			};
		}
	}
}

