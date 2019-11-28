namespace TemplatePattern
{
    using System;

    public class WholeWheat : Bread
    {
        public override void Bake()
        {
            Console.WriteLine($"Gathering Ingridients for Whole Wheat Bread.");
        }

        public override void MixIngridients()
        {
            Console.WriteLine($"Baking the Whole Wheat Bread. (15 minutes)");
        }
    }
}
