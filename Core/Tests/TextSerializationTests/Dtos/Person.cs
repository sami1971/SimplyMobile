using System;
using System.Collections.Generic;
using System.Linq;

namespace TextSerializationTests
{
	public class Person : IEquatable<Person>
	{
		public long Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public List<IAnimal> Pets { get; set; }

		public Person()
		{
			this.Pets = new List<IAnimal> ();
		}		

		public override string ToString ()
		{
			return string.Format ("[Person: Id={0}, FirstName={1}, LastName={2}, Pets={3}]", Id, FirstName, LastName, Pets);
		}

		#region IEquatable implementation
		public override bool Equals (object obj)
		{
			var person = obj as Person;
			return (person != null && this.Equals(person));
		}

		public override int GetHashCode ()
		{
			return this.Id.GetHashCode () 
				^ this.FirstName.GetHashCode ()
					^ this.LastName.GetHashCode () 
					^ this.Pets.GetHashCode ()
					;
		}

		public bool Equals (Person other)
		{
			Console.WriteLine (this);
			Console.WriteLine (other);
			return this.Id == other.Id 
				&& this.FirstName == other.FirstName 
				&& this.LastName == other.LastName 
					&& this.Pets.SequenceEqual(other.Pets)
					;
		}

		#endregion
	}
}

