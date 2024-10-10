using System;
namespace BuySell.Services
{
	public interface IWebViewService
	{
        string Get();
        string ReadAssetFile(string filepath);
    }
}

