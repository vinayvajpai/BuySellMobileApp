using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.Services;
using BuySell.View;
using BuySell.Views;
using BuySell.WebServices;
using Newtonsoft.Json;
using Stripe;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class AddCardViewModel : BaseViewModel
    {
        #region Constructor
        Action<CardModel> callBackResponse = null;
        public AddCardViewModel(INavigation _nav)
        {
            navigation = _nav;
        }
        public AddCardViewModel(INavigation _nav, CardListModel objcard)
        {
            navigation = _nav;
            SelectedEditCard = objcard;
        }
        public AddCardViewModel(INavigation _nav, Action<CardModel> action)
        {
            navigation = _nav;
            callBackResponse = action;
        }
        public AddCardViewModel(INavigation _nav, Action<CardModel> action, bool IsEdit)
        {
            navigation = _nav;
            callBackResponse = action;
            IsEditCard = IsEdit;
            if(IsEdit)
            {
                PageTitle = "Edit Card";
            }
            else
            {
                PageTitle = "Add Card";
            }
        }
        #endregion

        #region Properties

        public string PageTitle { get; set; } = "Add Card";

        private CardModel _addCardModel = new CardModel()
        {
            State = Global.SelectedState
        };

        public CardModel addCardModel
        {
            get { return _addCardModel; }
            set
            {
                _addCardModel = value;
                OnPropertyChanged("addCardModel");
            }
        }

        private CardListModel _SelectedEditCard;
        public CardListModel SelectedEditCard
        {
            get
            {
                return _SelectedEditCard;
            }
            set
            {
                _SelectedEditCard = value;
                OnPropertyChanged(nameof(SelectedEditCard));
                if (SelectedEditCard != null)
                {
                    // edit card will work here.
                }
            }
        }



        private bool _IsShippingAddressToo;

        public bool IsShippingAddressToo
        {
            get { return _IsShippingAddressToo; }
            set
            {
                _IsShippingAddressToo = value;
                OnPropertyChanged(nameof(IsShippingAddressToo));
            }
        }

        private bool _IsEditCard = false;

        public bool IsEditCard
        {
            get { return _IsEditCard; }
            set
            {
                _IsEditCard = value;
                OnPropertyChanged(nameof(IsEditCard));
            }
        }

        #endregion

        #region Command
        private ICommand _BackCommand;
        public ICommand BackCommand
        {
            get
            {
                if (_BackCommand == null)
                {
                    _BackCommand = new Command(() =>
                    {
                        try
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            MessagingCenter.Unsubscribe<object, string>("SelectStateValue", "SelectStateValue");
                            navigation.PopAsync();
                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                            Debug.WriteLine(ex.Message);
                        }
                    });
                }

                return _BackCommand;
            }
        }

        private ICommand _SelectStateCommand;
        public ICommand SelectStateCommand
        {
            get
            {
                if (_SelectStateCommand == null)
                {
                    _SelectStateCommand = new Command(() =>
                    {
                        try
                        {
                            if (IsTap)
                                return;
                            IsTap = true;
                            navigation.PushAsync(new CountryListView());
                            MessagingCenter.Subscribe<object, string>("SelectStateValue", "SelectStateValue", (sender, arg) =>
                            {
                                if (arg != null)
                                {
                                    addCardModel.State = arg.ToString();
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            IsTap = false;
                            Debug.WriteLine(ex.Message);
                        }
                    });
                }

                return _SelectStateCommand;
            }
        }

        #endregion

        public async void AddCardmethod()
        {
            if (!string.IsNullOrWhiteSpace(addCardModel.Number) && !string.IsNullOrWhiteSpace(addCardModel.Expire) && !string.IsNullOrWhiteSpace(addCardModel.Cvc)
                && !string.IsNullOrWhiteSpace(addCardModel.PhoneNo) && !string.IsNullOrWhiteSpace(addCardModel.Name) && !string.IsNullOrWhiteSpace(addCardModel.AddressLine1)
               && !string.IsNullOrWhiteSpace(addCardModel.State) && !string.IsNullOrWhiteSpace(addCardModel.ZipCode))
            {

                if (IsTap)
                    return;
                IsTap = true;

                await addcardwithtoken();
                if (callBackResponse != null)
                {
                    callBackResponse.Invoke(addCardModel);
                }
            }
            else
            {
                IsTap = false;
            }
        }

        public async Task SaveAddMethod()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                if (string.IsNullOrEmpty(addCardModel.Name))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter name");
                    IsTap = false;
                    return;
                }
                if (string.IsNullOrEmpty(addCardModel.AddressLine1))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter addressline1");
                    IsTap = false;
                    return;
                }
                //if (string.IsNullOrEmpty(addCardModel.City))
                //{
                //    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter city");
                //    IsTap = false;
                //    return;
                //}
                if (string.IsNullOrEmpty(addCardModel.State))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter state");
                    IsTap = false;
                    return;
                }
                if (string.IsNullOrEmpty(addCardModel.ZipCode))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter zip code");
                    IsTap = false;
                    return;
                }


                var AddAddModel = new AddAddressModel()
                {
                    City = addCardModel.City,
                    Country = addCardModel.Country,
                    AddressLine1 = addCardModel.AddressLine1,
                    AddressLine2 = addCardModel.AddressLine2,
                    State =Global.GetTwoLetterforState(addCardModel.State),
                    ZipCode = addCardModel.ZipCode,
                    PhoneNo = addCardModel.PhoneNo,
                    Name = addCardModel.Name,
                    IsDefault = addCardModel.IsDefault,
                    IsBilling = true,
                    FullName = addCardModel.Name
                };

                if (IsShippingAddressToo)
                {
                    AddAddModel.IsBillingShippingSame = true;
                }
                else
                {
                    AddAddModel.IsBillingShippingSame = false;
                }
                try
                {

                    string methodUrl = string.Empty;
                    methodUrl = $"/api/Account/AddShippingAddress";
                    if (Constant.LoginUserData != null)
                    {
                        AddAddModel.UserId = Constant.LoginUserData.Id;
                        AddAddModel.State = Global.GetTwoLetterforState(AddAddModel.State);
                    }
                    if (!string.IsNullOrWhiteSpace(methodUrl))
                    {
                        RestResponseModel responseModel = await WebService.PostData(AddAddModel, methodUrl, true);
                        if (responseModel != null)
                        {
                            if (responseModel.status_code == 200)
                            {
                                var Address = JsonConvert.DeserializeObject<AddAddressModel>(responseModel.response_body);
                                Global.AlertWithAction(IsEditCard ? "Card Updated successfully!!" : "Card added successfully!!", async () =>
                                {
                                    if (IsShippingAddressToo)
                                        Constant.globalSelectedAddress = Address;

                                    try
                                    {
                                        bool doesPageExists = navigation.NavigationStack.Any(p => p is OrderDetailPage);
                                        if (doesPageExists)
                                        {
                                            await Global.PopToPage<OrderDetailPage>(navigation);
                                        }
                                        else
                                        {
                                            await navigation.PopAsync();
                                        }

                                        //bool doesPageExists = navigation.NavigationStack.Any(p => p is OrderDetailPage);
                                        //if (doesPageExists)
                                        //{
                                        //    for (var counter = 1; counter < 2; counter++)
                                        //    {
                                        //        navigation.RemovePage(navigation.NavigationStack[navigation.NavigationStack.Count - 2]);
                                        //    }
                                        //    await navigation.PopAsync();
                                        //}
                                        //else
                                        //{
                                        //    await navigation.PopAsync();
                                        //}
                                        IsTap = false;
                                    }
                                    catch (Exception ex)
                                    {
                                        Debug.WriteLine(ex);
                                        IsTap = false;
                                        await navigation.PopAsync();
                                    }
                                });
                                //var alertConfig = new AlertConfig
                                //{
                                //    Message = IsEditCard ? "Card Updated successfully" : "Card added successfully",
                                //    OkText = "OK",
                                //    OnAction = async () =>
                                //    {
                                //        if (IsShippingAddressToo)
                                //            Constant.globalSelectedAddress = AddAddModel;


                                //        try
                                //        {
                                //            bool doesPageExists = navigation.NavigationStack.Any(p => p is OrderDetailPage);
                                //            if (doesPageExists)
                                //            {
                                //                for (var counter = 1; counter < 2; counter++)
                                //                {
                                //                    navigation.RemovePage(navigation.NavigationStack[navigation.NavigationStack.Count - 2]);
                                //                }
                                //                await navigation.PopAsync();
                                //            }
                                //            else
                                //            {
                                //                await navigation.PopAsync();
                                //            }
                                //        }
                                //        catch (Exception ex)
                                //        {
                                //            Debug.WriteLine(ex);
                                //            await navigation.PopAsync();
                                //        }
                                       

                                //    }
                                //};
                                //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                            }
                            else if (responseModel.status_code == 500)
                            {
                                ResponseBodyModel responseBodyModel = JsonConvert.DeserializeObject<ResponseBodyModel>(responseModel.response_body);
                                IsTap = false;
                                Acr.UserDialogs.UserDialogs.Instance.Alert(responseBodyModel.Message);
                                UserDialogs.Instance.HideLoading();
                            }
                            else
                            {
                                IsTap = false;
                                Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                                UserDialogs.Instance.HideLoading();
                            }
                        }
                        else
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else
                    {
                        IsTap = false;
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
                catch (Exception ex)
                {
                    IsTap = false;
                    UserDialogs.Instance.HideLoading();
                    Debug.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                IsTap = false;
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task addcardwithtoken()
        {

            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                if (string.IsNullOrEmpty(addCardModel.Number))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter card no");
                    IsTap = false;
                    return;
                }

                if (addCardModel.Number.Length < 15)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter valid card no");
                    IsTap = false;
                    return;
                }

                if (string.IsNullOrEmpty(addCardModel.Expire))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter expiry date");
                    IsTap = false;
                    return;
                }

                if (addCardModel.Expire.Length < 5)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter valid expiry date");
                    IsTap = false;
                    return;
                }

                if (string.IsNullOrEmpty(addCardModel.Cvc))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter cvv no");
                    IsTap = false;
                    return;
                }
                if (string.IsNullOrEmpty(addCardModel.PhoneNo))
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Please enter phone no");
                    IsTap = false;
                    return;
                }

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Adding Card, Please wait...");
                await Task.Delay(50);
                #region Check card is valid or not
                try
                {
                    var exp = addCardModel.Expire.Split('/');
                    var options = new PaymentMethodCreateOptions
                    {
                        Type = "card",
                        Card = new PaymentMethodCardOptions
                        {
                            Number = addCardModel.Number.Replace(" ", ""),
                            ExpMonth = Convert.ToInt64(exp[0]),
                            ExpYear = 2000 + Convert.ToInt64(exp[1]),
                            Cvc = addCardModel.Cvc,
                        },
                    };
                    var service = new PaymentMethodService();
                    var res = service.Create(options);
                }
                catch (StripeException ex)
                {
                    IsTap = false;
                    
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    await Acr.UserDialogs.UserDialogs.Instance.AlertAsync(Global.PaymentErrorMessage(ex.StripeError.Code));
                    return;
                }
                #endregion

                if (Constant.LoginUserData != null)
                {
                    addCardModel.UserId = Constant.LoginUserData.Id;
                }

                IStripePaymentService stripePaymentService = DependencyService.Get<IStripePaymentService>();
                var token = await stripePaymentService.SavePaymentMethod(addCardModel);
                if (token == null)
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert("Unable to tokenize card, please  try later");
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    IsTap = false;
                    return;
                }

                //var token = stripePaymentService.GeneratePaymentToken(addCardModel);
                // var tokenResult = await generatecardToken(addCardModel.Number, Convert.ToString(addCardModel.ExpMonth), Convert.ToString(addCardModel.ExpYear), addCardModel.Cvc);

                var cardno = addCardModel.Number.Replace(" ", "");
                CCardModel RequestforAddcard = new CCardModel()
                {
                    PaymentId = 0,
                    UserId = addCardModel.UserId,
                    Token = token.Id.ToString(),
                    CCType = "card",
                    CCLast4 = cardno.Substring(cardno.Length - 4, 4),
                    CCFirst6 = cardno.Substring(0, 6),
                    //AuthReference = token.//token.Card.Id,
                    sCCExpMonthYear = addCardModel.Expire.Replace("/", ""),
                    IsDefault = addCardModel.IsDefault
                };

                string methodUrl = $"/api/Account/UpdateCustomerPayment";
                if (!string.IsNullOrWhiteSpace(methodUrl))
                {
                    Debug.WriteLine(JsonConvert.SerializeObject(RequestforAddcard));

                    RestResponseModel responseModel = await WebService.PostData(RequestforAddcard, methodUrl, true);
                    if (responseModel != null)
                    {
                        if (responseModel.status_code == 200)
                        {
                            var CardData = JsonConvert.DeserializeObject<Cardmodel>(responseModel.response_body);
                            if (CardData != null)
                            {
                                Constant.globalAddedCard = new CardListModel()
                                {
                                    Id = CardData.PaymentId,
                                    UserId = Constant.LoginUserData.Id,
                                    CardToken = CardData.Token,
                                    Span = CardData.CCLast4,
                                    Bin = CardData.CCFirst6,
                                    CardType = CardData.CCType,
                                    IsPrimary = true,
                                    Expiry = CardData.sCCExpMonthYear,
                                    IsDeleted = true,
                                    CreatedDate = DateTime.Now,
                                    CardIcon = CardData.CardIcon,
                                };
                                Global.GlobalCardList.Add(Constant.globalAddedCard);

                                if (Constant.LoginUserData.PGCustomerId == null)
                                {
                                    UpdatePGRequestModel updatePGRequestModel = new UpdatePGRequestModel();
                                    updatePGRequestModel.UserId = Constant.LoginUserData.Id;
                                    updatePGRequestModel.PGCustomerId = token.CustomerId;
                                    methodUrl = $"/api/Account/UpdateUserPGCustomerId";
                                    RestResponseModel responsePGModel = await WebService.PostData(updatePGRequestModel, methodUrl, true);
                                    if(responsePGModel != null)
                                    {
                                        if (responsePGModel.status_code == 200)
                                        {
                                            Constant.LoginUserData.PGCustomerId = token.CustomerId;
                                            Global.SetValueInProperties("LoginUserData", JsonConvert.SerializeObject(Constant.LoginUserData));
                                        }
                                    }
                                }
                                await SaveAddMethod();
                            }
                            else if (responseModel.status_code == 500)
                            {
                                ResponseBodyModel responseBodyModel = JsonConvert.DeserializeObject<ResponseBodyModel>(responseModel.response_body);
                                IsTap = false;
                                Acr.UserDialogs.UserDialogs.Instance.Alert(responseBodyModel.Message);
                                UserDialogs.Instance.HideLoading();
                            }
                            else
                            {
                                IsTap = false;
                                Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                                UserDialogs.Instance.HideLoading();
                            }
                        }
                        else
                        {
                            IsTap = false;
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else
                    {
                        IsTap = false;
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else
                {
                    IsTap = false;
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                }


            }
            catch (Exception ex)
            {
                IsTap = false;
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                UserDialogs.Instance.HideLoading();
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task<cardAuthResponse> generatecardToken(string cardno, string expmonth, string expyear4digit, string cvv)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.stripe.com/v1/tokens");
                request.Headers.Add("Authorization", "Basic c2tfdGVzdF80ZUMzOUhxTHlqV0Rhcmp0VDF6ZHA3ZGM6");
                var collection = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("card[number]", cardno),
                new KeyValuePair<string, string>("card[exp_month]", expmonth),
                new KeyValuePair<string, string>("card[exp_year]", expyear4digit),
                new KeyValuePair < string, string >("card[cvc]", cvv)
            };
                var content = new FormUrlEncodedContent(collection);
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<cardAuthResponse>(result);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }

        }

    }
}

