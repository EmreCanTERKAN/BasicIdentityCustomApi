using BasicIdentityCustomApi.Dtos;
using BasicIdentityCustomApi.Types;

namespace BasicIdentityCustomApi.Services
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user);
    }
}
