using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model.RestResponse;
using BuySell.View;
using BuySell.Views.BuyingSellingViews;
using BuySell.WebServices;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuySell.ViewModel
{
    public class OrderDisputeViewModel : BaseViewModel
    {
        public OrderDisputeViewModel(INavigation nav,  BuyingSellingModel buyingSellingModel)
        {
            BuyingProduct = buyingSellingModel;
            navigation = nav;
            SelectedOrder = BuyingProduct.OrderItems.FirstOrDefault().Product;
        }

        private BuyingSellingModel buyingproduct;
        public BuyingSellingModel BuyingProduct
        {
            get
            {
                return buyingproduct;
            }
            set
            {
                buyingproduct = value;
                OnPropertyChanged("BuyingProduct");
            }
        }
        
        private bool _ShowToBuyer;
        public bool ShowToBuyer
        {
            get
            {
                return _ShowToBuyer;
            }
            set
            {
                _ShowToBuyer = value;
                OnPropertyChanged("ShowToBuyer");
            }
        }


        private ViewModel.BuyingSellingProduct _SelectedOrder;
        public ViewModel.BuyingSellingProduct SelectedOrder
        {
            get
            {
                return _SelectedOrder;
            }
            set
            {
                _SelectedOrder = value;
                OnPropertyChanged("SelectedOrder");
            }
        }
        

        private Command _DisputeOfferCmd;
        public Command DisputeOfferCmd
        {
            get
            {
                return _DisputeOfferCmd ?? (_DisputeOfferCmd = new Command(async () =>
                {
                    await DisputeOfferMethod();
                }
             ));
            }
        }


        public async Task DisputeOfferMethod()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Raising dispute, Please wait...");
                await Task.Delay(50);
                string methodUrl = $"/api/Order/DisputeOrder";

                if (!string.IsNullOrWhiteSpace(methodUrl))
                {
                    RestResponseModel responseModel = await WebService.PostData(BuyingProduct, methodUrl, true);
                    Debug.WriteLine(JsonConvert.SerializeObject(BuyingProduct));
                    Debug.WriteLine(JsonConvert.SerializeObject(responseModel));
                    if (responseModel != null)
                    {

                        if (responseModel.status_code == 200)
                        {
                            string shippingLabelResponse = JsonConvert.DeserializeObject<string>(responseModel.response_body);
                            if (shippingLabelResponse != null)
                            {
                                Global.AlertWithAction("Dispute submitted successfully!!", async () =>
                                {
                                    bool doesPageExists = navigation.NavigationStack.Any(p => p is BuyingListView);
                                    if (doesPageExists)
                                    {
                                        await Global.PopToPage<BuyingListView>(navigation);
                                    }
                                    else
                                    {
                                        await navigation.PopAsync();
                                    }
                                });

                                IsTap = false;
                                Global.IsReload = true;
                                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                            }
                            else
                            {
                                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                                Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                                IsTap = false;
                            }
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
                            Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                            IsTap = false;
                        }
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        IsTap = false;
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    IsTap = false;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

}

