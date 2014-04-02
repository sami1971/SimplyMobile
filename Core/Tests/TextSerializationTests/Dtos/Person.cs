using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TextSerializationTests
{
    [DataContract]
    public class Person : IEquatable<Person>
    {
        [DataMember(Order = 1)]
        public long Id { get; set; }

        [DataMember(Order = 2)]
        public string FirstName { get; set; }

        [DataMember(Order = 3)]
        public string LastName { get; set; }

        public Person()
        {

        }       

        public override string ToString ()
        {
            return string.Format ("[Person: Id={0}, FirstName={1}, LastName={2}", Id, FirstName, LastName);
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
//                  ^ this.Pets.GetHashCode ()
                    ;
        }

        public bool Equals (Person other)
        {
            return this.Id == other.Id 
                && this.FirstName == other.FirstName 
                && this.LastName == other.LastName 
//                  && this.Pets.SequenceEqual(other.Pets)
                    ;
        }

        #endregion
    }

    public enum Status
    {
        Single,
        Married,
        Divorced
    }

    public class TinyPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Both string and integral enum value representations can be parsed:
        public Status Status { get; set; }
        public string Address { get; set; }
        // Just to be sure we support that one, too:
        public IEnumerable<int> Scores { get; set; }
        public object Data { get; set; }
        // Generic dictionaries are also supported; e.g.:
        // '{
        //    "Name": "F. Bastiat", ...
        //    "History": [
        //       { "key": "1801-06-30", "value": "Birth date" }, ...
        //    ]
        //  }'
        public IDictionary<DateTime, string> History { get; set; }
    }

    public class TinyPersonNoDic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public string Address { get; set; }
        public List<int> Scores { get; set; }
    }
}

