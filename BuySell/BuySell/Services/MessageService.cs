using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuySell.Services
{
    public class MessageService : IMessageService
    {
        public async Task ShowAsync(string message)
        {
            ContentPage page = (ContentPage)App.Current.MainPage.Navigation.NavigationStack.Last();
            await page.DisplayAlert("", message, "OK");
        }
    }
}
