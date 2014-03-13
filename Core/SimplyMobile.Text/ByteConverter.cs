using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyMobile.Text
{
    public static class ByteConverter
    {
        #region DateTime
        public static byte[] ToBytes(this DateTime dateTime)
        {
            return BitConverter.GetBytes(dateTime.ToBinary());
        }

        public static DateTime ToDateTime(this byte[] bytes, int startIndex = 0)
        {
            return DateTime.FromBinary(BitConverter.ToInt64(bytes, startIndex));
        }
        #endregion

        #region DateTimeOffset
        public static byte[] ToBytes(this DateTimeOffset offset)
        {
            var bytes = new byte[16];
            Buffer.BlockCopy(BitConverter.GetBytes(offset.Ticks), 0, bytes, 0, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(offset.Offset.Ticks), 0, bytes, 8, 8);
            return bytes;
        }

        public static DateTimeOffset ToDateTimeOffset(this byte[] bytes, int startIndex = 0)
        {
            var ticks = BitConverter.ToInt64(bytes, startIndex);
            var offset = BitConverter.ToInt64(bytes, startIndex + 8);
            return new DateTimeOffset(ticks, TimeSpan.FromTicks(offset));
        }
        #endregion

        #region TimeSpan
        public static byte[] ToBytes(this TimeSpan timespan)
        {
            return BitConverter.GetBytes(timespan.Ticks);
        }

        public static TimeSpan ToTimeSpan(this byte[] bytes, int startIndex = 0)
        {
            return TimeSpan.FromTicks(BitConverter.ToInt64(bytes, startIndex));
        }
        #endregion

        public static byte[] ToBytes(this int[] ints)
        {
            var bytes = new byte[Buffer.ByteLength(ints)];
            Buffer.BlockCopy(ints, 0, bytes, 0, bytes.Length);
            return bytes;
        }

        #region decimal
        public static byte[] ToBytes(this Decimal dec)
        {
            return Decimal.GetBits(dec).ToBytes();
        }

        public static Decimal ToDecimal(this byte[] bytes)
        {
            var ints = new int[4];

            for (var n = 0; n < 4; n++)
            {
                ints[n] = BitConverter.ToInt32(bytes, n*4);
            }

            return new Decimal(ints);
        }
        #endregion
    }
}
