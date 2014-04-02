using System;

namespace StackOverflowSamples
{
    public class TestUser
    {
        public const string DummyResponse = 
            "{\"status\":\"SUCCESS\",\"value\":{\"id\":185,\"name\":\"Joe S\",\"number\":\"15555555555\"}}";

        public string Name { get; set; }
        public int Id { get; set; }
        public string Number { get; set; }
    }

    public class ServiceResponse<T>
    {
        public string Status { get; set; }

        public T Value { get; set; }
    }
}

