using System;
using System.Runtime.Serialization;

namespace JsonSerialization
{
	[DataContract]
	public class ParentDTO
	{
		public long Id { get; set; }
		public DateTimeOffset Created { get; set; }
	}
}

