using System;
using System.Threading.Tasks;
using BuySell.Model;
using Stripe;

namespace BuySell.Services
{
    public interface IStripePaymentService
    {
        Task<Charge> PayWithCard(PaymentModel paymentModel);
        Task<PaymentIntent> PayWithCartIntent(PaymentModel paymentModel);
        Token GeneratePaymentToken(CardModel cardModel);
        Task<PaymentMethod> SavePaymentMethod(CardModel cardModel);
        Task<Refund> RefundPayment(string charge_Id);
    }
}

