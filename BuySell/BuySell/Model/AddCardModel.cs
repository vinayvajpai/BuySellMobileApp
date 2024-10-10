using System;
using System.Collections.Generic;
using System.Linq;
using BuySell.Helper;
using BuySell.Services;
using BuySell.ViewModel;
using Stripe;

namespace BuySell.Model
{
    public class AddCardModel : ViewModel.BaseViewModel
    {
        public int UserId;
        private string _Number=string.Empty;
        public string Number { get {
                return _Number;
            } set {

                _Number = value;
                OnPropertyChanged("Number");
                OnPropertyChanged("CardIcon");
            } }
        public Nullable<long> ExpMonth { get{
                    if(!string.IsNullOrEmpty(Expire))
                        {
                            return Convert.ToInt64(Expire.Split('/')[0]);
                        }
                        return null;
                    }
                }
        public Nullable<long> ExpYear {
            get
            {
                if (!string.IsNullOrEmpty(Expire))
                {
                    return Convert.ToInt64(Expire.Split('/')[1]);
                }
                return 00;
            }
        }
        public string Cvc { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNo { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        //public string State { get; set; }
        private string _State = string.Empty;
        public string State
        {
            get
            {
                return _State;
            }
            set
            {

                _State = value;
                OnPropertyChanged("State");
            }
        }
        public string Country { get; set; }
        public string Expire { get; set; }
        private string _CardIcon= "defaultcard.png";
        public string CardIcon
        {
            get
            {
                if (string.IsNullOrEmpty(Number))
                {
                    _CardIcon = "defaultcard.png";
                }
                else
                {
                    var cardType = CreditCardService.GetCreditCardType(Number.Replace(" ",""));
                    if (cardType.ToString().ToLower().Equals("mastercard"))
                    {
                        _CardIcon = "Card_Mastercard.png";
                    }
                    else
                    {
                        _CardIcon = "https://checkoutshopper-live.adyen.com/checkoutshopper/images/logos/small/" + cardType.ToString().ToLower() + ".png";
                    }
                    //_CardIcon = Helper.Global.GetCardICon(Number);
                }
                return _CardIcon;
            }
            set
            {
                _CardIcon = value;
                OnPropertyChanged("CardIcon");
            }
        }
        public Token stripToken { get; set; }
    }

    public class CardModel : AddCardModel
    {
        public int ID { get; set; }

        public bool IsDefault { get; set; }
    }

    public class PaymentModel
    {
        /// <summary>
        /// Gets or sets the payment token from client.
        /// </summary>
        public string Token { get; set; }

        public long Amount { get; set; }

        public string Description { get; set; }

        public Dictionary<string, string> metaData { get; set; }
    }

    public class CCardModel : AddCardModel
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public string CCType { get; set; }
        public string CCLast4 { get; set; }
        public string CCFirst6 { get; set; }
        public string AuthReference { get; set; }
        public string sCCExpMonthYear { get; set; }
        public bool IsDefault { get; set; }
      
    }

    public class Card
    {
        public string id { get; set; }
        public string @object { get; set; }
        public object address_city { get; set; }
        public object address_country { get; set; }
        public object address_line1 { get; set; }
        public object address_line1_check { get; set; }
        public object address_line2 { get; set; }
        public object address_state { get; set; }
        public object address_zip { get; set; }
        public object address_zip_check { get; set; }
        public string brand { get; set; }
        public string country { get; set; }
        public string cvc_check { get; set; }
        public object dynamic_last4 { get; set; }
        public int exp_month { get; set; }
        public int exp_year { get; set; }
        public string fingerprint { get; set; }
        public string funding { get; set; }
        public string last4 { get; set; }
        public Metadata metadata { get; set; }
        public object name { get; set; }
        public object tokenization_method { get; set; }
    }

    public class Metadata
    {
    }

    public class cardAuthResponse
    {
        public string id { get; set; }
        public string @object { get; set; }
        public Card card { get; set; }
        public string client_ip { get; set; }
        public int created { get; set; }
        public bool livemode { get; set; }
        public string type { get; set; }
        public bool used { get; set; }
    }


    public class CardListModel :BaseViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CardToken { get; set; }
        public string Span { get; set; }
        public string Bin { get; set; }
        public string CardType { get; set; }
        public bool IsPrimary { get; set; }
        public string Expiry { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CardIcon { get; set; } = "defaultcard.png";
        public string FormattedExpiry
        {
            get
            {
                if (!string.IsNullOrEmpty(Expiry))
                    return Expiry.Insert(2, "/");
                else
                {
                    return null;
                }
            }
        }
    }


    public class Cardmodel
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public string CCType { get; set; }
        public string CCLast4 { get; set; }
        public string CCFirst6 { get; set; }
        public string AuthReference { get; set; }
        public string sCCExpMonthYear { get; set; }
        public bool IsDefault { get; set; }
        public string CardIcon { get; set; } = "defaultcard.png";
    }

    public class UpdatePGRequestModel
    {
         public int UserId { get; set; }
         public string PGCustomerId { get; set; }
    }
}

