using System;
using NUnit.Framework;
using SimplyMobile.Text;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace TextSerializationTests
{
    [TestFixture ()]
    public class BooleanSpeedTests
    {
        /// <summary>
        /// Gets the serializer to use.
        /// </summary>
        private static IEnumerable<ITextSerializer> Serializers
        {
            get
            {
                return new ITextSerializer[] {
                    new SimplyMobile.Text.ServiceStack.JsonSerializer(),
                    new SimplyMobile.Text.JsonNet.JsonSerializer(),
                    new SimplyMobile.Text.RuntimeSerializer.JsonSerializer()
                };
            }
        }

        private static List<BooleanList> List
        {
            get
            {
                return (Enumerable.Range(0, 3)).Select (t =>
                    new BooleanList () {
                    FormName = t
                }).ToList ();
            }
        }

        private static List<BooleanList> List10k
        {
            get
            {
                return (Enumerable.Range(0, 10000)).Select (t =>
                    new BooleanList () {
                    FormName = t
                }).ToList ();
            }
        }

        [Test]
        public void A_SingleRun()
        {
            var l = List;

            foreach (var serializer in Serializers)
            {
                for (int n = 0; n < 2;)
                {
                    var stopwatch = Stopwatch.StartNew ();
                    var s = serializer.Serialize (l);
                    var ss = stopwatch.ElapsedMilliseconds;

                    var rl = serializer.Deserialize<List<BooleanList>> (s);
                    var ds = stopwatch.ElapsedMilliseconds;

                    stopwatch.Stop ();

                    Console.WriteLine("Pass #:{0}", ++n);
                    Console.WriteLine (serializer);
                    Console.WriteLine ("Serialization speed  : {0}ms", ss);
                    Console.WriteLine ("Deserialization speed: {0}ms", ds);
                }
            }

        }

        [Test]
        public void B_OneKRun()
        {
            var l = List;

            foreach (var serializer in Serializers)
            {
                string s = string.Empty;
                var stopwatch = Stopwatch.StartNew ();
                for (var n = 0; n < 1000; n++)
                {
                    s = serializer.Serialize (l);
                }
                var ss = stopwatch.ElapsedMilliseconds;

                for (var n = 0; n < 1000; n++)
                {
                    var rl = serializer.Deserialize<List<BooleanList>> (s);
                }
                var ds = stopwatch.ElapsedMilliseconds;

                stopwatch.Stop ();

                Console.WriteLine (serializer.ToString ());
                Console.WriteLine (string.Format ("Serialization speed   (1k): {0}ms", ss));
                Console.WriteLine (string.Format ("Deserialization speed (1k): {0}ms", ds));
            }

        }

        [Test]
        public void C_Single_With_10k_List()
        {
            var l = List10k;

            foreach (var serializer in Serializers)
            {
                string s = string.Empty;
                var stopwatch = Stopwatch.StartNew ();
//                for (var n = 0; n < 1000; n++)
//                {
                    s = serializer.Serialize (l);
//                }
                var ss = stopwatch.ElapsedMilliseconds;

//                for (var n = 0; n < 1000; n++)
//                {
                    var rl = serializer.Deserialize<List<BooleanList>> (s);
//                }
                var ds = stopwatch.ElapsedMilliseconds;

                stopwatch.Stop ();

                Console.WriteLine (serializer.ToString ());
                Console.WriteLine (string.Format ("Serialization speed   (1x10k): {0}ms", ss));
                Console.WriteLine (string.Format ("Deserialization speed (1x10k): {0}ms", ds));
            }

        }

        [Test]
        public void D_10_With_10k_List()
        {
            var l = List10k;

            foreach (var serializer in Serializers)
            {
                string s = string.Empty;
                var stopwatch = Stopwatch.StartNew ();
                for (var n = 0; n < 10; n++)
                {
                    s = serializer.Serialize (l);
                }
                var ss = stopwatch.ElapsedMilliseconds;

                for (var n = 0; n < 10; n++)
                {
                    var rl = serializer.Deserialize<List<BooleanList>> (s);
                }

                var ds = stopwatch.ElapsedMilliseconds;

                stopwatch.Stop ();

                Console.WriteLine (serializer.ToString ());
                Console.WriteLine (string.Format ("Serialization speed   (1k x10k): {0}ms", ss));
                Console.WriteLine (string.Format ("Deserialization speed (1k x10k): {0}ms", ds));
            }

        }
    }
}

