using ResellerApp.Api.Models.Ebay;

namespace ResellerApp.Api.Interfaces
{
    public interface IEbayService
    {
        //Task<string> GetApplicationTokenAsync();
        Task<EbayTokenResponse> GetApplicationTokenAsync();
    }
}