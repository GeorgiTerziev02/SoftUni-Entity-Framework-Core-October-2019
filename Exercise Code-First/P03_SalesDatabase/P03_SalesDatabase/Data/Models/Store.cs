namespace P03_SalesDatabase.Data.Models
{
    using System.Collections.Generic;

    public class Store
    {
        public int StoreId { get; set; }

        public int Name { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
