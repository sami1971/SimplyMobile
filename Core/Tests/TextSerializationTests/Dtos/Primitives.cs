using System;

namespace TextSerializationTests
{
	public class Primitives
	{
		public int Int { get; set; }
		public long Long { get; set; }
		public float Float { get; set; }
		public double Double { get; set; }
		public bool Boolean { get; set; }
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

			p.String = p.ToString ();

			return p;
		}
	}
}

