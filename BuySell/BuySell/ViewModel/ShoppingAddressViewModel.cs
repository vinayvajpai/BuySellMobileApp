using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Acr.UserDialogs;
using BuySell.Helper;
using BuySell.Model;
using BuySell.Model.RestResponse;
using BuySell.Views;
using BuySell.Views.Login_Flow;
using BuySell.WebServices;
using Newtonsoft.Json;
using Stripe;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BuySell.ViewModel
{
    public class ShoppingAddressViewModel : BaseViewModel
    {
        Action<AddAddressModel> callbackAction = null;
        #region Constructor
        public ShoppingAddressViewModel(INavigation _nav)
        {
            navigation = _nav;
        }

        public ShoppingAddressViewModel(INavigation _nav, Action<AddAddressModel> action)
        {
            navigation = _nav;
            callbackAction = action;
        }

        #endregion

        #region Properties
        private ObservableCollection<AddAddressModel> _ShoppingAddList = new ObservableCollection<AddAddressModel>(Helper.Global.GlobalAddressList.ToList());
        public ObservableCollection<AddAddressModel> ShoppingAddList
        {
            get { return _ShoppingAddList; }
            set { _ShoppingAddList = value; OnPropertyChanged("ShoppingAddList"); }
        }

        private AddAddressModel _SelectedAddressModel;
        public AddAddressModel SelectedAddressModel
        {
            get
            {
                return _SelectedAddressModel;
            }
            set
            {
                _SelectedAddressModel = value;
                OnPropertyChanged("SelectedAddressModel");
                if (_SelectedAddressModel != null)
                {
                    var address = value;
                    address.State = Global.GetTwoLetterforState(address.State);
                    Constant.globalSelectedAddress = address;
                    navigation.PopAsync();
                    _SelectedAddressModel = null;
                }
            }
        }

        #endregion

        #region Methods
        public void GetShoppingAddressList()
        {
            try
            {
                //ShoppingAddList.Clear();
                //ShoppingAddList = new ObservableCollection<AddAddressModel>();

            }
            catch (Exception ex)
            {

            }

        }
        #endregion

        #region Command
        private Command _BackCommand;
        public Command BackCommand
        {
            get
            {
                return _BackCommand ?? (_BackCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;

                        IsTap = true;
                        await navigation.PopAsync(true);
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

        private Command _DeleteCommand;
        public Command DeleteCommand
        {
            get
            {
                return _DeleteCommand ?? (_DeleteCommand = new Command<AddAddressModel>(async (objAdd) =>
                {
                    try
                    {
                        if (!Constant.IsConnected)
                        {
                            UserDialogs.Instance.Toast("Internet not available");
                            IsTap = false;
                            return;
                        }

                        if (IsTap)
                            return;

                        IsTap = true;
                        var confirm = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync(Helper.Constant.deleteConfirmMsgStr, okText: Helper.Constant.OKStr, cancelText: Helper.Constant.CancelStr);
                        if (confirm)
                        {
                            
                            //uncomment below line when delete shipping address API applied.
                            //Global.GetAllShippingAddress();
                            try
                            {
                                ShippingDeleteRequest shippingDeleteRequest = new ShippingDeleteRequest();
                                shippingDeleteRequest.AddressLine1 = objAdd.AddressLine1;
                                shippingDeleteRequest.AddressLine2 = objAdd.AddressLine2;
                                shippingDeleteRequest.City = objAdd.City;
                                shippingDeleteRequest.Country = objAdd.Country;
                                shippingDeleteRequest.IsBilling = objAdd.IsBilling;
                                shippingDeleteRequest.IsBillingShippingSame = objAdd.IsBillingShippingSame;
                                shippingDeleteRequest.ShippingAddressId = objAdd.ShippingAddressId;
                                shippingDeleteRequest.State = objAdd.State;
                                shippingDeleteRequest.UserId = Constant.LoginUserData.Id;
                                
                                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Deleting Address, Please wait...");
                                await Task.Delay(50);
                                string methodUrl = $"/api/Account/DeleteShippingAddress?id={Constant.LoginUserData.Id}";

                                if (!string.IsNullOrWhiteSpace(methodUrl))
                                {
                                    RestResponseModel responseModel = await WebService.PostData(shippingDeleteRequest, methodUrl, true);
                                    if (responseModel != null)
                                    {
                                        if (responseModel.status_code == 200)
                                        {
                                            Global.AlertWithAction("Address removed successfully!!", async () =>
                                            {
                                                IsTap = false;
                                                GetAllShippingAddress();
                                            });
                                            //var alertConfig = new AlertConfig
                                            //{
                                            //    Message = "Address removed successfully",
                                            //    OkText = "OK",
                                            //    OnAction = async () =>
                                            //    {
                                            //        IsTap = false;
                                            //        GetAllShippingAddress();
                                            //    }
                                            //};
                                            if(Constant.globalSelectedAddress == objAdd)
                                            {
                                                Constant.globalSelectedAddress = new AddAddressModel();
                                            }
                                            //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                                            UserDialogs.Instance.HideLoading();
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
                                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message);
                                UserDialogs.Instance.HideLoading();
                            }

                        }
                        IsTap = false;
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }


        private Command _SetAsDefaultCommand;
        public Command SetAsDefaultCommand
        {
            get
            {
                return _SetAsDefaultCommand ?? (_SetAsDefaultCommand = new Command<AddAddressModel>(async (objAdd) =>
                {
                    try
                    {
                        if (!Constant.IsConnected)
                        {
                            UserDialogs.Instance.Toast("Internet not available");
                            IsTap = false;
                            return;
                        }

                        Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Address being set as default, please wait...");
                        await Task.Delay(50);
                        //SelectedAddressModel = objAdd;
                        string methodUrl = $"/api/Account/UpdateShippingAddress";
                        if (Constant.LoginUserData != null)
                        {
                            objAdd.UserId = Constant.LoginUserData.Id;
                            objAdd.IsDefault = true;
                            objAdd.State = Global.GetTwoLetterforState(objAdd.State);
                        }

                        if (!string.IsNullOrWhiteSpace(methodUrl))
                        {
                            Debug.WriteLine(JsonConvert.SerializeObject(objAdd));
                            RestResponseModel responseModel = await WebService.PostData(objAdd, methodUrl, true);
                            if (responseModel != null)
                            {
                                if (responseModel.status_code == 200)
                                {
                                    Global.AlertWithAction("Default address set successfully!!", async () =>
                                    {
                                        Constant.globalSelectedAddress = objAdd;
                                        GetAllShippingAddress();
                                    });
                                    //var alertConfig = new AlertConfig
                                    //{
                                    //    Message = "Default address set successfully",
                                    //    OkText = "OK",
                                    //    OnAction = async () =>
                                    //    {
                                    //        Constant.globalSelectedAddress = objAdd;
                                    //        GetAllShippingAddress();
                                    //    }
                                    //};
                                    //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                                   
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
                                    Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                                }
                               
                            }
                            else
                            {
                                IsTap = false;
                                Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                        Acr.UserDialogs.UserDialogs.Instance.HideLoading();
                    }
                }
             ));
            }
        }

        private Command _EditCommand;
        public Command EditCommand
        {
            get
            {
                return _EditCommand ?? (_EditCommand = new Command<AddAddressModel>(async (objAddress) =>
                {
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;
                        objAddress.State = Global.GetFullNameforState(objAddress.State);
                        var objAddShipPage = new AddShippingAddressPage((addModel) =>
                        {
                            OnPropertyChanged("ShoppingAddList");
                        }, objAddress, true);
                        await navigation.PushAsync(objAddShipPage);
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }

        private Command _AddNewAddressCommand;
        public Command AddNewAddressCommand
        {
            get
            {
                return _AddNewAddressCommand ?? (_AddNewAddressCommand = new Command(async () =>
                {
                    try
                    {
                        if (IsTap)
                            return;

                        var objAddShipPage = new AddShippingAddressPage(async (addModel) =>
                        {
                            //addModel.ShippingAddressId = ShoppingAddList.Select(s => s.ShippingAddressId).Max() + 1;                            
                            ShoppingAddList.Add(addModel);
                            Helper.Global.GlobalAddressList.Add(addModel);
                            OnPropertyChanged("ShoppingAddList");
                        });
                        IsTap = true;
                       await navigation.PushAsync(objAddShipPage);
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                    }
                }
             ));
            }
        }


        #region getallShipping Addresses
        public async void GetAllShippingAddress()
        {
            try
            {
                if (!Constant.IsConnected)
                {
                    UserDialogs.Instance.Toast("Internet not available");
                    IsTap = false;
                    return;
                }

                Acr.UserDialogs.UserDialogs.Instance.ShowLoading("Loading, Please wait..");
                await Task.Delay(50);
                string methodUrl = $"/api/Account/ListShippingAddress?id={Constant.LoginUserData.Id}";
                if (!string.IsNullOrWhiteSpace(methodUrl))
                {
                    RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
                    if (responseModel != null)
                    {
                        if (responseModel.status_code == 200)
                        {
                           var ShipAddList  = JsonConvert.DeserializeObject<ObservableCollection<AddAddressModel>>(responseModel.response_body);

                            if (ShipAddList != null)
                            {
                                //if (ShoppingAddList.Count > 0)
                                //    Constant.globalSelectedAddress = ShoppingAddList.Where(account => account.IsDefault == true).FirstOrDefault();
                                //if(Constant.globalSelectedAddress==null)
                                //{

                                //}
                                if(ShipAddList.Count > 0)
                                {
                                    ShipAddList.ForEach(x => x.State = Global.GetTwoLetterforState(x.State));
                                    ShoppingAddList = ShipAddList;
                                }
                                else
                                {
                                    Constant.globalSelectedAddress = new AddAddressModel();
                                    ShoppingAddList = new ObservableCollection<AddAddressModel>();
                                }
                            }
                            else
                            {
                                Constant.globalSelectedAddress = new AddAddressModel();
                                ShoppingAddList = new ObservableCollection<AddAddressModel>();
                            }
                            UserDialogs.Instance.HideLoading();
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
                            Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
                            UserDialogs.Instance.HideLoading();
                        }
                    }
                    else
                    {
                        Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                        UserDialogs.Instance.HideLoading();
                    }
                }
                else
                {
                    Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                Debug.WriteLine(ex.Message);
            }
        }
        #endregion


        //public async void GetAllShippingAddress()
        //{
        //    try
        //    {
        //        string methodUrl = $"/api/Account/ListShippingAddress?id={Constant.LoginUserData.Id}";
        //        if (!string.IsNullOrWhiteSpace(methodUrl))
        //        {
        //            RestResponseModel responseModel = await WebService.GetData(methodUrl, true);
        //            if (responseModel != null)
        //            {
        //                if (responseModel.status_code == 200)
        //                {
        //                    ShoppingAddList = Global.GlobalAddressList = JsonConvert.DeserializeObject<ObservableCollection<AddAddressModel>>(responseModel.response_body);

        //                    if (Global.GlobalAddressList != null)
        //                    {
        //                        if (Global.GlobalAddressList.Count > 0)
        //                            Constant.globalSelectedAddress = Global.GlobalAddressList.Where(account => account.IsDefault == true).FirstOrDefault();
        //                        else
        //                            Constant.globalSelectedAddress = new AddAddressModel();
        //                    }
        //                    else
        //                    {
        //                        Constant.globalSelectedAddress = new AddAddressModel();
        //                    }
        //                }
        //                else
        //                {
        //                    IsTap = false;
        //                    Acr.UserDialogs.UserDialogs.Instance.Alert(responseModel.ErrorMessage);
        //                    UserDialogs.Instance.HideLoading();
        //                }
        //            }
        //            else
        //            {
        //                IsTap = false;
        //                Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
        //                UserDialogs.Instance.HideLoading();
        //            }
        //        }
        //        else
        //        {
        //            IsTap = false;
        //            Acr.UserDialogs.UserDialogs.Instance.Alert(Constant.ErrorMessage);
        //            UserDialogs.Instance.HideLoading();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        IsTap = false;
        //        UserDialogs.Instance.HideLoading();
        //        Debug.WriteLine(ex.Message);
        //    }
        //}

        #endregion
    }
}
