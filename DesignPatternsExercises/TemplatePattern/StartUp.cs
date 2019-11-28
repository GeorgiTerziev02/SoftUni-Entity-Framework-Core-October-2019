namespace TemplatePattern
{
    using System;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            TwelveGrain twelveGrainBread = new TwelveGrain();
            Sourdough sourdoughBread = new Sourdough();
            WholeWheat wholeWheatBread = new WholeWheat();

            twelveGrainBread.Make();
            sourdoughBread.Make();
            wholeWheatBread.Make();
        }
    }
}
