using BasicIdentityCustomApi.Dtos;
using BasicIdentityCustomApi.Types;

namespace BasicIdentityCustomApi.Services
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user);

        Task<ServiceMessage<UserInfoDto>> LoginUser(LoginUserDto user);
    }
}
