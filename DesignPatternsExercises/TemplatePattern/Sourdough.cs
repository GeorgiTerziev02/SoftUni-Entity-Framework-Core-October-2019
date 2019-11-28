﻿namespace TemplatePattern
{
    using System;

    public class Sourdough : Bread
    {
        public override void Bake()
        {
            Console.WriteLine($"Gathering Ingridients for Sourdough Bread.");
        }

        public override void MixIngridients()
        {
            Console.WriteLine($"Baking the Sourdough Bread. (20 minutes)");
        }
    }
}
