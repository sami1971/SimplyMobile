using System;

namespace TextSerializationTests
{
	public abstract class Animal : IAnimal
	{
		public string Name { get; set; }

		public abstract string MakeSound ();

		#region IEquatable implementation
		public override bool Equals (object obj)
		{
			var animal = obj as IAnimal;
			return animal != null && Equals (animal);
		}

		public override int GetHashCode ()
		{
			return this.MakeSound ().GetHashCode () ^ this.Name.GetHashCode ();
		}

		public bool Equals (IAnimal other)
		{
			return other.MakeSound ().Equals (this.MakeSound ()) && other.Name.Equals (this.Name);
		}
		#endregion
	}
}

