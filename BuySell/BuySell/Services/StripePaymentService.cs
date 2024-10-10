using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BuySell.Helper;
using BuySell.Model;
using Newtonsoft.Json;
using Stripe;

namespace BuySell.Services
{
    public class StripePaymentService : IStripePaymentService
    {
        public Token GeneratePaymentToken(CardModel cardModel)
        {
            StripeConfiguration.ApiKey = Constant.stripSecertAPIKey;
            var option = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = cardModel.Number,
                        ExpMonth = cardModel.ExpMonth.ToString(),
                        ExpYear = cardModel.ExpYear.ToString(),
                        Cvc = cardModel.Cvc,
                        Currency = "USD",
                        Name = cardModel.Name,
                        AddressCity = cardModel.City,
                        AddressZip = cardModel.ZipCode,
                        AddressLine1 = cardModel.AddressLine1,
                        AddressCountry = cardModel.State
                    }
                };
                var service = new TokenService();
                var token = service.Create(option);


            return token;
        }

        async public Task<Charge> PayWithCard(PaymentModel paymentModel)
        {
            try
            {
                
                    var chargeOptions = new ChargeCreateOptions
                    {
                        Amount = paymentModel.Amount,
                        Currency = "USD",
                        Source = paymentModel.Token,
                        Description = paymentModel.Description,
                        Metadata = paymentModel.metaData,
                        ReceiptEmail = "dhirajumalebuysell@gmail.com"
                    };
                    var service = new ChargeService();
                    var response = await service.CreateAsync(chargeOptions);
                    return response;
                
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        async public Task<PaymentIntent> PayWithCartIntent(PaymentModel paymentModel)
        {
            try
            {
                var servicePM = new PaymentMethodService();
                var pmDetails = servicePM.Get(paymentModel.Token);
                if (pmDetails != null)
                {
                    var options1 = new PaymentIntentCreateOptions
                    {
                        Amount = paymentModel.Amount,
                        Currency = "USD",
                        PaymentMethod = paymentModel.Token,
                        Description = paymentModel.Description,
                        Metadata = paymentModel.metaData,
                        Customer = pmDetails.CustomerId,
                        ReceiptEmail=Constant.LoginUserData.Email,
                        
                    };
                    var service1 = new PaymentIntentService();
                    var PI = await service1.CreateAsync(options1);

                    //To confirm the payment
                    var options = new PaymentIntentConfirmOptions
                    {
                        PaymentMethod = paymentModel.Token
                    };
                    var service = new PaymentIntentService();
                    var pIC= service.Confirm(PI.Id, options);

                    return pIC;
                }
                return null;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public Task<Refund> RefundPayment(string charge_Id)
        {
            try
            {
                var options = new RefundCreateOptions
                {
                    Charge = charge_Id,
                    Reason ="Due to some technical issue, payment is not completed."
                };
                var service = new RefundService();
                return service.CreateAsync(options);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        async public Task<PaymentMethod> SavePaymentMethod(CardModel cardModel)
        {
            try
            {
                StripeConfiguration.ApiKey = Constant.stripSecertAPIKey;
                Customer customer = null;
                //To Create customer method
                if(Constant.LoginUserData.PGCustomerId == null)
                {
                    var optionsC = new CustomerCreateOptions
                    {
                        Address = new AddressOptions()
                        {
                            PostalCode = cardModel.ZipCode,
                            Line1 = cardModel.AddressLine1,
                            State = cardModel.State,
                            Line2 = cardModel.AddressLine2,
                            Country = cardModel.Country
                        }
                    };
                    var serviceC = new CustomerService();
                    customer = serviceC.Create(optionsC);
                }

                //To Create payment method
                var options = new PaymentMethodCreateOptions
                {
                    Type = "card",
                    Card = new PaymentMethodCardOptions
                    {
                        Number = cardModel.Number,
                        ExpMonth = cardModel.ExpMonth,
                        ExpYear = cardModel.ExpYear,
                        Cvc = cardModel.Cvc,
                    },
                    BillingDetails = new PaymentMethodBillingDetailsOptions() {
                        Name = cardModel.Name,
                        Address = new AddressOptions()
                        {
                            PostalCode = cardModel.ZipCode,
                            Line1 = cardModel.AddressLine1,
                            State = cardModel.State,
                            Line2 =cardModel.AddressLine2,
                            Country = cardModel.Country,
                            
                        }
                    }
                };
                var service = new PaymentMethodService();
                var pm = await service.CreateAsync(options);

                //To attached the paymentmethod to customer
                var optionPMA = new PaymentMethodAttachOptions
                {
                    Customer = Constant.LoginUserData.PGCustomerId==null?customer.Id: Constant.LoginUserData.PGCustomerId,
                };
                var servicePMA = new PaymentMethodService();
                var pmA = servicePMA.Attach(pm.Id, optionPMA);
                return pmA;
                
            }
            catch (Exception ex)
            {

            }
            return null;
        }

    }
}

