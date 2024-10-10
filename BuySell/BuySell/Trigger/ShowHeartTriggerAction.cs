using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace BuySell.Trigger
{
    public class ShowHeartTriggerAction : TriggerAction<Xamarin.Forms.ImageButton>, INotifyPropertyChanged
    {
        public string FillHeart { get; set; }
        public string UnfillHeart { get; set; }

        bool _Heart = true;

        public bool Heart
        {
            set
            {
                if (_Heart != value)
                {
                    _Heart = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Heart)));
                }
            }
            get => _Heart;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected override void Invoke(ImageButton sender)
        {
            //if (string.IsNullOrWhiteSpace(Global.Password) && string.IsNullOrWhiteSpace(Global.Username))
            //{
            //    UserDialogs.Instance.Alert("Please login first");
            //    return;
            //}
            //else
            //{
            sender.Source = Heart ? FillHeart : UnfillHeart;
            Heart = !Heart;
            //}
        }
    }
}

