using inveroryManagerApp.Models;

namespace inveroryManagerApp.Event
{
  public class OrderProcessedEventArgs
  {
        public OrderModel ProcessedOrder { get; }

        public OrderProcessedEventArgs(OrderModel processedOrder)
        {
            ProcessedOrder = processedOrder;
        }
    }
}
