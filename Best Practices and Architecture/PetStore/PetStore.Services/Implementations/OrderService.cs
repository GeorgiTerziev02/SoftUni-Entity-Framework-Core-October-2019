namespace PetStore.Services.Implementations
{
    using PetStore.Data;

    public class OrderService : IOrderService
    {
        private readonly PetStoreDbContext data;

        public OrderService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public void CompleteOrder(int OrderId)
        {
            var order = this.data
                .Orders
                .Find(OrderId);

            order.Status = Data.Models.OrderStatus.Done;

            this.data.SaveChanges();
        }
    }
}
