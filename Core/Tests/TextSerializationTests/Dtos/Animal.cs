using System;
using System.Runtime.Serialization;

namespace TextSerializationTests
{
	[DataContract]
	public abstract class Animal : IAnimal
	{
		[DataMemberAttribute(Order=1)]
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

