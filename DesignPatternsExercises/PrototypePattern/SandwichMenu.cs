namespace PrototypePattern
{
    using System.Collections.Generic;

    public class SandwichMenu
    {
        private readonly Dictionary<string, SandwichPrototype> _sandwiches = new Dictionary<string, SandwichPrototype>();

        public SandwichPrototype this[string name]
        {
            get
            {
                return _sandwiches[name];
            }
            set
            {
                this._sandwiches[name] = value;
            }
        }
    }
}
