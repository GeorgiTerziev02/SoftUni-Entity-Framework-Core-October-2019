namespace PrototypePattern
{
    using System;

    public class Sandwich : SandwichPrototype
    {
        private string bread;
        private string meat;
        private string cheese;
        private string vegies;

        public Sandwich(string bread, string meat, string cheese, string vegies)
        {
            this.bread = bread;
            this.meat = meat;
            this.cheese = cheese;
            this.vegies = vegies;
        }

        public override SandwichPrototype Clone()
        {
            string ingridients = GetIngridientsList();
            Console.WriteLine($"Cloning sandwich with with ingridients: {ingridients}");

            return this.MemberwiseClone() as SandwichPrototype;
        }

        private string GetIngridientsList()
        {
            return $"{this.bread}, {this.meat}, {this.cheese}, {this.vegies}";
        }
    }
}
