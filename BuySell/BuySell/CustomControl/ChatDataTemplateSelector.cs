using System;
using BuySell.Model;
using Xamarin.Forms;

namespace BuySell.CustomControl
{
    public class ChatDataTemplateSelector : DataTemplateSelector
    {
        public ChatDataTemplateSelector()
        {
        }

        public DataTemplate ValidTemplate { get; set; }
        public DataTemplate InvalidTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((OfferChatModel)item).Sender ? ValidTemplate : InvalidTemplate;
        }
    }
}
