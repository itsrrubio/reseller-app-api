using ResellerApp.Api.DTOs;

namespace ResellerApp.Api.Services
{
    public interface IItemService
    {
        Task<int> CreateItemAsync(CreateItemDto dto);
        Task<List<ItemResponseDto>> GetItemsAsync();
        Task<ItemResponseDto> GetItemByIdAsync(int id);
        Task<bool> UpdateItemAsync(int id, UpdateItemDto dto);
        Task<bool> DeleteItemAsync(int id);
    }
}
