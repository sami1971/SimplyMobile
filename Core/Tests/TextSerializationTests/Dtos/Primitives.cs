using System;
using System.Runtime.Serialization;

namespace TextSerializationTests
{
    [DataContract]
    public class Primitives : IEquatable<Primitives>
    {
        [DataMember(Order = 1)]
        public int Int { get; set; }
        [DataMember(Order = 2)]
        public long Long { get; set; }
        [DataMember(Order = 3)]
        public float Float { get; set; }
        [DataMember(Order = 4)]
        public double Double { get; set; }
        [DataMember(Order = 5)]
        public bool Boolean { get; set; }
        [DataMember(Order = 6)]
        public string String { get; set; }

        public Primitives ()
        {
        }

        public override string ToString ()
        {
            return string.Format ("[Primitives: Int={0}, Long={1}, Float={2}, Double={3}, Boolean={4}]", Int, Long, Float, Double, Boolean);
        }

        public static Primitives Create(int i)
        {
            var p = new Primitives () 
            {
                Int = i,
                Long = i,
                Float = i,
                Double = i,
                Boolean = i % 2 == 0,
            };

            p.String = p.ToString();

            return p;
        }

        #region IEquatable implementation
        public override bool Equals (object obj)
        {
            return this.Equals (obj as Primitives);
        }

        public override int GetHashCode ()
        {
            return this.Int ^ this.Long.GetHashCode() 
                ^ this.Float.GetHashCode() 
                    ^ this.Double.GetHashCode() 
                    ^ this.Boolean.GetHashCode() 
                    ^ this.String.GetHashCode();
        }

        public bool Equals (Primitives other)
        {
            if (other == null) return false;
            return this.Int == other.Int && this.Long == other.Long && this.Float == other.Float && this.Double == other.Double
                && this.Boolean == other.Boolean && this.String == other.String;
        }

        #endregion
    }
}

