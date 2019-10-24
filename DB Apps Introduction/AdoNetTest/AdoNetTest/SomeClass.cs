namespace AdoNetTest
{
    using System;

    public class SomeClass : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Final");
        }
    }
}
