namespace ProductShop.Dto
{
    public class UserDetailsDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? Age { get; set; }

        public SoldProductsDto SoldProducts { get; set; }
    }
}
