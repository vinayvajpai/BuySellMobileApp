using System;
using System.ComponentModel;
using BuySell.Helper;
using BuySell.ViewModel;
namespace BuySell.Model
{
    public class AddAddressModel : BaseViewModelWithoutProperty
    {
        public int UserId;

        public int ShippingAddressId;
        public bool IsBilling { get; set; } = false;
        public bool IsBillingShippingSame { get; set; } = true;

        private string _Name = string.Empty;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }

        private string _PhoneNo;
        public string PhoneNo
        {
            get
            {
                return _PhoneNo;
            }
            set
            {
                _PhoneNo = value;
                OnPropertyChanged("PhoneNo");
            }
        }

        private string _AddressLine1 = string.Empty;
        public string AddressLine1
        {
            get
            {
                return _AddressLine1;
            }
            set
            {
                _AddressLine1 = value;
                OnPropertyChanged("AddressLine1");
            }
        }
        private string _AddressLine2 = string.Empty;
        public string AddressLine2
        {
            get
            {
                return _AddressLine2;
            }
            set
            {
                _AddressLine2 = value;
                OnPropertyChanged("AddressLine2");
            }
        }
        private string _ZipCode;
        public string ZipCode
        {
            get
            {
                return _ZipCode;
            }
            set
            {
                _ZipCode = value;
                OnPropertyChanged("ZipCode");
            }
        }

        private string _City;
        public string City
        {
            get
            {
                return _City;
            }
            set
            {
                _City = value;
                OnPropertyChanged("City");
            }
        }

        private string _State;
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

        private string _Country;
        public string Country
        {
            get
            {
                return _Country;
            }
            set
            {
                _Country = value;
                OnPropertyChanged("Country");
            }
        }
        //public int ID { get; set; }

        private bool _IsDefault;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsDefault
        {
            get
            {
                return _IsDefault;
            }
            set
            {
                _IsDefault = value;
                OnPropertyChanged(nameof(IsDefault));
            }
        }

        private string _FullName;
        public string FullName
        {
            get
            {
                return _FullName;
            }
            set
            {
                _FullName = value;
                OnPropertyChanged("FullName");
            }
        }
    }

    public class ShippingDeleteRequest
    {
        public int UserId { get; set; }
        public int ShippingAddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool IsDefault { get; set; }
        public string Country { get; set; }
        public bool IsBilling { get; set; }
        public bool IsBillingShippingSame { get; set; }
    }

    //public class AddAddressModel : BaseViewModel
    //{
    //    public int UserId { get; set; }
    //    public int ShippingAddressId { get; set; }
    //    public string AddressLine1 { get; set; }
    //    public string AddressLine2 { get; set; }
    //    public string City { get; set; }
    //    public string State { get; set; }
    //    public string ZipCode { get; set; }
    //    public bool IsDefault { get; set; }
    //    public string Country { get; set; }
    //}


}

