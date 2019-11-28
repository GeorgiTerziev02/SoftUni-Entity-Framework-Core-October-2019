namespace TemplatePattern
{
    using System;

    public abstract class Bread
    {
        public abstract void MixIngridients();

        public abstract void Bake();

        public virtual void Slice()
        {
            Console.WriteLine($"Slicing the {this.GetType().Name} bread!");
        }

        //Template Method
        public void Make()
        {
            this.MixIngridients();
            this.Bake();
            this.Slice();
        }
    }
}
