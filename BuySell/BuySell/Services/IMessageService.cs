using System;
using System.Threading.Tasks;

namespace BuySell.Services
{
    public interface IMessageService
    {
        Task ShowAsync(string message);
    }
}
