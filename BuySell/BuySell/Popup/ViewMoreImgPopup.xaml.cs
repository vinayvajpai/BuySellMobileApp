using System;
using System.Collections.Generic;
using System.Linq;
using BuySell.Helper;
using BuySell.Model;
using BuySell.ViewModel;
using FFImageLoading.Transformations;
using FFImageLoading.Work;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
namespace BuySell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMoreImgPopup : PopupPage
    {
        public event EventHandler<List<ProductSelectImage>> eventClose;
        ViewMoreImgViewModel vm;
        List<ProductSelectImage> selectedImgList;
        public ViewMoreImgPopup(List<Xamarin.Forms.ImageSource> imgList,string BgColor=null)
        {
            InitializeComponent();
            BindingContext= vm = new ViewMoreImgViewModel();
            vm.BgColor = BgColor!=null?BgColor:vm.ThemeColor;
            //CarouselViewControl.ItemsSource = imgList;
            if (imgList.Count > 1)
            {
                lblTitleText.Text = "View More Images";
                frm.IsVisible = true;
                imageViewContainer.IsVisible = false;
            }
            else
            {
                frm.IsVisible = false;
                imageViewContainer.IsVisible = true;
                ImageView.Source = imgList[0];
                this.CloseWhenBackgroundIsClicked = true;
            }
            vm.PropertyChanged += Vm_PropertyChanged;
        }
        private void Vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsDeleteAll"))
            {
                if (vm.IsDeleteAll)
                {
                    PopupCancelCommand(sender, null);
                }
            }
        }
        protected override void OnAppearing()
        {
            vm.IsTap = false;
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            vm.IsTap = false;
            base.OnDisappearing();
        }
        public ViewMoreImgPopup(List<ProductSelectImage> imgList, string BgColor=null)
        {
            InitializeComponent();
            selectedImgList = imgList;
            BindingContext = vm = new ViewMoreImgViewModel();
            vm.BgColor = BgColor != null ? BgColor : vm.ThemeColor;
            vm.Items = new System.Collections.ObjectModel.ObservableCollection<ProductSelectImage>(imgList);
            lblTitleText.Text = "View More Images";
            frm.IsVisible = true;
            imageViewContainer.IsVisible = false;
            vm.PropertyChanged += Vm_PropertyChanged;
            if (vm.Items.Count > 1)
            {
                vm.IsShowDeleteAll = true;
            }
            else
            {
                vm.IsShowDeleteAll = false;
            }
        }
        async void PopupCancelCommand(System.Object sender, System.EventArgs e)
        {
            if(eventClose != null)
            {
                //var list = vm.Items.ToList().Select(i=>i.image).ToList();
                eventClose.Invoke(sender, vm.Items.ToList());
            }
            await PopupNavigation.Instance.PopAsync();
        }

        private List<ITransformation> transformations;
        public List<ITransformation> Transformations
        {
            get
            {
                return transformations;
            }
            set
            {
                transformations = value;
                OnPropertyChanged("Transformations");
            }
        }

        private void DragGestureRecognizer_DragStarting(Object sender, DragStartingEventArgs e)
        {
            //var label = (Label)((Element)sender).Parent;
            ////Debug.WriteLine($"DragGestureRecognizer_DragStarting [{label.Text}]");

            //e.Data.Properties["Label"] = label;

            e.Handled = true;
        }
        private void DropGestureRecognizer_Drop(Object sender, DropEventArgs e)
        {
            //var label = (Label)((Element)sender).Parent;
            //var dropLabel = (Label)e.Data.Properties["Label"];
            //if (label == dropLabel)
            //    return;

            ////Debug.WriteLine($"DropGestureRecognizer_Drop [{dropLabel.Text}] => [{label.Text}]");

            //var sourceContainer = (Grid)dropLabel.Parent;
            //var targetContainer = (Grid)label.Parent;
            //sourceContainer.Children.Remove(dropLabel);
            //targetContainer.Children.Remove(label);
            //sourceContainer.Children.Add(label);
            //targetContainer.Children.Add(dropLabel);

            e.Handled = true;
        }
        private void DragGestureRecognizer_DragStarting_Collection(System.Object sender, Xamarin.Forms.DragStartingEventArgs e)
        {

        }
        private void DropGestureRecognizer_Drop_Collection(System.Object sender, Xamarin.Forms.DropEventArgs e)
        {
            // We handle reordering login in our view model
            e.Handled = true;
        }
    }
}