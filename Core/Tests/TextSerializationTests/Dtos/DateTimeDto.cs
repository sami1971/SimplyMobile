using System;
using System.Runtime.Serialization;

namespace TextSerializationTests
{
    [DataContract]
    public class DateTimeDto : IEquatable<DateTimeDto>
    {
        [DataMember(Order = 1)]
        public TimeSpan TimeSpan { get; set; }
        [DataMember(Order = 2)]
        public DateTime DateTime { get; set; }
        [DataMember(Order = 3)]
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

        #region IEquatable implementation
        public override bool Equals (object obj)
        {
            var dto = obj as DateTimeDto;
            return this.Equals (dto);
        }

        public override int GetHashCode ()
        {
            return this.DateTime.GetHashCode () ^ this.DateTimeOffset.GetHashCode () ^ this.TimeSpan.GetHashCode ();
        }

        public bool Equals (DateTimeDto other)
        {
            if (other == null) return false;
            return this.DateTime == other.DateTime && this.TimeSpan == other.TimeSpan
                && this.DateTimeOffset == other.DateTimeOffset;
        }

        #endregion
    }
}

