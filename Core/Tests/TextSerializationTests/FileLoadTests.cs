using System;
using System.Collections.Generic;
using SimplyMobile.Text;
using System.IO;

#if WINDOWS_PHONE
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using TestFixture = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestClassAttribute;
using Test = Microsoft.VisualStudio.TestPlatform.UnitTestFramework.TestMethodAttribute;
#else
using NUnit.Framework;
#endif

namespace TextSerializationTests
{
    [TestFixture ()]
    public abstract class FileLoadTests
    {
        /// <summary>
        /// Gets the deserializer to use.
        /// </summary>
        protected abstract ITextSerializer Deserializer { get; }
        
        [Test()]
        public void DicosTest()
        {
            LoopTest (this.Deserializer.GetType().FullName, this.Deserializer.Deserialize<DictionaryData>, GetDicosPath(), 10000);
        }

        [Test()]
        public void TinyTest()
        {
            LoopTest (this.Deserializer.GetType().FullName, this.Deserializer.Deserialize<TinyPerson>, GetTinyPath(), 100000);
        }

        [Test()]
        public void TinyTestSinDictionary()
        {
            LoopTest (this.Deserializer.GetType().FullName, this.Deserializer.Deserialize<TinyPersonNoDic>, GetTinyPath(), 100000);
        }

        [Test()]
        public void HighlyNestedTest()
        {
            LoopTest (this.Deserializer.GetType().FullName, this.Deserializer.Deserialize<HighlyNested>, GetHighlyNestedPath(), 10000);
        }

        public static string GetDicosPath()
        {
            return System.IO.Path.Combine (
                System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal),
                "dicos.json");
        }

        public static string GetTinyPath()
        {
            return System.IO.Path.Combine (
                System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal),
                "tiny.json");
        }

        public static string GetHighlyNestedPath()
        {
            return System.IO.Path.Combine (
                System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal),
                "highly_nested.json");
        }

        private void LoopTest(string parserName, Func<string, object> parseFunc, string testFile, int count)
        {
            Console.WriteLine (parserName);
            System.Threading.Thread.MemoryBarrier();
            var initialMemory = System.GC.GetTotalMemory(true);

            string json;
            using (var streamReader = new StreamReader(testFile))
            {
                json = streamReader.ReadToEnd();
            }

            var st = DateTime.Now;
            var l = new List<object>();
            for (var i = 0; i < count; i++)
                l.Add(parseFunc(json));
            var tm = (int)DateTime.Now.Subtract(st).TotalMilliseconds;

            System.Threading.Thread.MemoryBarrier();
            var finalMemory = System.GC.GetTotalMemory(true);
            var consumption = finalMemory - initialMemory;

            System.Diagnostics.Debug.Assert(l.Count == count);
            Console.WriteLine (parserName);
            Console.WriteLine ("Count: {0}k", count / 1000);
            Console.WriteLine("... Done, in {0} ms. Throughput: {1} characters / second.", tm.ToString("0,0"), (1000 * (decimal)(count * json.Length) / (tm > 0 ? tm : 1)).ToString("0,0.00"));
            Console.WriteLine();
            Console.WriteLine("\tMemory used : {0}", ((decimal)finalMemory).ToString("0,0"));
            Console.WriteLine();

            GC.Collect();
            Console.WriteLine("Press a key...");
            Console.WriteLine();
        }
    }
}

