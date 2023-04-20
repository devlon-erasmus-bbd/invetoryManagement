using inveroryManagerApp.Models;

namespace inveroryManagerApp.Event
{
  public class OrderProcessor
  {
        public event EventHandler<OrderProcessedEventArgs> OrderProcessed;

        public void ProcessOrder(OrderModel order)
        {
            // Process the order

            // Raise the OrderProcessed event
            OrderProcessed?.Invoke(this, new OrderProcessedEventArgs(order));
        }
    }
}
