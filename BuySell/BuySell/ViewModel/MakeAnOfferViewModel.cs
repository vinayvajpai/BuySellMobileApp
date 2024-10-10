using System;
using BuySell.Views;
using Xamarin.Forms;
using BuySell.Model;
using System.Diagnostics;

namespace BuySell.ViewModel
{
    public class MakeAnOfferViewModel : BaseViewModel
    {
        #region Constructor
        private readonly Services.IMessageService _messageService;
        public MakeAnOfferViewModel(INavigation nav, DashBoardModel prodDataModel)
        {
            this._messageService = DependencyService.Get<Services.IMessageService>();
            navigation = nav;
            ProdataModel = prodDataModel;
            if (prodDataModel != null)
            {
                ListPrice = prodDataModel.Price;
                OfferPrice = prodDataModel.OfferPrice!= null ? prodDataModel.OfferPrice :"" ;
            }
        }
        #endregion

        #region Properties
        private DashBoardModel _ProdataModel;
        public DashBoardModel ProdataModel
        {
            get { return _ProdataModel; }
            set { _ProdataModel = value; OnPropertyChanged("ProdataModel"); }
        }

        private string _ListPrice;
        public string ListPrice
        {
            get { return _ListPrice; }
            set { _ListPrice = value; OnPropertyChanged("ListPrice"); }
        }

        private string _OfferPrice = "$";
        public string OfferPrice
        {
            get { return _OfferPrice; }
            set
            {
                _OfferPrice = value;
                if (_OfferPrice.Length == 0 && string.IsNullOrEmpty(_OfferPrice))
                {
                    _OfferPrice = "$";
                }
                OnPropertyChanged("OfferPrice");
            }
        }
        #endregion

        #region Commands
        private Command _MakeAnOfferDetail;
        public Command MakeAnOfferDetail
        {
            get { return _MakeAnOfferDetail ?? (_MakeAnOfferDetail = new Command(async () => {
                if (!string.IsNullOrEmpty(OfferPrice) && !string.IsNullOrWhiteSpace(OfferPrice) && !OfferPrice.Equals("$"))
                {
                    try
                    {
                        if (IsTap)
                            return;
                        IsTap = true;
                        await navigation.PushAsync(new MakeAnOfferDetailPage(ProdataModel, OfferPrice));
                    }
                    catch (Exception ex)
                    {
                        IsTap = false;
                        Debug.WriteLine(ex.Message);
                    }
                    
                }
                else
                {
                    IsTap = false;
                    await _messageService.ShowAsync("Please enter offer price.");
                    return;
                }
                }
                ));
             }
        }
        #endregion
    }
}
