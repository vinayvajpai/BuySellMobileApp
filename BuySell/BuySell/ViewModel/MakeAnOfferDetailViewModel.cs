using System;
using System.Collections.ObjectModel;
using BuySell.Helper;
using Xamarin.Forms;
using BuySell.Model;
using System.Linq;
using Acr.UserDialogs;

namespace BuySell.ViewModel
{
    public class MakeAnOfferDetailViewModel : BaseViewModel
    {
        #region Constructor
        public MakeAnOfferDetailViewModel(INavigation _nav, DashBoardModel dataModel, string offerPrice)
        {
            navigation = _nav;
            ProdataModel = dataModel;
            if (offerPrice != null)
            {
                offerPrice = offerPrice.Replace("$", "");
                OfferPrice = Convert.ToDouble(offerPrice);
            }
            else
            {
                OfferPrice = Convert.ToDouble(ProdataModel.OfferPrice);
            }

        }
        #endregion

        #region Property
        private DashBoardModel _ProdataModel;
        public DashBoardModel ProdataModel
        {
            get { return _ProdataModel; }
            set { _ProdataModel = value; OnPropertyChanged("ProdataModel"); }
        }

        private double _OfferPrice;
        public double OfferPrice
        {
            get { return _OfferPrice; }
            set { _OfferPrice = value; OnPropertyChanged("OfferPrice"); }
        }
        #endregion

        #region Methods
        public async void SendOffer()
        {
            try
            {
                var res = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync("Are you sure you want to commit to this offer?", "Confirmation", "Yes", "Cancel");
                if (res)
                {
                    DashBoardModel dashBoardModel = new DashBoardModel
                    {
                        Id = ProdataModel.Id,
                        ProductName = ProdataModel.ProductName,
                        Description = ProdataModel.Description,
                        ProductCategory = ProdataModel.ProductCategory,
                        ParentCategory = ProdataModel.ParentCategory,
                        Gender = ProdataModel.Gender,
                        Price = ProdataModel.Price,
                        Size = ProdataModel.Size,
                        SizeValue = ProdataModel.SizeValue,
                        otherImages = ProdataModel.otherImages,
                        ProductImage = ProdataModel.ProductImage,
                        Availability = ProdataModel.Availability,
                        //ProductRating = ProdataModel.ProductRating,
                        ProductColor = ProdataModel.ProductColor,
                        ProductCondition = ProdataModel.ProductCondition,
                        quantityModels = ProdataModel.quantityModels,
                        StoreCategory = Global.Storecategory,
                        StoreType = Global.Storecategory,
                        OfferPrice = Convert.ToString(OfferPrice),
                        Brand = ProdataModel.Brand,
                        UserFirstName = ProdataModel.UserFirstName,
                        UserLastName = ProdataModel.UserLastName,
                        UserProfile = ProdataModel.UserProfile,
                        UserName = ProdataModel.UserName,
                    };
                    ProdataModel.OfferPrice = Convert.ToString(OfferPrice);
                    Global.OfferProductList.Add(dashBoardModel);
                    Global.AlertWithAction("Offer added Successfully!!", () =>
                    {
                        navigation.PopModalAsync();
                    });
                    //var alertConfig = new AlertConfig
                    //{
                    //    Message = "Offer added Successfully!!",
                    //    OkText = "OK",
                    //    OnAction = () =>
                    //    {
                    //        navigation.PopModalAsync();
                    //    }
                    //};
                    //Acr.UserDialogs.UserDialogs.Instance.Alert(alertConfig);
                }
                else
                {
                    await navigation.PopModalAsync();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Commands
        private Command _SendOfferCommand;
        public Command SendOfferCommand
        {
            get
            {
                return _SendOfferCommand ?? (_SendOfferCommand = new Command(async () =>
                {

                    SendOffer();
                }));
            }
        }
        #endregion
    }
}
