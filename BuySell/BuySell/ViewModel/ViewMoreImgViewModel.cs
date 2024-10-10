using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BuySell.Model;
using Controls.ImageCropper;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BuySell.ViewModel
{
    public class ViewMoreImgViewModel : BaseViewModel
    {
        #region Constructor
        public ViewMoreImgViewModel()
        {
            StateRefresh = new Command(OnStateRefresh);
            StateReset = new Command(OnStateReset);
            StateTest = new Command(OnStateTest);
            ItemDragged = new Command<ProductSelectImage>(OnItemDragged);
            ItemDraggedOver = new Command<ProductSelectImage>(OnItemDraggedOver);
            ItemDragLeave = new Command<ProductSelectImage>(OnItemDragLeave);
            ItemDropped = new Command<ProductSelectImage>(i => OnItemDropped(i));
            DeleteAllCommand = new Command(DeleteAll);
            DeleteCommand = new Command<ProductSelectImage>(async (obj) => {
                var result = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync("Are you sure you want to delete?", "Delete", "OK", "Cancel");
                if (result)
                {
                    Items.Remove(obj);
                    if (Items.Count == 0)
                    {
                        Items.Clear();
                        IsDeleteAll = true;
                    }
                    if (Items.Count > 1)
                    {
                        IsShowDeleteAll = true;
                    }
                    else
                    {
                        IsShowDeleteAll = false;
                    }
                }
            });

            EditCommand = new Command<ProductSelectImage>(async (obj) => {
                EditImage(obj);
            });
        }
        #endregion

        #region Commands
        public ICommand StateRefresh { get; }
        public ICommand StateReset { get; }
        public ICommand StateTest { get; }
        public ICommand ItemDragged { get; }
        public ICommand ItemDraggedOver { get; }
        public ICommand ItemDragLeave { get; }
        public ICommand ItemDropped { get; }
        public ICommand DeleteAllCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        #endregion

        #region Properties
        private ObservableCollection<ProductSelectImage> _items = new ObservableCollection<ProductSelectImage>();
        public ObservableCollection<ProductSelectImage> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        private string _BgColor;
        public string BgColor
        {
            get { return _BgColor; }
            set { SetProperty(ref _BgColor, value); }
        }

        private bool _IsDeleteAll;
        public bool IsDeleteAll
        {
            get { return _IsDeleteAll; }
            set { SetProperty(ref _IsDeleteAll, value); }
        }

        private bool _IsShowDeleteAll = false;
        public bool IsShowDeleteAll
        {
            get { return _IsShowDeleteAll; }
            set { SetProperty(ref _IsShowDeleteAll, value); }
        }
        #endregion

        #region Methods
        private async void DeleteAll()
        {
            var result = await Acr.UserDialogs.UserDialogs.Instance.ConfirmAsync("Are you sure you want to delete all images?","Delete","OK","Cancel");
            if (result)
            {
                Items.Clear();
                IsDeleteAll = true;
            }
        }
        private void OnStateRefresh()
        {
            Debug.WriteLine($"OnStateRefresh");
            OnPropertyChanged(nameof(Items));
            PrintItemsState();
        }
        private void OnStateReset()
        {
            Debug.WriteLine($"OnStateReset");
            //ResetItemsState();
            PrintItemsState();
        }
        private void OnStateTest()
        {
            //Items.RemoveAt(4);
            //Items.Insert(0, new ReorderImgListModel { Title = "Item new 1" });
            PrintItemsState();
        }
        private void OnItemDragged(ProductSelectImage item)
        {
            if (Items.Count == 0)
                return;
            //Debug.WriteLine($"OnItemDragged: {item?.Title}");
            Items.ForEach(i => i.IsBeingDragged = item == i);
        }
        private void OnItemDraggedOver(ProductSelectImage item)
        {
            if (Items.Count == 0)
                return;
            //Debug.WriteLine($"OnItemDraggedOver: {item?.Title}");
            var itemBeingDragged = _items.FirstOrDefault(i => i.IsBeingDragged);
            Items.ForEach(i => i.IsBeingDraggedOver = item == i && item != itemBeingDragged);
        }
        private void OnItemDragLeave(ProductSelectImage item)
        {
            if (Items.Count == 0)
                return;
            //Debug.WriteLine($"OnItemDragLeave: {item?.Title}");
            Items.ForEach(i => i.IsBeingDraggedOver = false);
        }
        private async Task OnItemDropped(ProductSelectImage item)
        {
            if (Items.Count == 0)
                return;

            var itemToMove = _items.First(i => i.IsBeingDragged);
            var itemToInsertBefore = item;

            if (itemToMove == null || itemToInsertBefore == null || itemToMove == itemToInsertBefore)
                return;

            var categoryToMoveFrom = Items.First(g => g==itemToMove);
            var insertAtIndex = Items.IndexOf(itemToInsertBefore);
            Items.Remove(itemToMove);
            itemToMove.image = categoryToMoveFrom.image;
            Items.Insert(insertAtIndex, itemToMove);
            itemToMove.IsBeingDragged = false;
            itemToInsertBefore.IsBeingDraggedOver = false;

            // Wait for remove animation to be completed
            // https://github.com/xamarin/Xamarin.Forms/issues/13791
            // await Task.Delay(1000);

            //var categoryToMoveTo = GroupedItems.First(g => g.Contains(itemToInsertBefore));
            //var insertAtIndex = categoryToMoveTo.IndexOf(itemToInsertBefore);
            //itemToMove.Category = categoryToMoveFrom.Name;
            //categoryToMoveTo.Insert(insertAtIndex, itemToMove);
            //itemToMove.IsBeingDragged = false;
            //itemToInsertBefore.IsBeingDraggedOver = false;
            //Debug.WriteLine($"OnItemDropped: [{itemToMove?.Title}] => [{itemToInsertBefore?.Title}], target index = [{insertAtIndex}]");

            PrintItemsState();
        }
        private void PrintItemsState()
        {
            Debug.WriteLine($"Items {Items.Count}, state:");
            for (int i = 0; i < Items.Count; i++)
            {
                //Debug.WriteLine($"\t{i}: Group: {Items[i].Category} | Item: {Items[i].Title}");
            }
        }
        private async void EditImage(ProductSelectImage proImg)
        {
            
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/tempReOrderEdit.png";
            File.WriteAllBytes(path, proImg.OrgimageBytes);
            var filePath = proImg.path;
            if (filePath.ToLower().Contains("http"))
            {
                filePath = path;
            }

            await ImageCropper.Current.Crop(new CropSettings()
            {
                CropShape = CropSettings.CropShapeType.Rectangle,
                PageTitle = "EDIT IMAGE",
                //AspectRatioX = 16,
                //AspectRatioY = 16,
            }, filePath).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    var ex = t.Exception;
                }
                else if (t.IsCanceled)
                {
                    
                }
                else if (t.IsCompleted)
                {
                    var result = t.Result;
                    proImg.imageBytes = File.ReadAllBytes(t.Result);
                    var imgSource = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(t.Result)));
                    proImg.image = imgSource;
                    OnPropertyChanged("Items");
                }
            });
        }
        #endregion
    }
}
