using FashionStoreViewModel;

namespace FashionStoreWebApp.Services
{
    public interface IUserService
    {
        Task<UserVm> GetUserById(string userId);
        Task<FormResult> CreateUserAsync(UserCreationDTO userCreation);
        Task<FormResult> UpdateUserAsync(UserCreationDTO userDto);
        Task<FormResult> DeleteUserAsync(string userId);
        Task<PagingData<UserVm>> GetPagedUsersAsync(UserSearchRequest userSearch, int pageIndex, int pageSize);
    }
}
