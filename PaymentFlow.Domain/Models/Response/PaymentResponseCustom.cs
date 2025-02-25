namespace PaymentFlow.Domain.Models.Response
{
    public class PaymentResponseCustom
    {
        public PaymentResponseCustom
        (
            decimal totalCurrencyDaily,
            IEnumerable<PaymentResponse> paymentsList
        )
        {
            TotalCurrencyDaily = totalCurrencyDaily;
            PaymentsList = paymentsList;
        }

        public decimal TotalCurrencyDaily { get; }
        public IEnumerable<PaymentResponse> PaymentsList { get; }
    }
}
